using SignalRChatMVC.Domain.DTO;
using SignalRChatMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatMVC.SignalRHelper
{
    public class UserSignalR
    {
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        public List<UserFriendsDTO> Friends { get; set; }
    }
}