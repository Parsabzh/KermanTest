using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.ArtGallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class ArtGalleryRepository:IArtGalleryRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public ArtGalleryRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<ArtGallery>> AddArtGallery(ArtGallery artGallery)
        {
            try
            {
                await _db.ArtGalleries.AddAsync(artGallery);
                await _db.SaveChangesAsync();
                return new ResponseBase<ArtGallery>()
                {
                    Entity = artGallery,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<ArtGallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<ArtGallery>> UpdateArtGallery(ArtGallery artGallery)
        {
            try
            {
                var model = await _db.ArtGalleries.FindAsync(artGallery.ArtGalleryId);
                if (model == null)
                {
                    return new ResponseBase<ArtGallery>()
                    {
                        Message = "محصول نگارخانه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(artGallery, model);
                _db.ArtGalleries.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<ArtGallery>()
                {
                    Entity = artGallery,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<ArtGallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<ArtGallery>> DeleteArtGallery(int artGalleryId)
        {
            try
            {
                var model = await _db.ArtGalleries.FindAsync(artGalleryId);
                if (model == null)
                {
                    return new ResponseBase<ArtGallery>()
                    {
                        Message = "محصول نگارخانه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.ArtGalleries.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<ArtGallery>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<ArtGallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<ArtGallery>> GetArtGallery(int artGalleryId)
        {
            try
            {
                var model = await _db.ArtGalleries.FindAsync(artGalleryId);
                if (model == null)
                {
                    return new ResponseBase<ArtGallery>()
                    {
                        Message = "محصول نگارخانه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<ArtGallery>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<ArtGallery>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<ArtGallery>>> GetAllArtGallery()
        {
            try
            {
                var model = await _db.ArtGalleries.ToListAsync();

                if (model == null)
                {
                    return new ResponseBase<IEnumerable<ArtGallery>>()
                    {
                        Message = "محصول نگارخانه موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<ArtGallery>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<ArtGallery>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
