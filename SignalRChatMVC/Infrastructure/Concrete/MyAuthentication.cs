using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.Repository.Abstract;
using SignalRChatMVC.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatMVC.Infrastructure.Concrete
{
    public class MyAuthentication : IAuthProvider
    {
        private IUserRepo _userRepo;

        public static User UserSession
        {
            get { return (User)HttpContext.Current.Session["user"]; }
            set { HttpContext.Current.Session["user"] = value; }
        }

        public MyAuthentication(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public bool Login(string username, string password)
        {
            try
            {
                var user = _userRepo.GetUserByUsernamePassword(username, password);

                if (user != null)
                {
                    UserSession = user;
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Logout()
        {
            UserSession = null;
        }

        public bool Register(User user)
        {
            try
            {
                user.IsOnline = true;

                if (_userRepo.Add(user))
                {
                    _userRepo.SaveChanges();
                    UserSession = user;
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }
    }
}