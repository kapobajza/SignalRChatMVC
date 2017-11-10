using SignalRChatMVC.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.DAL;

namespace SignalRChatMVC.Domain.Repository.Concrete
{
    public class UserRepo : IUserRepo
    {
        private EFContext _ctx;
        public UserRepo(EFContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<User> Users
        {
            get
            {
                try
                {
                    return _ctx.Users;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool Add(User user)
        {
            try
            {
                var u = _ctx.Users.SingleOrDefault(x => x.UserName == user.UserName);

                if (u == null)
                {
                    _ctx.Users.Add(user);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Save()
        {
            try
            {
                _ctx.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public User GetUserByUsernamePassword(string username, string password = "")
        {
            try
            {
                return _ctx.Users.SingleOrDefault(x => x.UserName == username && 
                        (x.Password == password || password == ""));
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Edit(User entity, User newValues)
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

                if(changed)
                    _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            try
            {
                _ctx.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> SaveAsync()
        {
            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<User> AddRange(IEnumerable<User> entities)
        {
            return _ctx.Users.AddRange(entities);
        }
    }
}
