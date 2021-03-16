using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<ResponseBase<Event>> AddEvent(Event eve);
        Task<ResponseBase<Event>> UpdateEvent(Event eve);
        Task<ResponseBase<Event>> DeleteEvent(int eveId);
        Task<ResponseBase<Event>> GetEvent(int eveId);
        Task<ResponseBase<IEnumerable<Event>>> GetAllEvent();
        Task<ResponseBase<IEnumerable<Event>>> GetArtistEvent(string userId);
        Task<ResponseBase<IEnumerable<Event>>> GetCustomerEvent(string userId);



    }
}
