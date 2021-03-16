using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Article;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Application.ViewModels.News;
using KermanCraft.Application.ViewModels.Window;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class WindowNewsController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IWindowService _windowService;
        private readonly IImageService _imageService;
        private readonly INewsService _newsService;
        private readonly IArticleService _articleService;
        private readonly IArtistService _artistService;
        private readonly IWebHostEnvironment _eve;

        public WindowNewsController(IToastNotification toastNotification,
            IWindowService windowService,
            IImageService imageService,
            INewsService newsService, IArticleService articleService, IArtistService artistService, IWebHostEnvironment eve)
        {
            _toastNotification = toastNotification;
            _windowService = windowService;
            _imageService = imageService;
            _newsService = newsService;
            _articleService = articleService;
            _artistService = artistService;
            _eve = eve;
        }

        [HttpGet]
        //[Route("/Admin/AddWindow")]

        public IActionResult AddWindow()
        {

            return View();
        }

        [HttpPost]
        //[Route("/Admin/Admin/AddWindow")]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddWindow(WindowViewModel win)
        {
            if (!ModelState.IsValid)
            {
                return View(win);
            }
            win.Date = DateTime.Now;
            
            var response = await _windowService.AddWindow(win);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                return View(win);
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(win);
                    }
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    var fileName = Guid.NewGuid() + extension;
                    //for the file due to security reasons.
                    var path = Path.Combine(_eve.WebRootPath, "images/Window");
                    var filePath = Path.Combine(path, fileName);
                    await using var bits = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(bits);
                    await _imageService.AddImage(new ImageViewModel()
                    {
                        ImageUrl = fileName,
                        WindowFkId = response.Entity.WindowId,
                    });
                }

            }
            else
            {
                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = "window.png",
                    WindowFkId = response.Entity.WindowId,
                });
            }

            return RedirectToAction("WindowList", "WindowNews", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWindow(int id)
        {
            var window = await _windowService.GetWindow(id);
            if (!window.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(window.Message);
                return RedirectToAction("WindowList", "WindowNews", new { Area = "Admin" });
            }

            var image = await _imageService.GetWindowsImage(id);

            _toastNotification.AddSuccessToastMessage(window.Message);
            return View(new UpdateWindowViewModel()
            {
                Text = window.Entity.Text,
                Subject = window.Entity.Subject,
                Date = window.Entity.Date,
                WindowId = window.Entity.WindowId,
                ImageViewModels = image.Entity.ToList(),
                VideoUrl = window.Entity.VideoUrl
            });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateWindow(UpdateWindowViewModel win)
        {
            if (!ModelState.IsValid)
            {
                return View(win);
            }
            win.Date = DateTime.Now;

            var response = await _windowService.UpdateWindow(win);
            if (!response.IsSuccessful)
            {

                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(win);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("WindowList", "WindowNews", new { Area = "Admin" });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddImage(int winId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateWindow", "WindowNews", new { Area = "Admin", id = winId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateWindow", "WindowNews", new { Area = "Admin", id = winId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "Window")

                });

            }
            return RedirectToAction("UpdateWindow", "WindowNews", new { Area = "Admin", id = winId });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteImage(int imgId)
        {
            await _imageService.DeleteImage(imgId);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteWindow(int id)
        {
            var response = await _windowService.DeleteWindow(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> WindowList()
        {
            var response = await _windowService.GetAllWindow();
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
            return View(response.Entity);


        }

        [HttpGet]
        public IActionResult AddNews()
        {

            return View();
        }
        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddNews(NewsViewModel news)
        {
            if (!ModelState.IsValid)
            {
                return View(news);
            }

            news.Date = DateTime.Now;
            var response = await _newsService.AddNews(news);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                return View(news);
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(news);
                    }

                    await _imageService.AddImage(new ImageViewModel()
                    {
                        ImageUrl = await ImageTools.Add(file, "News"),
                        NewsFkId = response.Entity.NewsId
                    });
                }

            }
            else
            {
                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = "news.png",
                    NewsFkId = response.Entity.NewsId,
                });
            }

            return RedirectToAction("NewsList", "WindowNews", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateNews(int id)
        {
            var news = await _newsService.GetNews(id);
            if (!news.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(news.Message);
                return RedirectToAction("NewsList", "WindowNews");
            }

            var image = await _imageService.GetNewsImage(id);

            _toastNotification.AddSuccessToastMessage(news.Message);
            return View(new UpdateNewsViewModel()
            {
                Text = news.Entity.Text,
                Subject = news.Entity.Subject,
                Date = news.Entity.Date,
                NewsId = news.Entity.NewsId,
                ImageViewModels = image.Entity.ToList()
            });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateNews(UpdateNewsViewModel news)
        {
            if (!ModelState.IsValid)
            {
                return View(news);
            }
            news.Date = DateTime.Now;

            var response = await _newsService.UpdateNews(news);
            if (!response.IsSuccessful)
            {

                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(news);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("NewsList", "WindowNews", new { Area = "Admin" });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddNewsImage(int newsId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateNews", "WindowNews", new { Area = "Admin", id = newsId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateNews", "WindowNews", new { Area = "Admin", id = newsId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "News")

                });

            }
            return RedirectToAction("UpdateNews", "WindowNews", new { Area = "Admin", id = newsId });
        }



        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteNews(int id)
        {
            var response = await _newsService.DeleteNews(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> NewsList()
        {
            var response = await _newsService.GetAllNews();
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
            return View(response.Entity);


        }


        [HttpGet]
        public IActionResult AddArticle()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddArticle(ArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            var user = _artistService.GetArtistUserByMobile(article.PhoneNumber);
            if (!user.IsSuccessful)
            {
                ModelState.AddModelError("", user.Message);
                return View(article);
            }
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
            article.Date = DateTime.Now.Date.ToString("d");
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

            return RedirectToAction("ArticleList", "WindowNews", new { Area = "Admin" });
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
        //[AutoValidateAntiforgeryToken]
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
            return RedirectToAction("ArticleList", "WindowNews", new { Area = "Admin" });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddArticleImage(int articleId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateArticle", "WindowNews", new { Area = "Admin", id = articleId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateArticle", "WindowNews", new { Area = "Admin", id = articleId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "Article")

                });

            }
            return RedirectToAction("UpdateArticle", "WindowNews", new { Area = "Admin", id = articleId });
        }



        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteArticle(int id)
        {
            var response = await _articleService.DeleteArticle(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> ArticleList()
        {
            var response = await _articleService.GetAllArticle();
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
            return View(response.Entity);


        }
    }
}
