using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IEventMemberService
    {
        Task<ResponseBase<EventMemberViewModel>> AddEventMember(EventMemberViewModel eventMember);
        Task<ResponseBase<EventMemberViewModel>> UpdateEventMember(EventMemberViewModel eventMember);
        Task<ResponseBase<EventMemberViewModel>> DeleteEventMember(int eventMemberId);
        Task<ResponseBase<EventMemberViewModel>> GetEventMember(int eventMemberId);
        Task<ResponseBase<IEnumerable<EventMemberViewModel>>> GetAllEventMember();
        Task<ResponseBase<IEnumerable<EventMemberViewModel>>> GetEventMembers(int eventId);
        Task<ResponseBase<IEnumerable<EventMemberViewModel>>> GetCustomerEvent(string userId);

    }
}
