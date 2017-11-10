using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Entities
{
    public class Friend
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("UserFriend")]
        [StringLength(50)]
        public string FriendId { get; set; }
        public virtual User UserFriend { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        [StringLength(50)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public bool? Waiting { get; set; }
        public bool? Accepted { get; set; }
    }
}
