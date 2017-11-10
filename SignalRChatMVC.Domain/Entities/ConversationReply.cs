using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Entities
{
    [Table("ConversationReplies")]
    public class ConversationReply
    {
        public int ConversationReplyId { get; set; }

        [Required]
        [StringLength(300)]
        public string Text { get; set; }

        public DateTime Sent { get; set; }

        [StringLength(50)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(50)]
        public string ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}
