using System;

namespace KermanCraft.Application.ViewModels.Event
{
    public class EventMemberViewModel
    {

        public int EventMemberId { get; set; }
        public int EventId { get; set; }
        public string PeopleFkId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public string ReferenceId { get; set; }


    }
}
