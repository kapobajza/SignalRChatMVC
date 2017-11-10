using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Entities
{
    public class Conversation
    {
        [StringLength(50)]
        public string ConversationId { get; set; }

        [ForeignKey("ToUser")]
        [StringLength(50)]
        public string ToUserFK { get; set; }
        public virtual User ToUser { get; set; }

        [ForeignKey("FromUser")]
        [StringLength(50)]
        public string FromUserFK { get; set; }
        public virtual User FromUser { get; set; }
    }
}
