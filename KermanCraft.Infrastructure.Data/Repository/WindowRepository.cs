using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Window;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class WindowRepository:IWindowRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public WindowRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<Window>> AddWindow(Window win)
        {
            try
            {
                await _db.Windows.AddAsync(win);
                await _db.SaveChangesAsync();
                return new ResponseBase<Window>()
                {
                    Entity = win,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Window>()
                {
                    IsSuccessful=false,
                    Message=e.Message
                };
            }
        }

        public async Task<ResponseBase<Window>> UpdateWindow(Window win)
        {
            try
            {
                var model = await _db.Windows.FindAsync(win.WindowId);
                if (model==null)
                {
                    return new ResponseBase<Window>()
                    {
                        Message = "روزنه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(win, model);
                _db.Windows.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Window>()
                {
                    Entity = win,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Window>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Window>> DeleteWindow(int winId)
        {
            try
            {
                var model = await _db.Windows.FindAsync(winId);
                if (model == null)
                {
                    return new ResponseBase<Window>()
                    {
                        Message = "روزنه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.Windows.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Window>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Window>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Window>> GetWindow(int winId)
        {
            try
            {
                var model = await _db.Windows.FindAsync(winId);
                if (model == null)
                {
                    return new ResponseBase<Window>()
                    {
                        Message = "روزنه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Window>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Window>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
        
        public async Task<ResponseBase<IEnumerable<Window>>> GetAllWindows()
        {
            try
            {
                var model = await _db.Windows.ToListAsync();
                if (model == null)
                {
                    return new ResponseBase<IEnumerable<Window>>()
                    {
                        Message = "روزنه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Window>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Window>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
