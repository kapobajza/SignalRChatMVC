using SignalRChatMVC.Domain.DAL;
using SignalRChatMVC.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.DTO;
using System.Diagnostics;

namespace SignalRChatMVC.Domain.Repository.Concrete
{
    public class FriendRepo : BaseRepoEntity<Friend>, IFriendRepo
    {
        public FriendRepo(EFContext ctx) : base(ctx)
        {

        }

        public IEnumerable<Friend> Friends
        {
            get
            {
                return _ctx.Friends;
            }
        }

        public bool Add(Friend entity)
        {
            return _ctx.Friends.Add(entity) != null;
        }

        public IEnumerable<Friend> AddRange(IEnumerable<Friend> entities)
        {
            return _ctx.Friends.AddRange(entities);
        }

        public override void Edit(Friend entity, Friend newValues)
        {
            base.Edit(entity, newValues);

            if (propertyChanged)
                _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<UserFriendsDTO> UserFriends(string username)
        {
            //_ctx.Database.Log = s => Debug.WriteLine(s);

            var userFriends = (from friends in _ctx.Friends.AsNoTracking()
                               join users in _ctx.Users.AsNoTracking()
                               on friends.FriendId equals users.UserName
                               where friends.UserId == username && friends.Accepted == true
                               select new UserFriendsDTO() { FriendName = friends.FriendId, IsOnline = users.IsOnline }).ToList();

            return userFriends;
        }
    }
}
