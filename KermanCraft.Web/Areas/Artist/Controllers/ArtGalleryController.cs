using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.ArtGallery;
using KermanCraft.Application.ViewModels.Gallery;
using KermanCraft.Application.ViewModels.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    public class ArtGalleryController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IImageService _imageService;
        private readonly IGalleryService _galleryService;
        private readonly IArtistService _artistService;

        public ArtGalleryController(IToastNotification toastNotification, 
            IImageService imageService, 
            IGalleryService galleryService,
            IArtistService artistService)
        {
            _toastNotification = toastNotification;
            _imageService = imageService;
            _galleryService = galleryService;
            _artistService = artistService;
        }


        [HttpGet]
        public IActionResult AddGallery()
        {

            return View();
        }

        [HttpPost]
       //// [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddGallery(GalleryViewModel gallery)
        {
            if (!ModelState.IsValid)
            {
                return View(gallery);
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            gallery.PeopleId = userId;
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

            return RedirectToAction("GalleryList", "ArtGallery", new { Area = "Artist" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGallery(int id)
        {
            var gallery = await _galleryService.GetGallery(id);
            if (!gallery.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(gallery.Message);
                return RedirectToAction("GalleryList", "ArtGallery",new { Area = "Artist" });
            }


            _toastNotification.AddSuccessToastMessage(gallery.Message);
            return View(gallery.Entity);
        }

        [HttpPost]
       //// [AutoValidateAntiforgeryToken]
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
                        return RedirectToAction("UpdateGallery", "ArtGallery", new { Area = "Artist", id = gallery.GalleryId });
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
            return RedirectToAction("GalleryList", "ArtGallery", new { Area = "Artist" });

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
       //// [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddGalleryImage(ImageViewModel img)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any())
                return RedirectToAction("GetGalleryImages", "ArtGallery", new {Area = "Artist", galleryId = img.GalleryFkId});
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
            return RedirectToAction("GetGalleryImages", "ArtGallery", new { Area = "Artist", galleryImage = img.GalleryFkId });
        }

        [HttpPost]
       //// [AutoValidateAntiforgeryToken]
        public async Task DeleteGaImage(int imgId)
        {
            await _imageService.DeleteImage(imgId);
        }

        [HttpPost]
       //// [AutoValidateAntiforgeryToken]
        public async Task DeleteGallery(int id)
        {
            var response = await _galleryService.DeleteGallery(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> GalleryList(string userId)
        {
            var user = await _artistService.GetArtistUserById(userId);

            var response = await _galleryService.GetArtistGallery(userId);
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
            return RedirectToAction("index", "Index", new { Area = "Artist" });
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
