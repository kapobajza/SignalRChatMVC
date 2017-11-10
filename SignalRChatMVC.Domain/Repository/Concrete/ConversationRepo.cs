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
    public class ConversationRepo : IConversationRepo
    {
        private EFContext _ctx;

        public ConversationRepo(EFContext ctx)
        {
            _ctx = ctx;
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

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Edit(Conversation entity, Conversation newValues)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _ctx.SaveChangesAsync();
        }

        public void WriteToDebugLog()
        {
            _ctx.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        //public IEnumerable<UserMessagesHelper> GetUserMessages()
        //{
        //    var list = _ctx.Chats.Select(x => new UserMessagesHelper() { UserName = x.User.UserName, Text = x.Message.Text, Sent = x.Sent }).OrderByDescending(x => x.Sent).ToList();

        //    return list;
        //}
    }
}
