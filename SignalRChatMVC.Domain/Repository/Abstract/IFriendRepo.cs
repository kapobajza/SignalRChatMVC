using SignalRChatMVC.Domain.DTO;
using SignalRChatMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IFriendRepo : IRepo<Friend>
    {
        IEnumerable<Friend> Friends { get; }
        IEnumerable<UserFriendsDTO> UserFriends(string username);
    }
}
