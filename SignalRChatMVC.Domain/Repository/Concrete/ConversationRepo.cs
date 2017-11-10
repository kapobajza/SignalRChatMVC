using SignalRChatMVC.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.DAL;
using System.Data.Entity;
using SignalRChatMVC.Domain.DTO;

namespace SignalRChatMVC.Domain.Repository.Concrete
{
    public class ConversationRepo : BaseRepoEntity<Conversation>, IConversationRepo
    {
        public ConversationRepo(EFContext ctx) : base(ctx)
        {

        }

        public IEnumerable<Conversation> Conversations
        {
            get
            {
                return _ctx.Conversations;
            }
        }

        public bool Add(Conversation entity)
        {
            return _ctx.Conversations.Add(entity) != null ? true : false;
        }

        public IEnumerable<Conversation> AddRange(IEnumerable<Conversation> entities)
        {
            return _ctx.Conversations.AddRange(entities);
        }

        public override void Edit(Conversation entity, Conversation newValues)
        {
            base.Edit(entity, newValues);

            if (propertyChanged)
                _ctx.Entry(entity).State = EntityState.Modified;
        }

        //public IEnumerable<UserMessagesHelper> GetUserMessages()
        //{
        //    var list = _ctx.Chats.Select(x => new UserMessagesHelper() { UserName = x.User.UserName, Text = x.Message.Text, Sent = x.Sent }).OrderByDescending(x => x.Sent).ToList();

        //    return list;
        //}
    }
}
