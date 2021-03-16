using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IEventService
    {
        Task<ResponseBase<EventViewModel>> AddEvent(EventViewModel eve);
        Task<ResponseBase<EventViewModel>> UpdateEvent(EventViewModel eve);
        Task<ResponseBase<EventViewModel>> DeleteEvent(int eveId);
        Task<ResponseBase<EventViewModel>> GetEvent(int eveId);
        Task<ResponseBase<IEnumerable<EventViewModel>>> GetAllEventByType(string type);
        Task<ResponseBase<IEnumerable<EventViewModel>>> GetAllEvent();
        Task<ResponseBase<IEnumerable<EventViewModel>>> GetArtistEvent(string userId);
    }
}
