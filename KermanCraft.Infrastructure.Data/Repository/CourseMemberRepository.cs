using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.Models.Product;

using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class CourseMemberRepository : ICourseMemberRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public CourseMemberRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<CourseMember>> AddCourseMember(CourseMember courseMember)
        {
            try
            {
                await _db.CourseMembers.AddAsync(courseMember);
                await _db.SaveChangesAsync();
                return new ResponseBase<CourseMember>()
                {
                    Entity = courseMember,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<CourseMember>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
  

    public async Task<ResponseBase<CourseMember>> UpdateCourseMember(CourseMember courseMember)
    {
        try
        {
            var model = await _db.CourseMembers.FindAsync(courseMember.CourseMemberId);
            if (model == null)
            {
                return new ResponseBase<CourseMember>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _mapper.Map(courseMember, model);
            _db.CourseMembers.Update(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<CourseMember>()
            {
                Entity = courseMember,
                IsSuccessful = true,
                Message = MessageResource.UpdateSuccess
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<CourseMember>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<CourseMember>> DeleteCourseMember(int courseMemberId)
    {
        try
        {
            var model = await _db.CourseMembers.FindAsync(courseMemberId);
            if (model == null)
            {
                return new ResponseBase<CourseMember>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _db.CourseMembers.Remove(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<CourseMember>()
            {
                Message = MessageResource.DeletSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<CourseMember>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<CourseMember>> GetCourseMember(int courseMemberId)
    {
        try
        {
            var model = await _db.CourseMembers.FindAsync(courseMemberId);
            if (model == null)
            {
                return new ResponseBase<CourseMember>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<CourseMember>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<CourseMember>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<IEnumerable<CourseMember>>> GetCourseMembers(int courseId)
    {
        try
        {
            var model = await _db.CourseMembers.Where(i=>i.CourseId==courseId).ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<CourseMember>>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<CourseMember>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<CourseMember>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
        public async Task<ResponseBase<IEnumerable<CourseMember>>> GetAllCourseMember()
    {
        try
        {
            var model = await _db.CourseMembers.ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<CourseMember>>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<CourseMember>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<CourseMember>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
}
}
