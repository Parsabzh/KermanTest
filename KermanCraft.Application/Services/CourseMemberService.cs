using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class CourseMemberService:ICourseMemberService
    {
        private readonly ICourseMemberRepository _courseMemberRepository;
        private readonly IMapper _mapper;

        public CourseMemberService(IMapper mapper, ICourseMemberRepository courseMemberRepository)
        {
            _mapper = mapper;
            _courseMemberRepository = courseMemberRepository;
        }

        public async Task<ResponseBase<CourseMemberViewModel>> AddCourseMember(CourseMemberViewModel courseMember)
        {
          
            var result = await _courseMemberRepository.AddCourseMember(_mapper.Map<CourseMember>(courseMember));
            return new ResponseBase<CourseMemberViewModel>()
            {
                Entity = _mapper.Map<CourseMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<CourseMemberViewModel>> UpdateCourseMember(CourseMemberViewModel courseMember)
        {
          
            var result = await _courseMemberRepository.UpdateCourseMember(_mapper.Map<CourseMember>(courseMember));
            return new ResponseBase<CourseMemberViewModel>()
            {
                Entity = _mapper.Map<CourseMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CourseMemberViewModel>> DeleteCourseMember(int courseMemberId)
        {
            var result = await _courseMemberRepository.DeleteCourseMember(courseMemberId);
            return new ResponseBase<CourseMemberViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CourseMemberViewModel>> GetCourseMember(int courseMemberId)
        {
            var result = await _courseMemberRepository.GetCourseMember(courseMemberId);
            return new ResponseBase<CourseMemberViewModel>()
            {
                Entity = _mapper.Map<CourseMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CourseMemberViewModel>>> GetAllCourseMember()
        {

            var result = await _courseMemberRepository.GetAllCourseMember();
            return new ResponseBase<IEnumerable<CourseMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CourseMemberViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CourseMemberViewModel>>> GetCourseMembers(int courseId)
        {
            var result = await _courseMemberRepository.GetCourseMembers(courseId);
            return new ResponseBase<IEnumerable<CourseMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CourseMemberViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CourseMemberViewModel>>> GetCustomerCourse(string userId)
        {
            var result = await _courseMemberRepository.GetAllCourseMember();
            var userCourse = result.Entity.Where(i => i.PeopleFkId == userId);
            return new ResponseBase<IEnumerable<CourseMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CourseMemberViewModel>>(userCourse),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
