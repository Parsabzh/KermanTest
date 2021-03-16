using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly KermanCraftDb _db;
        private readonly UserManager<People> _userManager;
        private readonly IMapper _mapper;

        public EventRepository(KermanCraftDb db, IMapper mapper, UserManager<People> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseBase<Event>> AddEvent(Event eve)
        {
            try
            {
                await _db.Events.AddAsync(eve);
                await _db.SaveChangesAsync();
                return new ResponseBase<Event>()
                {
                    Entity = eve,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Event>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Event>> UpdateEvent(Event eve)
        {
            try
            {
                var model = await _db.Events.FindAsync(eve.EventId);
                if (model == null)
                {
                    return new ResponseBase<Event>()
                    {
                        Message = "رویداد موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(eve, model);
                _db.Events.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Event>()
                {
                    Entity = eve,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Event>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Event>> DeleteEvent(int eveId)
        {
            try
            {
                var model = await _db.Events.FindAsync(eveId);
                if (model == null)
                {
                    return new ResponseBase<Event>()
                    {
                        Message = "رویداد موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.Events.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Event>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Event>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Event>> GetEvent(int eveId)
        {
            try
            {
                var model = await _db.Events.FindAsync(eveId);
                if (model == null)
                {
                    return new ResponseBase<Event>()
                    {
                        Message = "رویداد موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Event>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Event>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Event>>> GetAllEvent()
        {
            try
            {
                var model = await _db.Events.ToListAsync();
                if (model == null)
                {
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = "رویداد موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Event>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Event>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Event>>> GetArtistEvent(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                }
                var user = await _userManager.FindByIdAsync(userId);
                var isInRole = await _userManager.IsInRoleAsync(user, "Artist");
                if (!isInRole)
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                var model = await _db.Events.Where(i => i.PeopleFkId == userId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = "رویداد موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Event>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Event>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Event>>> GetCustomerEvent(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                }
                var user = await _userManager.FindByIdAsync(userId);
                var isInRole = await _userManager.IsInRoleAsync(user, "Customer");
                if (!isInRole)
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                var model = await _db.Events.Where(i => i.PeopleFkId == userId).ToListAsync();
                if (!model.Any()) 
                {
                    return new ResponseBase<IEnumerable<Event>>()
                    {
                        Message = "رویداد موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Event>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Event>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
