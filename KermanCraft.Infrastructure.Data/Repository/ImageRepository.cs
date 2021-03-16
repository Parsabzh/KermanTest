using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Image;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class ImageRepository:IImageRepository
    {
        private readonly KermanCraftDb _db;

        public ImageRepository(KermanCraftDb db)
        {
            _db = db;
        }

        public async Task<ResponseBase<Image>> AddImage(Image img)
        {
            try
            {
                await _db.Images.AddAsync(img);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    Entity = img,
                    IsSuccessful=true,
                    Message=MessageResource.InsertSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Image>>> GetWindowsImage(int winId)
        {
            try
            {
                var model = await _db.Images.Where(i=>i.WindowFkId==winId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Image>>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Entity = model,
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Image>>> GetNewsImage(int newsId)
        {
            try
            {
                var model = await _db.Images.Where(i => i.NewsFkId == newsId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Image>>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Entity = model,
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Image>>> GetGalleryImage(int galleryId)
        {
            try
            {
                var model = await _db.Images.Where(i => i.GalleryFkId == galleryId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Image>>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Entity = model,
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Image>>> GetArtGalleryImage(int artGallery)
        {
            try
            {
                var model = await _db.Images.Where(i => i.ArtGalleryFkId == artGallery).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Image>>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Entity = model,
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Image>>> GetProductImage(int productId)
        {
            try
            {
                var model = await _db.Images.Where(i => i.ProductFkId == productId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Image>>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Entity = model,
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Image>>> GetArticleImage(int articleId)
        {
            try
            {
                var model = await _db.Images.Where(i => i.ArticleFkId == articleId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Image>>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Entity = model,
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Image>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }


        public async Task<ResponseBase<Image>> DeleteImage(int imgId)
        {
            try
            {
                var model = await _db.Images.FindAsync(imgId);
                if (model==null)
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound,PeopleResource.File)
                    };
                }

                _db.Images.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteProductImage(int productId)
        {
            try
            {

                var model = _db.Images.Where(i=>i.ProductFkId==productId);
                if (model.Any())
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteArticleImage(int articleId)
        {
            try
            {

                var model = _db.Images.Where(i => i.ArticleFkId == articleId);
                if (model.Any())
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteProductImage(int productId, string imageUrl)
        {
            try
            {
                var model = await _db.Images.SingleOrDefaultAsync(i =>
                    i.ProductFkId == productId && i.ImageUrl == imageUrl);
                if (model == null)
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteWindowsImage(int windowId)
        {
            try
            {

                var model = _db.Images.Where(i => i.WindowFkId == windowId);
                if (model.Any())
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }



        }

        public async Task<ResponseBase<Image>> DeleteWindowsImage(int windowId, string imageUrl)
        {
            try
            {
                var model = await _db.Images.SingleOrDefaultAsync(i =>
                    i.WindowFkId == windowId && i.ImageUrl == imageUrl);
                if (model == null)
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteNewsImage(int newsId)
        {
            try
            {

                var model = _db.Images.Where(i => i.NewsFkId == newsId);
                if (model.Any())
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteNewsImage(int newsId, string imageUrl)
        {
            try
            {
                var model = await _db.Images.SingleOrDefaultAsync(i =>
                    i.NewsFkId == newsId && i.ImageUrl == imageUrl);
                if (model == null)
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteGalleryImage(int galleryId)
        {

            try
            {

                var model = _db.Images.Where(i => i.GalleryFkId == galleryId);
                if (model.Any())
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteGalleryImage(int galleryId, string imageUrl)
        {
            try
            {
                var model = await _db.Images.SingleOrDefaultAsync(i =>
                    i.GalleryFkId == galleryId && i.ImageUrl == imageUrl);
                if (model == null)
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteArtGalleryImage(int artGalleryId)
        {
            try
            {

                var model = _db.Images.Where(i => i.ArtGalleryFkId == artGalleryId);
                if (model.Any())
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Image>> DeleteArtGalleryImage(int artGalleryId, string imageUrl)
        {
            try
            {
                var model = await _db.Images.SingleOrDefaultAsync(i =>
                    i.ArtGalleryFkId == artGalleryId && i.ImageUrl == imageUrl);
                if (model == null)
                {
                    return new ResponseBase<Image>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.File)
                    };
                }

                _db.Images.RemoveRange(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Image>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Image>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }
    }
}
