using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface ICourseMemberService
    {
        Task<ResponseBase<CourseMemberViewModel>> AddCourseMember(CourseMemberViewModel courseMember);
        Task<ResponseBase<CourseMemberViewModel>> UpdateCourseMember(CourseMemberViewModel courseMember);
        Task<ResponseBase<CourseMemberViewModel>> DeleteCourseMember(int courseMemberId);
        Task<ResponseBase<CourseMemberViewModel>> GetCourseMember(int courseMemberId);
        Task<ResponseBase<IEnumerable<CourseMemberViewModel>>> GetAllCourseMember();
        Task<ResponseBase<IEnumerable<CourseMemberViewModel>>> GetCourseMembers(int courseId);
        Task<ResponseBase<IEnumerable<CourseMemberViewModel>>> GetCustomerCourse(string userId);
    }
}
