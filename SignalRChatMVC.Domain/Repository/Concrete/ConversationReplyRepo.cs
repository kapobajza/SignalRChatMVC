using SignalRChatMVC.Domain.DAL;
using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.DTO;
using SignalRChatMVC.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Concrete
{
    public class ConversationReplyRepo : IConversationReplyRepo
    {
        private EFContext _ctx;

        public ConversationReplyRepo(EFContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ConversationReply> ConversationReplies
        {
            get
            {
                return _ctx.ConversationReplies;
            }
        }

        public bool Add(ConversationReply entity)
        {
            return _ctx.ConversationReplies.Add(entity) != null ? true : false;
        }

        public IEnumerable<ConversationReply> AddRange(IEnumerable<ConversationReply> entities)
        {
            return _ctx.ConversationReplies.AddRange(entities);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(ConversationReply entity, ConversationReply newValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserMessagesDTO> GetAllMessages(string conversation)
        {
            var tempMessages = new List<UserMessagesDTO>();
            var messages = _ctx.ConversationReplies.AsNoTracking().Select(x => new { ConversationId = x.ConversationId, UserName = x.User.UserName, Text = x.Text, Sent = x.Sent }).Where(x => x.ConversationId == conversation).ToList();

            foreach (var item in messages)
            {
                tempMessages.Add(new UserMessagesDTO()
                {
                    ConversationId = item.ConversationId,
                    Sent = item.Sent.ToString("dd.MM.yyyy HH:mm:ss"),
                    Text = item.Text,
                    UserName = item.UserName
                });
            }

            return tempMessages;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}
