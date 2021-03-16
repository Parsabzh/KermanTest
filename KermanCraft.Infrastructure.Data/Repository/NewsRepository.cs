using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.News;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public NewsRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<News>> AddNews(News news)
        {
            try
            {
                await _db.News.AddAsync(news);
                await _db.SaveChangesAsync();
                return new ResponseBase<News>()
                {
                    Entity = news,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<News>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<News>> UpdateNews(News news)
        {
            try
            {
                var model = await _db.News.FindAsync(news.NewsId);
                if (model == null)
                {
                    return new ResponseBase<News>()
                    {
                        Message = "اخبار موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(news, model);
                _db.News.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<News>()
                {
                    Entity = news,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<News>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<News>> DeleteNews(int newsId)
        {
            try
            {
                var model = await _db.News.FindAsync(newsId);
                if (model == null)
                {
                    return new ResponseBase<News>()
                    {
                        Message = "اخبار موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.News.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<News>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<News>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<News>> GetNews(int newsId)
        {
            try
            {
                var model = await _db.News.FindAsync(newsId);
                if (model == null)
                {
                    return new ResponseBase<News>()
                    {
                        Message = "اخبار موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<News>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<News>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<News>>> GetAllNews()
        {
            try
            {
                var model = await _db.News.ToListAsync();
                return !model.Any()
                ? new ResponseBase<IEnumerable<News>>()
                {
                    Message = "اخبار موردنظر یافت نشد.",
                    IsSuccessful = false
                }
                : new ResponseBase<IEnumerable<News>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<News>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
