using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Event
{
   public class EventMember
    {
        [Key]
        public int EventMemberId { get; set; }
        public int EventId { get; set; }
        public string PeopleFkId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public string ReferenceId { get; set; }
        public Event Event { get; set; }

    }
}
