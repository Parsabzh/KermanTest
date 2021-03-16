using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.Models.Product;

using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class EventMemberRepository : IEventMemberRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public EventMemberRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<EventMember>> AddEventMember(EventMember eventMember)
        {
            try
            {
                await _db.EventMembers.AddAsync(eventMember);
                await _db.SaveChangesAsync();
                return new ResponseBase<EventMember>()
                {
                    Entity = eventMember,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<EventMember>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
  

    public async Task<ResponseBase<EventMember>> UpdateEventMember(EventMember eventMember)
    {
        try
        {
            var model = await _db.EventMembers.FindAsync(eventMember.EventMemberId);
            if (model == null)
            {
                return new ResponseBase<EventMember>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _mapper.Map(eventMember, model);
            _db.EventMembers.Update(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<EventMember>()
            {
                Entity = eventMember,
                IsSuccessful = true,
                Message = MessageResource.UpdateSuccess
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<EventMember>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<EventMember>> DeleteEventMember(int eventMemberId)
    {
        try
        {
            var model = await _db.EventMembers.FindAsync(eventMemberId);
            if (model == null)
            {
                return new ResponseBase<EventMember>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _db.EventMembers.Remove(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<EventMember>()
            {
                Message = MessageResource.DeletSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<EventMember>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<EventMember>> GetEventMember(int eventMemberId)
    {
        try
        {
            var model = await _db.EventMembers.FindAsync(eventMemberId);
            if (model == null)
            {
                return new ResponseBase<EventMember>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<EventMember>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<EventMember>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<IEnumerable<EventMember>>> GetEventMembers(int eventId)
    {
        try
        {
            var model = await _db.EventMembers.Where(i=>i.EventId==eventId).ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<EventMember>>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<EventMember>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<EventMember>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

        public async Task<ResponseBase<IEnumerable<EventMember>>> GetAllEventMember()
    {
        try
        {
            var model = await _db.EventMembers.ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<EventMember>>()
                {
                    Message = "فرد موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<EventMember>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<EventMember>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
}
}
