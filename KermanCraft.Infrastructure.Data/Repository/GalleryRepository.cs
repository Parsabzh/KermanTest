using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Gallery;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class GalleryRepository:IGalleryRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;
        private readonly UserManager<People> _userManager;

        public GalleryRepository(KermanCraftDb db, IMapper mapper, UserManager<People> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<ResponseBase<Gallery>> AddGallery(Gallery gallery)
        {
            try
            {
                if (await _db.Galleries.AnyAsync(i=>i.Name==gallery.Name))
                {
                    return new ResponseBase<Gallery>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.AlreadyExist,"گالری")
                    };
                }
                await _db.Galleries.AddAsync(gallery);
                await _db.SaveChangesAsync();
                return new ResponseBase<Gallery>()
                {
                    Entity = gallery,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Gallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Gallery>> UpdateGallery(Gallery gallery)
        {
            try
            {
                var model = await _db.Galleries.FindAsync(gallery.GalleryId);
                if (model == null)
                {
                    return new ResponseBase<Gallery>()
                    {
                        Message = string.Format(MessageResource.NotFound,"گالری"),
                        IsSuccessful = false
                    };
                }

                _mapper.Map(gallery, model);
                _db.Galleries.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Gallery>()
                {
                    Entity = gallery,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Gallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Gallery>> DeleteGallery(int galleryId)
        {
            try
            {
                var model = await _db.Galleries.FindAsync(galleryId);
                if (model == null)
                {
                    return new ResponseBase<Gallery>()
                    {
                        Message = string.Format(MessageResource.NotFound, "گالری"),
                        IsSuccessful = false
                    };
                }

                _db.Galleries.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Gallery>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Gallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Gallery>> GetGallery(int galleryId)
        {
            try
            {
                var model = await _db.Galleries.FindAsync(galleryId);
                if (model == null)
                {
                    return new ResponseBase<Gallery>()
                    {
                        Message = string.Format(MessageResource.NotFound, "گالری"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Gallery>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Gallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Gallery>>> GetAllGallery()
        {
            try
            {
                var model = await _db.Galleries.ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Gallery>>()
                    {
                        Message = string.Format(MessageResource.NotFound, "گالری"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Gallery>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Gallery>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Gallery>>> GetArtistGallery(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseBase<IEnumerable<Gallery>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                }
                var user = await _userManager.FindByIdAsync(userId);
                var isInRole = await _userManager.IsInRoleAsync(user, "Artist");
                if (!isInRole)
                    return new ResponseBase<IEnumerable<Gallery>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User),
                        IsSuccessful = false
                    };
                var model = await _db.Galleries.Where(i=>i.PeopleId==userId).ToListAsync();

                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Gallery>>()
                    {
                        Message = string.Format(MessageResource.NotFound, "گالری"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Gallery>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Gallery>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

    }
}
