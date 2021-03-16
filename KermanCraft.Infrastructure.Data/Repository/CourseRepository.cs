using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public CourseRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<Course>> AddCourse(Course course)
        {
            try
            {
                await _db.Courses.AddAsync(course);
                await _db.SaveChangesAsync();
                return new ResponseBase<Course>()
                {
                    Entity = course,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Course>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Course>> UpdateCourse(Course course)
        {
            try
            {
                var model = await _db.Courses.FindAsync(course.CourseId);
                if (model == null)
                {
                    return new ResponseBase<Course>()
                    {
                        Message = "مطلب آموزشی موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(course, model);
                _db.Courses.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Course>()
                {
                    Entity = course,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Course>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Course>> DeleteCourse(int courseId)
        {
            try
            {
                var model = await _db.Courses.FindAsync(courseId);
                if (model == null)
                {
                    return new ResponseBase<Course>()
                    {
                        Message = "مطلب آموزشی موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.Courses.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Course>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Course>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Course>> GetCourse(int courseId)
        {
            try
            {
                var model = await _db.Courses.FindAsync(courseId);
                if (model == null)
                {
                    return new ResponseBase<Course>()
                    {
                        Message = "مطلب آموزشی موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Course>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Course>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Course>>> GetAllCourse()
        {
            try
            {
                var model = await _db.Courses.ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Course>>()
                    {
                        Message = "مطلب آموزشی موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Course>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Course>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Course>>> GetUserCourse(string user)
        {
            try
            {
                var model = await _db.Courses.Where(i => i.PeopleId == user).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Course>>()
                    {
                        Message = "مطلب آموزشی موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Course>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Course>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }



    }
}
