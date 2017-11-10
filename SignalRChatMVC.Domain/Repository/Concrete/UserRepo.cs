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
    public class UserRepo : BaseRepoEntity<User>, IUserRepo
    {
        public UserRepo(EFContext ctx) : base(ctx)
        {

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

        public override void Edit(User entity, User newValues)
        {
            base.Edit(entity, newValues);

                if(propertyChanged)
                    _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<User> AddRange(IEnumerable<User> entities)
        {
            return _ctx.Users.AddRange(entities);
        }
    }
}
