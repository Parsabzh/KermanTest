using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Tag;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public TagRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<Tag>> AddTag(Tag tag)
        {
            try
            {
                await _db.Tags.AddAsync(tag);
                await _db.SaveChangesAsync();
                return new ResponseBase<Tag>()
                {
                    Entity = tag,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Tag>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Tag>> UpdateTag(Tag tag)
        {
            try
            {
                var model = await _db.Tags.FindAsync(tag.TagId);
                if (model == null)
                {
                    return new ResponseBase<Tag>()
                    {
                        Message = "برچسب موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(tag, model);
                _db.Tags.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Tag>()
                {
                    Entity = tag,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Tag>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Tag>> DeleteTag(int tagId)
        {
            try
            {
                var model = await _db.Tags.FindAsync(tagId);
                if (model == null)
                {
                    return new ResponseBase<Tag>()
                    {
                        Message = "برچسب موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.Tags.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Tag>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Tag>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Tag>> GetTag(int tagId)
        {
            try
            {
                var model = await _db.Tags.FindAsync(tagId);
                if (model == null)
                {
                    return new ResponseBase<Tag>()
                    {
                        Message = "برچسب موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Tag>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Tag>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Tag>>> GetProductTag(int productId)
        {
            try
            {
                var model = await _db.Tags.Where(i => i.ProductFkId == productId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Tag>>()
                    {
                        Entity = null,
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound,"برچسب")
                    };
                }

                return new ResponseBase<IEnumerable<Tag>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Tag>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };

            }
        }

        public async Task<ResponseBase<IEnumerable<Tag>>> GetNewsTag(int newsId)
        {
            try
            {
                var model = await _db.Tags.Where(i => i.NewsFkId == newsId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Tag>>()
                    {
                        Entity = null,
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, "برچسب")
                    };
                }

                return new ResponseBase<IEnumerable<Tag>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Tag>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };

            }
        }

        public async Task<ResponseBase<IEnumerable<Tag>>> GetArticleTag(int articleId)
        {
            try
            {
                var model = await _db.Tags.Where(i => i.ArticleFkId == articleId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Tag>>()
                    {
                        Entity = null,
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, "برچسب")
                    };
                }

                return new ResponseBase<IEnumerable<Tag>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Tag>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };

            }
        }

        public async Task<ResponseBase<IEnumerable<Tag>>> GetAllTag()
        {
            try
            {
                var model = await _db.Tags.ToListAsync();
                if (model == null)
                {
                    return new ResponseBase<IEnumerable<Tag>>()
                    {
                        Message = "برچسب موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Tag>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Tag>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
