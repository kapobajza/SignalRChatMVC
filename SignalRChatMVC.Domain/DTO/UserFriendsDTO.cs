using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.DTO
{
    public class UserFriendsDTO
    {
        public string FriendName { get; set; }
        public bool IsOnline { get; set; }
    }
}
