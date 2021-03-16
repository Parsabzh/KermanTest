using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class EventMemberService:IEventMemberService
    {
        private readonly IEventMemberRepository _eventMemberRepository;
        private readonly IMapper _mapper;

        public EventMemberService(IMapper mapper, IEventMemberRepository eventMemberRepository)
        {
            _mapper = mapper;
            _eventMemberRepository = eventMemberRepository;
        }

        public async Task<ResponseBase<EventMemberViewModel>> AddEventMember(EventMemberViewModel eventMember)
        {
          
            var result = await _eventMemberRepository.AddEventMember(_mapper.Map<EventMember>(eventMember));
            return new ResponseBase<EventMemberViewModel>()
            {
                Entity = _mapper.Map<EventMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<EventMemberViewModel>> UpdateEventMember(EventMemberViewModel eventMember)
        {
          
            var result = await _eventMemberRepository.UpdateEventMember(_mapper.Map<EventMember>(eventMember));
            return new ResponseBase<EventMemberViewModel>()
            {
                Entity = _mapper.Map<EventMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<EventMemberViewModel>> DeleteEventMember(int eventMemberId)
        {
            var result = await _eventMemberRepository.DeleteEventMember(eventMemberId);
            return new ResponseBase<EventMemberViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<EventMemberViewModel>> GetEventMember(int eventMemberId)
        {
            var result = await _eventMemberRepository.GetEventMember(eventMemberId);
            return new ResponseBase<EventMemberViewModel>()
            {
                Entity = _mapper.Map<EventMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<EventMemberViewModel>>> GetAllEventMember()
        {

            var result = await _eventMemberRepository.GetAllEventMember();
            return new ResponseBase<IEnumerable<EventMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<EventMemberViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<EventMemberViewModel>>> GetEventMembers(int eventId)
        {
            var result = await _eventMemberRepository.GetEventMembers(eventId);
            return new ResponseBase<IEnumerable<EventMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<EventMemberViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<EventMemberViewModel>>> GetCustomerEvent(string userId)
        {
            var result = await _eventMemberRepository.GetAllEventMember();
            var userEvent = result.Entity.Where(i => i.PeopleFkId == userId);
                return new ResponseBase<IEnumerable<EventMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<EventMemberViewModel>>(userEvent),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
