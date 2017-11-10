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
    public class FriendRepo : IFriendRepo
    {
        private EFContext _ctx;
        public FriendRepo(EFContext ctx)
        {
            _ctx = ctx;
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

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(Friend entity, Friend newValues)
        {
            try
            {
                bool changed = false;
                var properties1 = newValues.GetType().GetProperties();
                var properties2 = entity.GetType().GetProperties();

                for (int i = 0; i < properties1.Length; i++)
                {
                    var value1 = properties1[i].GetValue(entity, null);
                    var value2 = properties2[i].GetValue(newValues, null);

                    if (properties1[i].Name == properties2[i].Name && value1 != value2)
                    {
                        properties1[i].SetValue(entity, value2, null);
                        changed = true;
                    }
                }

                if (changed)
                    _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<UserFriendsDTO> UserFriends(string username)
        {
            _ctx.Database.Log = s => Debug.WriteLine(s);
            
            var userFriends = (from friends in _ctx.Friends.AsNoTracking()
                               join users in _ctx.Users.AsNoTracking()
                               on friends.FriendId equals users.UserName
                               where friends.UserId == username && friends.Accepted == true
                               select new UserFriendsDTO() { FriendName = friends.FriendId, IsOnline = users.IsOnline }).ToList();
            
            return userFriends;
        }
    }
}
