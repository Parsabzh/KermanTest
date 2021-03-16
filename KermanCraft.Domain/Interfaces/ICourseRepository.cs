using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<ResponseBase<Course>> AddCourse(Course course);
        Task<ResponseBase<Course>> UpdateCourse(Course course);
        Task<ResponseBase<Course>> DeleteCourse(int courseId);
        Task<ResponseBase<Course>> GetCourse(int courseId);
        Task<ResponseBase<IEnumerable<Course>>> GetAllCourse();
        Task<ResponseBase<IEnumerable<Course>>> GetUserCourse(string user);
    }
}
