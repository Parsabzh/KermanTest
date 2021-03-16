using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Article;
using KermanCraft.Application.ViewModels.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]

    public class WindowNewsController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IImageService _imageService;
        private readonly IArticleService _articleService;

        public WindowNewsController(IToastNotification toastNotification,
            IImageService imageService, 
             IArticleService articleService)
        {
            _toastNotification = toastNotification;
            _imageService = imageService;
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult AddArticle(string userId)
        {
            var article = new ArticleViewModel()
            {
                PeopleId = userId
            };
            return View(article);
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddArticle(ArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            article.PeopleId = userId;
                article.Status = false;
            article.Date = DateTime.Now.Date.ToString("d");
            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (Path.GetExtension(file.FileName) != ".pdf") continue;
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    var fileName = Guid.NewGuid() + extension;
                    //for the file due to security reasons.
                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Article\\pdf", fileName);

                    await using var bits = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(bits);
                    article.PdfUrl = fileName;

                }

            }
            var response = await _articleService.AddArticle(article);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                return View(article);
            }
         
            //if (files.Any())
            //{
            //    foreach (var file in files)
            //    {
            //        if (Path.GetExtension(file.FileName) == ".pdf")
            //        {
            //            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            //            var fileName = Guid.NewGuid() + extension;
            //            //for the file due to security reasons.
            //            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Article\\pdf", fileName);

            //            await using var bits = new FileStream(path, FileMode.Create);
            //            await file.CopyToAsync(bits);
            //            article.PdfUrl = fileName;
            //            continue;
            //        }
            //        else if (!file.IsImage())
            //        {
            //            ModelState.AddModelError("", "فرمت فایل نامعتبر است");
            //            return View(article);
            //        }

            //        await _imageService.AddImage(new ImageViewModel()
            //        {
            //            ImageUrl = await ImageTools.Add(file, "Article"),
            //            ArticleFkId = response.Entity.ArticleId
            //        });
            //    }

            //}
            //else
            //{
            //    await _imageService.AddImage(new ImageViewModel()
            //    {
            //        ImageUrl = "article.png",
            //        ArticleFkId = response.Entity.ArticleId,
            //    });
            //}

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            var article = await _articleService.GetArticle(id);
            if (!article.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(article.Message);
                return RedirectToAction("ArticleList", "WindowNews");
            }

            var image = await _imageService.GetArticleImage(id);

            _toastNotification.AddSuccessToastMessage(article.Message);
            return View(new ArticleViewModel()
            {
                Text = article.Entity.Text,
                Subject = article.Entity.Subject,
                Date = article.Entity.Date,
                ArticleId = article.Entity.ArticleId,
                ImageViewModels = image.Entity.ToList()
            });
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateArticle(ArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }
            article.Date = DateTime.Now.Date.ToString("d");
            var response = await _articleService.UpdateArticle(article);
            if (!response.IsSuccessful)
            {

                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(article);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("ArticleList", "WindowNews", new { Area = "Artist" });

        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddArticleImage(int articleId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateArticle", "WindowNews", new { Area = "Artist", id = articleId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateArticle", "WindowNews", new { Area = "Artist", id = articleId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "Article")

                });

            }
            return RedirectToAction("UpdateArticle", "WindowNews", new { Area = "Artist", id = articleId });
        }

       

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task DeleteArticle(int id)
        {
            var response = await _articleService.DeleteArticle(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> ArticleList(string userId)
        {
            var response = await _articleService.GetArtistArticle(userId);
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
    }
}
