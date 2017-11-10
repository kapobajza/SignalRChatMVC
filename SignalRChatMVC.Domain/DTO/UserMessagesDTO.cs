using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.DTO
{
    public class UserMessagesDTO
    {
        public string ConversationId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Sent { get; set; }
    }
}
