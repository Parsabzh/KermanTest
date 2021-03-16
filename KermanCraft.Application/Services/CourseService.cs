using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class CourseService:ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<ResponseBase<CourseViewModel>> AddCourse(CourseViewModel course)
        {
            course.DateTime = PersianDate.ToGeorgianDate(course.DateTime).Date.ToString();
            var result = await _courseRepository.AddCourse(_mapper.Map<Course>(course));
            return new ResponseBase<CourseViewModel>()
            {
                Entity = _mapper.Map<CourseViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<CourseViewModel>> UpdateCourse(CourseViewModel course)
        {
            course.DateTime = PersianDate.ToGeorgianDate(course.DateTime).Date.ToString();
            var result = await _courseRepository.UpdateCourse(_mapper.Map<Course>(course));
            return new ResponseBase<CourseViewModel>()
            {
                Entity = _mapper.Map<CourseViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CourseViewModel>> DeleteCourse(int courseId)
        {
            var result = await _courseRepository.DeleteCourse(courseId);
            return new ResponseBase<CourseViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CourseViewModel>> GetCourse(int courseId)
        {
            var result = await _courseRepository.GetCourse(courseId);
            return new ResponseBase<CourseViewModel>()
            {
                Entity = _mapper.Map<CourseViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CourseViewModel>>> GetAllCourse()
        {

            var result = await _courseRepository.GetAllCourse();
            return new ResponseBase<IEnumerable<CourseViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CourseViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CourseViewModel>>> GetUserCourse(string userId)
        {
            var result = await _courseRepository.GetAllCourse();
            return new ResponseBase<IEnumerable<CourseViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CourseViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
