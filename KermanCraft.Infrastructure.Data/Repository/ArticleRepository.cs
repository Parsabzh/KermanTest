using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Article;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public ArticleRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<Article>> AddArticle(Article article)
        {
            try
            {
                await _db.Articles.AddAsync(article);
                await _db.SaveChangesAsync();
                return new ResponseBase<Article>()
                {
                    Entity = article,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Article>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Article>> UpdateArticle(Article article)
        {
            try
            {
                var model = await _db.Articles.FindAsync(article.ArticleId);
                if (model == null)
                {
                    return new ResponseBase<Article>()
                    {
                        Message = "مقاله موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(article, model);
                _db.Articles.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Article>()
                {
                    Entity = article,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Article>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Article>> DeleteArticle(int articleId)
        {
            try
            {
                var model = await _db.Articles.FindAsync(articleId);
                if (model == null)
                {
                    return new ResponseBase<Article>()
                    {
                        Message = "مقاله موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.Articles.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Article>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Article>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Article>> GetArticle(int articleId)
        {
            try
            {
                var model = await _db.Articles.FindAsync(articleId);
                if (model == null)
                {
                    return new ResponseBase<Article>()
                    {
                        Message = "مقاله موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Article>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Article>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Article>>> GetArtistArticle(string userId)
        {
            try
            {
                var model = await _db.Articles.Where(i => i.PeopleId == userId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Article>>()
                    {
                        Message = "مقاله موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Article>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Article>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Article>>> GetAllArticle()
        {
            try
            {
                var model = await _db.Articles.ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Article>>()
                    {
                        Message = "مقاله موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Article>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Article>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
