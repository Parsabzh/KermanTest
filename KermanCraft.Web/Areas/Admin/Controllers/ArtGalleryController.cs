using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.ArtGallery;
using KermanCraft.Application.ViewModels.Gallery;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
  
    public class ArtGalleryController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IImageService _imageService;
        private readonly IArtGalleryService _artGalleryService;
        private readonly IGalleryService _galleryService;
        private readonly IArtistService _artistService;

        public ArtGalleryController(IToastNotification toastNotification,
            IImageService imageService,
            IArtGalleryService artGalleryService, 
            IGalleryService galleryService, 
            IArtistService artistService)
        {
            _toastNotification = toastNotification;
            _imageService = imageService;
            _artGalleryService = artGalleryService;
            _galleryService = galleryService;
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult AddArtGallery()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddArtGallery(ArtGalleryViewModel artGallery)
        {
            if (!ModelState.IsValid)
            {
                return View(artGallery);
            }

            var response = await _artGalleryService.AddArtGallery(artGallery);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                return View(artGallery);
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(artGallery);
                    }

                    await _imageService.AddImage(new ImageViewModel()
                    {
                        ImageUrl = await ImageTools.Add(file, "ArtGallery"),
                        ArtGalleryFkId = response.Entity.ArtGalleryId,
                    });
                }

            }
            else
            {
                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = "artGallery.png",
                    ArtGalleryFkId = response.Entity.ArtGalleryId,
                });
            }

            return View(artGallery);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArtGallery(int id)
        {
            var artGallery = await _artGalleryService.GetArtGallery(id);
            if (!artGallery.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(artGallery.Message);
                return RedirectToAction("ArtGalleryList", "ArtGallery");
            }

            var image = await _imageService.GetArtGalleryImage(id);

            _toastNotification.AddSuccessToastMessage(artGallery.Message);
            return View(new ArtGalleryViewModel()
            {
                Description = artGallery.Entity.Description,
                Name = artGallery.Entity.Name,
                ArtGalleryId = artGallery.Entity.ArtGalleryId,
                ImageViewModels = image.Entity.ToList()
            });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateArtGallery(ArtGalleryViewModel artGallery)
        {
            if (!ModelState.IsValid)
            {
                return View(artGallery);
            }
            var response = await _artGalleryService.UpdateArtGallery(artGallery);
            if (!response.IsSuccessful)
            {

                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(artGallery);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("ArtGalleryList", "ArtGallery", new { Area = "Admin" });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddImage(int artGalleryId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateArtGallery", "ArtGallery", new { Area = "Admin", id = artGalleryId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateArtGallery", "ArtGallery", new { Area = "Admin", id = artGalleryId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "ArtGallery")

                });

            }
            return RedirectToAction("UpdateArtGallery", "ArtGallery", new { Area = "Admin", id = artGalleryId });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteImage(int id)
        {
            await _imageService.DeleteImage(id);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteArtGallery(int id)
        {
            var response = await _artGalleryService.DeleteArtGallery(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> ArtGalleryList()
        {
            var response = await _artGalleryService.GetAllArtGallery();
            if (response.IsSuccessful)
            {
                _toastNotification.AddSuccessToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return View(response.Entity);
            }

            _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("index", "Admin", new { Area = "Admin" });


        }

        [HttpGet]
        public IActionResult AddGallery()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddGallery(GalleryViewModel gallery)
        {
            if (!ModelState.IsValid)
            {
                return View(gallery);
            }

            var user = _artistService.GetArtistUserByMobile(gallery.PhoneNumber);
            gallery.PeopleId = user.Entity.Id;
            var response = await _galleryService.AddGallery(gallery);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                return View(gallery);
            }

            string imageUrl = null;
            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(gallery);
                    }
                    imageUrl = await ImageTools.Add(file, "Gallery");
                }

                gallery.ImageUrl = imageUrl;
            }
            else
            {
                gallery.ImageUrl = "gallery.png";
            }

            return RedirectToAction("GalleryList", "ArtGallery", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGallery(int id)
        {
            var gallery = await _galleryService.GetGallery(id);
            if (!gallery.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(gallery.Message);
                return RedirectToAction("GalleryList", "ArtGallery");
            }


            _toastNotification.AddSuccessToastMessage(gallery.Message);
            return View(gallery.Entity);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateGallery(GalleryViewModel gallery)
        {
            if (!ModelState.IsValid)
            {
                return View(gallery);
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    var currentImage = await _galleryService.GetGallery(gallery.GalleryId);
                    if (currentImage.Entity.ImageUrl != null)
                    {
                        ImageTools.Delete(currentImage.Entity.ImageUrl, "Gallery");
                    }
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return RedirectToAction("UpdateGallery", "ArtGallery", new { Area = "Admin", id = gallery.GalleryId });
                    }
                    gallery.ImageUrl = await ImageTools.Add(file, "Gallery");
                }
            }
            var response = await _galleryService.UpdateGallery(gallery);
            if (!response.IsSuccessful)
            {

                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(gallery);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("GalleryList", "ArtGallery", new { Area = "Admin" });

        }

        [HttpGet]
        public IActionResult AddGalleryImage(int galleryId)
        {

            return View(new ImageViewModel()
            {
                GalleryFkId = galleryId
            });
        }
        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddGalleryImage(ImageViewModel img)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any())
                return RedirectToAction("GetGalleryImages", "ArtGallery", new {Area = "Admin", galleryId = img.GalleryFkId});
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return View(img);
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "Gallery"),
                    Description = img.Description,
                    GalleryFkId = img.GalleryFkId

                });

            }
            return RedirectToAction("GetGalleryImages", "ArtGallery", new { Area = "Admin", galleryImage = img.GalleryFkId });
        }

        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task DeleteGaImage(int imgId)
        //{
        //    await _imageService.DeleteImage(imgId);
        //}

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteGallery(int id)
        {
            var response = await _galleryService.DeleteGallery(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> GalleryList()
        {
            var response = await _galleryService.GetAllGallery();
            if (response.IsSuccessful)
            {
                _toastNotification.AddSuccessToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return View(response.Entity);
            }

            _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("index", "Admin", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryImages(int galleryId)
        {
            var response = await _imageService.GetGalleryImage(galleryId);
            
            return View(new ImageListViewModel(){Id = galleryId,
                ImageViewModels = response.Entity.ToList()});
        }

    }
}
