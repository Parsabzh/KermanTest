using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Comment;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly KermanCraftDb _db;
        private readonly UserManager<People> _userManager;
        private readonly IMapper _mapper;

        public CommentRepository(KermanCraftDb db, IMapper mapper, UserManager<People> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseBase<Comment>> AddComment(Comment comment)
        {
            try
            {
                await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                return new ResponseBase<Comment>()
                {
                    Entity = comment,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Comment>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Comment>> UpdateComment(Comment comment)
        {
            try
            {
                var model = await _db.Comments.FindAsync(comment.CommentId);
                if (model == null)
                {
                    return new ResponseBase<Comment>()
                    {
                        Message = "دیدگاه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(comment, model);
                _db.Comments.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Comment>()
                {
                    Entity = comment,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Comment>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Comment>> DeleteComment(int commentId)
        {
            try
            {
                var model = await _db.Comments.FindAsync(commentId);
                if (model == null)
                {
                    return new ResponseBase<Comment>()
                    {
                        Message = "دیدگاه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.Comments.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Comment>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Comment>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Comment>> GetComment(int commentId)
        {
            try
            {
                var model = await _db.Comments.FindAsync(commentId);
                if (model == null)
                {
                    return new ResponseBase<Comment>()
                    {
                        Message = "دیدگاه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Comment>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Comment>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Comment>>> GetAllComment()
        {
            try
            {
                var model = await _db.Comments.ToListAsync();
                if (model == null)
                {
                    return new ResponseBase<IEnumerable<Comment>>()
                    {
                        Message = "دیدگاه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Comment>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Comment>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

       
        public async Task<ResponseBase<IEnumerable<Comment>>> GetCustomerComment(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseBase<IEnumerable<Comment>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                }
                var user = await _userManager.FindByIdAsync(userId);
                var isInRole = await _userManager.IsInRoleAsync(user, "Customer");
                if (!isInRole)
                    return new ResponseBase<IEnumerable<Comment>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                var model = await _db.Comments.Where(i => i.PeopleId == userId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Comment>>()
                    {
                        Message = "دیدگاه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Comment>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Comment>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Comment>>> GetTypeComment(string type, int typeId)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                {
                    return new ResponseBase<IEnumerable<Comment>>()
                    {
                        Message = string.Format(MessageResource.NotFound, "نوع"),
                        IsSuccessful = false
                    };
                }

                var model = await _db.Comments.Where(i => i.Type == type && i.TypeId == typeId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Comment>>()
                    {
                        Message = "دیدگاه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Comment>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Comment>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
