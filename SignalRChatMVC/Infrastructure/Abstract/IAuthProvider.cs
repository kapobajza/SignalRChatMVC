using SignalRChatMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatMVC.Infrastructure.Abstract
{
    public interface IAuthProvider : IDisposable
    {
        bool Login(string username, string password);
        void Logout();
        bool Register(User user);
    }
}