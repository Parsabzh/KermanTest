using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface ICourseService
    {
        Task<ResponseBase<CourseViewModel>> AddCourse(CourseViewModel course);
        Task<ResponseBase<CourseViewModel>> UpdateCourse(CourseViewModel course);
        Task<ResponseBase<CourseViewModel>> DeleteCourse(int courseId);
        Task<ResponseBase<CourseViewModel>> GetCourse(int courseId);
        Task<ResponseBase<IEnumerable<CourseViewModel>>> GetAllCourse();
        Task<ResponseBase<IEnumerable<CourseViewModel>>> GetUserCourse(string userId);
    }
}
