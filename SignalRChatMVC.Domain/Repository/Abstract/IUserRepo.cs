using SignalRChatMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IUserRepo : IRepo<User>, IBaseRepo<User>
    {
        IEnumerable<User> Users { get; }
        User GetUserByUsernamePassword(string username, string password = "");
    }
}
