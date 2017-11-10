using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRChatMVC.Domain.Repository.Abstract;
using SignalRChatMVC.Infrastructure.Concrete;
using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.Repository.Concrete;
using SignalRChatMVC.Domain.DAL;
using System.Collections.Concurrent;
using SignalRChatMVC.SignalRHelper;

namespace SignalRChatMVC
{
    public class ChatHub : Hub
    {
        private EFContext _ctx = new EFContext();
        private IUserRepo _userRepo;
        private IConversationReplyRepo _conversationReplyRepo;
        private IFriendRepo _friendRepo;
        private static ConcurrentDictionary<string, UserSignalR> _connectedUsers = new ConcurrentDictionary<string, UserSignalR>();
        //private IConversationRepo _conversationRepo;

        public async Task GlobalSend(string username, string message)
        {
            using (_conversationReplyRepo = new ConversationReplyRepo(_ctx))
            {
                using (_userRepo = new UserRepo(_ctx))
                {
                    var dateTime = DateTime.Now;

                    _conversationReplyRepo.Add(new ConversationReply() { UserId = username, Sent = dateTime, Text = message, ConversationId = "Global" });
                    await _conversationReplyRepo.SaveChangesAsync();

                    Clients.All.globalSend(username, message, dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
                }
            }
        }

        public async Task FriendRequest(string fromUser, string toUser)
        {
            using (_friendRepo = new FriendRepo(_ctx))
            {
                var friends = new List<Friend>()
                {
                    new Friend() { UserId = fromUser, FriendId = toUser, Waiting = true, Accepted = false },
                    new Friend() { UserId = toUser, FriendId = fromUser, Waiting = true, Accepted = false }
                };

                _friendRepo.AddRange(friends);

                await _friendRepo.SaveChangesAsync();

                if (_connectedUsers[toUser] != null)
                    Clients.Client(_connectedUsers[toUser].ConnectionId).newFriendRequest(fromUser);
            }
        }

        public async Task FriendRequestAccepted(string fromUser, string toUser)
        {
            using (_friendRepo = new FriendRepo(_ctx))
            {
                var friends = _friendRepo.Friends.Where(x => (x.UserId == fromUser && x.FriendId == toUser) || (x.UserId == toUser && x.FriendId == fromUser)).ToList();

                if (friends != null)
                {
                    foreach (var friend in friends)
                    {
                        var newFriend = friend;
                        newFriend.Waiting = false;
                        newFriend.Accepted = true;

                        _friendRepo.Edit(friend, newFriend);
                    }

                    await _friendRepo.SaveChangesAsync();

                    if(_connectedUsers[toUser] != null)
                    {
                        var user = new Domain.DTO.UserFriendsDTO()
                        {
                            FriendName = fromUser,
                            IsOnline = _connectedUsers[fromUser] != null ? true : false
                        };

                        _connectedUsers[toUser].Friends.Add(user);

                        Clients.Client(_connectedUsers[toUser].ConnectionId).toUserAccepted(user);
                    }

                    if (_connectedUsers[fromUser] != null)
                    {
                        var user = new Domain.DTO.UserFriendsDTO()
                        {
                            FriendName = toUser,
                            IsOnline = _connectedUsers[toUser] != null ? true : false
                        };

                        _connectedUsers[fromUser].Friends.Add(user);

                        Clients.Client(_connectedUsers[fromUser].ConnectionId).fromUserAccepted(user);
                    }
                }
            }
        }

        public override Task OnConnected()
        {
            using (_userRepo = new UserRepo(_ctx))
            {
                using (_friendRepo = new FriendRepo(_ctx))
                {
                    var username = Context.Request.QueryString["username"];
                    var dbUser = _userRepo.Users.FirstOrDefault(x => x.UserName == username);
                    var newUser = dbUser;
                    newUser.IsOnline = true;

                    _userRepo.Edit(dbUser, newUser);

                    _userRepo.SaveChanges();

                    var userFriends = _friendRepo.UserFriends(username);

                    var user = new UserSignalR()
                    {
                        ConnectionId = Context.ConnectionId,
                        Username = username,
                        Friends = userFriends.ToList()
                    };


                    _connectedUsers.TryAdd(username, user);

                    Clients.AllExcept(Context.ConnectionId).clientConnected(username);
                    return Clients.Caller.connected(userFriends);
                }
            }
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            using (_userRepo = new UserRepo(_ctx))
            {
                UserSignalR u = null;
                var username = Context.Request.QueryString["username"];
                var dbUser = _userRepo.Users.FirstOrDefault(x => x.UserName == username);
                var newUser = dbUser;
                newUser.IsOnline = false;
                _userRepo.Edit(dbUser, newUser);

                _connectedUsers.TryRemove(username, out u);

                await _userRepo.SaveChangesAsync();
                Clients.AllExcept(Context.ConnectionId).clientDisconnected(username);
            }
        }
    }
}