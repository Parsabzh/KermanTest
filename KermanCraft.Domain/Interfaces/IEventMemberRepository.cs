using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IEventMemberRepository
    {
        Task<ResponseBase<EventMember>> AddEventMember(EventMember eventMember);
        Task<ResponseBase<EventMember>> UpdateEventMember(EventMember eventMember);
        Task<ResponseBase<EventMember>> DeleteEventMember(int eventMemberId);
        Task<ResponseBase<EventMember>> GetEventMember(int eventMemberId);
        Task<ResponseBase<IEnumerable<EventMember>>> GetAllEventMember();
        Task<ResponseBase<IEnumerable<EventMember>>> GetEventMembers(int eventId);
    }
}
