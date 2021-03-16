using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class EventService:IEventService
    {
        private readonly IEventRepository _eveRepository;
        private readonly IMapper _mapper;

        public EventService(IMapper mapper, IEventRepository eveRepository)
        {
            _mapper = mapper;
            _eveRepository = eveRepository;
        }

        public async Task<ResponseBase<EventViewModel>> AddEvent(EventViewModel eve)
        {
            eve.Date = PersianDate.ToGeorgianDate(eve.Date).Date.ToString("d");
            var result = await _eveRepository.AddEvent(_mapper.Map<Event>(eve));
            return new ResponseBase<EventViewModel>()
            {
                Entity = _mapper.Map<EventViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<EventViewModel>> UpdateEvent(EventViewModel eve)
        {
            eve.Date = PersianDate.ToGeorgianDate(eve.Date).Date.ToString("d");
            var result = await _eveRepository.UpdateEvent(_mapper.Map<Event>(eve));
            return new ResponseBase<EventViewModel>()
            {
                Entity = _mapper.Map<EventViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<EventViewModel>> DeleteEvent(int eveId)
        {
            var result = await _eveRepository.DeleteEvent(eveId);
            return new ResponseBase<EventViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<EventViewModel>> GetEvent(int eveId)
        {
            var result = await _eveRepository.GetEvent(eveId);
            return new ResponseBase<EventViewModel>()
            {
                Entity = _mapper.Map<EventViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<EventViewModel>>> GetAllEventByType(string type)
        {

            var result = await _eveRepository.GetAllEvent();
            var model = result.Entity.Where(i => i.Type == type);
            return new ResponseBase<IEnumerable<EventViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<EventViewModel>>(model),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<EventViewModel>>> GetAllEvent()
        {
            var result = await _eveRepository.GetAllEvent();
            return new ResponseBase<IEnumerable<EventViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<EventViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<EventViewModel>>> GetArtistEvent(string userId)
        {
            var result = await _eveRepository.GetArtistEvent(userId);
            return new ResponseBase<IEnumerable<EventViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<EventViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

      
    }
}
