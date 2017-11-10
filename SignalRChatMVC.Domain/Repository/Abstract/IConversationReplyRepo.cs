using SignalRChatMVC.Domain.DTO;
using SignalRChatMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IConversationReplyRepo : IRepo<ConversationReply>
    {
        IEnumerable<ConversationReply> ConversationReplies { get; }
        IEnumerable<UserMessagesDTO> GetAllMessages(string conversationId);
    }
}
