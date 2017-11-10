using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IConversationRepo : IRepo<Conversation>
    {
        IEnumerable<Conversation> Conversations { get; }
        //IEnumerable<UserMessagesHelper> GetUserMessages();
    }
}
