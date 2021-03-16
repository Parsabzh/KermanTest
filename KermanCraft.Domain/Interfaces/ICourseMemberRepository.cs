using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ICourseMemberRepository
    {
        Task<ResponseBase<CourseMember>> AddCourseMember(CourseMember courseMember);
        Task<ResponseBase<CourseMember>> UpdateCourseMember(CourseMember courseMember);
        Task<ResponseBase<CourseMember>> DeleteCourseMember(int courseMemberId);
        Task<ResponseBase<CourseMember>> GetCourseMember(int courseMemberId);
        Task<ResponseBase<IEnumerable<CourseMember>>> GetAllCourseMember();
        Task<ResponseBase<IEnumerable<CourseMember>>> GetCourseMembers(int courseId);
    }
}
