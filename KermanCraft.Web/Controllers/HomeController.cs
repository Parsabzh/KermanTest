
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Sms;
using KermanCraft.Application.ViewModels.Account;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Application.ViewModels.Customer;
using KermanCraft.Application.ViewModels.IndexView;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Application.ViewModels.Sms;
using KermanCraft.Domain.Models.People;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using NToastNotify;

namespace KermanCraft.Web.Controllers
{
    public class HomeController : Controller
    {


        #region Ctor

        private readonly ILogger<HomeController> _logger;
        private readonly IArtistService _artistService;
        private readonly ICustomerService _customerService;
        private readonly SignInManager<People> _signInManager;
        private readonly IGalleryService _galleryService;
        private readonly IToastNotification _toastNotification;
        private readonly IArtGalleryService _artGalleryService;
        private readonly IArticleService _articleService;
        private readonly INewsService _newsService;
        private readonly IWindowService _windowService;
        private readonly ICompanyService _companyService;
        private readonly IEventService _eventService;
        private readonly ICourseService _courseService;
        private readonly IImageService _imageService;
        private readonly IProductService _productService;
        private readonly ISmsService _smsService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ICommentService _commentService;
        private readonly IProductTypeService _productTypeService;


        public HomeController(ILogger<HomeController> logger,
            IArtistService artistService,
            ICustomerService customerService,
            SignInManager<People> signInManager,
            IGalleryService galleryService,
            IToastNotification toastNotification,
            IArtGalleryService artGalleryService,
            IArticleService articleService,
            INewsService newsService,
            IWindowService windowService,
            ICompanyService companyService, IEventService eventService,
            ICourseService courseService, IImageService imageService,
            IProductService productService, ISmsService smsService,
            IMapper mapper, IAccountService accountService, ICommentService commentService, IProductTypeService productTypeService)
        {

            _logger = logger;
            _artistService = artistService;
            _customerService = customerService;
            _signInManager = signInManager;
            _galleryService = galleryService;
            _toastNotification = toastNotification;
            _artGalleryService = artGalleryService;
            _articleService = articleService;
            _newsService = newsService;
            _windowService = windowService;
            _companyService = companyService;
            _eventService = eventService;
            _courseService = courseService;
            _imageService = imageService;
            _productService = productService;
            _smsService = smsService;
            _mapper = mapper;
            _accountService = accountService;
            _commentService = commentService;
            _productTypeService = productTypeService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var artist = await _artistService.ArtistUserList();
            artist.Entity = artist.Entity.TakeLast(8);
            var artGallery = await _artGalleryService.GetAllArtGallery();
            artGallery.Entity = artGallery.Entity.TakeLast(3);
            var company = await _companyService.GetAllCompany();
            company.Entity = company.Entity.TakeLast(10);
            var window = await _windowService.GetAllWindow();
            window.Entity = window.Entity.TakeLast(3);
            var course = await _courseService.GetAllCourse();
            course.Entity = course.Entity.TakeLast(10);
            var eve = await _eventService.GetAllEvent();
            eve.Entity = eve.Entity.TakeLast(10);
            var news = await _newsService.GetAllNews();
            news.Entity = news.Entity.TakeLast(10);
            var index = new FirstIndexViewModel()
            {
                ArtGalleryViewModels = artGallery.Entity.ToList(),
                ArtistViewModels = artist.Entity.ToList(),
                CompanyViewModels = company.Entity.ToList(),
                CourseViewModels = course.Entity.ToList(),
                EventViewModels = eve.Entity.ToList(),
                NewsViewModels = news.Entity.ToList(),
                WindowViewModels = window.Entity.ToList()


            };
            return View(index);

        }

        #region Account
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var result =
                await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);
            if (result.Succeeded)
            {


                if ((await _artistService.ArtistIsInRole(login.UserName)).Entity)
                {
                    var artist = _artistService.GetArtistUserByMobile(login.UserName);
                    if (artist.Entity.Status)
                    {
                        return RedirectToAction("Index", "Index", new { Area = "Artist", userId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
                    }

                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "حساب کاربری شما غیرفعال شده است. لطفا با پشتیبانی تماس بگیرید.");
                    return View();
                }
                else if ((await _customerService.CustomerIsInRole(login.UserName)).Entity)
                {
                    var customer =
                        await _customerService.GetCustomerByPhone(login.UserName);
                    if (customer.Entity.Status)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "حساب کاربری شما غیرفعال شده است. لطفا با پشتیبانی تماس بگیرید.");
                    return View();
                }

            }

            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }
            ModelState.AddModelError(string.Empty, "شماره موبایل یا رمزعبور اشتباه می باشد.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CustomerRegister()
        {
            return View();
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CustomerRegister(AddCustomerFrontViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            var lang = CultureInfo.CurrentCulture.Name;
            customer.LanguageId = lang switch
            {
                "fa-IR" => 1,
                "en-Us" => 2,
                _ => customer.LanguageId
            };

            customer.UserName = customer.PhoneNumber;
            customer.Status = true;
            customer.ImageUrl = "User.png";
            var code = await _smsService.GetCode(customer.PhoneNumber);
            if (customer.Code != code.Entity.Code)
            {
                ModelState.AddModelError("", "کد تایید اشتباه می باشد!");
                return View(customer);
            }

            await _smsService.DeleteCode(customer.PhoneNumber);
            var response = await _customerService.AddCustomer(_mapper.Map<AddCustomerViewModel>(customer));
            if (response.IsSuccessful) return RedirectToAction("Index", "Home");
            if (response.MessageList != null)
            {
                foreach (var item in response.MessageList)
                {
                    ModelState.AddModelError("", item);
                }
                return View(customer);
            }
            ModelState.AddModelError("", response.Message);
            return View(customer);

        }

        [HttpGet]
        public IActionResult ArtistRegister()
        {
            return View();
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ArtistRegister(AddArtistFrontViewModel artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }

            artist.ImageUrl = "User.png";
            artist.UserName = artist.PhoneNumber;
            artist.Status = true;
            var code = await _smsService.GetCode(artist.PhoneNumber);
            if (artist.Code != code.Entity.Code)
            {
                ModelState.AddModelError("", "کد تایید اشتباه می باشد!");
                return View(artist);
            }

            await _smsService.DeleteCode(artist.PhoneNumber);
            var response = await _artistService.AddArtist(_mapper.Map<AddArtistViewModel>(artist));

            if (response.IsSuccessful)
            {
                var result = await _signInManager.PasswordSignInAsync(artist.UserName, artist.Password, false, false);
                return RedirectToAction("Index", "Index", new { Area = "Artist", userId = response.Entity.Id });
            }
            if (response.MessageList != null)
            {
                foreach (var item in response.MessageList)
                {
                    ModelState.AddModelError("", item);
                }
                return View(artist);
            }
            ModelState.AddModelError("", response.Message);
            return View(artist);
        }


        [HttpPost]
        public async Task SendRegistrationCode(string userPhone)
        {
            if (string.IsNullOrEmpty(userPhone))
            {

                _toastNotification.AddErrorToastMessage("لطفا شماره موبایل را وارد کنید");
                return;
            }
            var code = await _smsService.GetCode(userPhone);
            if (code.IsSuccessful)
            {
                await _smsService.DeleteCode(userPhone);
            }
            code = await _smsService.AddSms(userPhone);
            await SmsProvider.SendSms("verifyphonenumber", $"{userPhone}", code.Entity.Code.ToString());

        }

        [HttpGet]
        public IActionResult SendSms()
        {

            return View();
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SendSms(SmsViewModel sms)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var code = await _smsService.GetCode(sms.PhoneNumber);
            if (code.Entity.Code == sms.Code)
            {
                return RedirectToAction("ChangePassword", "Home", new { phoneNumber = sms.PhoneNumber });
            }
            ModelState.AddModelError("", MessageResource.CodeIsNotCorrect);
            return View(sms);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string phoneNumber)
        {
            var model = new ForgetPasswordViewModel();
            var artistUser = _artistService.GetArtistUserByMobile(phoneNumber);
            if (artistUser.IsSuccessful)
            {
                model.UserId = artistUser.Entity.Id;
                return View(model);
            }
            var customer = await _customerService.GetCustomerByPhone(phoneNumber);
            model.UserId = customer.Entity.Id;
            return View(model);
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ForgetPasswordViewModel pass)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _accountService.ResetArtistPassword(pass.UserId, pass.NewPass);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }
            //_toastNotification.AddSuccessToastMessage(response.Message,new ToastrOptions()
            //{
            //    Title = "پیام"
            //});
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> VerifyUser(string userPhone)
        {
            if (string.IsNullOrEmpty(userPhone))
            {

                _toastNotification.AddErrorToastMessage("لطفا شماره موبایل را وارد کنید");
                return RedirectToAction("SendSms", "Home");
            }
            var code = await _smsService.GetCode(userPhone);
            if (code.IsSuccessful)
            {
                await _smsService.DeleteCode(userPhone);
            }
            var artistUser = _artistService.GetArtistUserByMobile(userPhone);
            var customer = await _customerService.GetCustomerByPhone(userPhone);
            if (!artistUser.IsSuccessful && !customer.IsSuccessful)
            {
                ModelState.AddModelError("", string.Format(MessageResource.NotFound, PeopleResource.User));
                return RedirectToAction("SendSms", "Home");
            }
            code = await _smsService.AddSms(userPhone);
            await SmsProvider.SendSms("verifyphonenumber", $"{userPhone}", code.Entity.Code.ToString());
            return RedirectToAction("SendSms", "Home");
        }

        #endregion

        #region Gallery

        [HttpGet]
        public async Task<IActionResult> GetAllGallery()
        {
            var response = await _galleryService.GetAllGallery();
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetGallery(int galleryId)
        {
            var response = await _galleryService.GetGallery(galleryId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);
        }

        #endregion

        #region ArtGallery
        [HttpGet]
        public async Task<IActionResult> GetAllArtGallery()
        {
            var response = await _artGalleryService.GetAllArtGallery();
          
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }
            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetArtGallery(int artGalleryId)
        {
            var response = await _artGalleryService.GetArtGallery(artGalleryId);

            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }
            var image = await _imageService.GetArtGalleryImage(response.Entity.ArtGalleryId);
            var cm = await _commentService.GetTypeComment("artGallery", artGalleryId);
            response.Entity.CommentViewModels = cm.Entity.ToList();
            response.Entity.ImageViewModels = image.Entity.ToList();
            return View(response.Entity);
        }

        #endregion

        #region News

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var response = await _newsService.GetAllNews();
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetNews(int newsId)
        {
            var response = await _newsService.GetNews(newsId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
                return RedirectToAction("GetAllNews", "Home");
            }
            var image = await _imageService.GetNewsImage(response.Entity.NewsId);
            var cm = await _commentService.GetTypeComment("news", newsId);
            response.Entity.ImageViewModels = image.Entity.ToList();
            response.Entity.CommentViewModels = cm.Entity.ToList();
            return View(response.Entity);
        }

        #endregion

        #region Article

        [HttpGet]
        public async Task<IActionResult> GetAllArticle()
        {
            var response = await _articleService.GetAllArticle();
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetArticle(int articleId)
        {
            var response = await _articleService.GetArticle(articleId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }
            var image = await _imageService.GetArticleImage(response.Entity.ArticleId);
            response.Entity.ImageViewModels = image.Entity.ToList();
            return View(response.Entity);
        }

        [HttpGet]
        public FileResult DownloadFile(string fileName)
        {
            //var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Article\\pdf\\", fileName);
            //var path = $"~/Article/pdf/{fileName}";
            //var path = env.WebRootFileProvider.GetFileInfo($"Article/pdf/{fileName}")?.PhysicalPath;
            //Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Article\\pdf\\", fileName); ;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", fileName);

        }

        #endregion

        #region Window

        [HttpGet]
        public async Task<IActionResult> GetAllWindow(string category)
        {
            var response = await _windowService.GetAllWindowByCategory(category);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetWindow(int windowId)
        {
            var response = await _windowService.GetWindow(windowId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }
            var image = await _imageService.GetWindowsImage(response.Entity.WindowId);
            var cm = await _commentService.GetTypeComment("window", windowId);
            response.Entity.CommentViewModels = cm.Entity.ToList();
            response.Entity.ImageViewModels = image.Entity.ToList();
            return View(response.Entity);
        }
        #endregion

        #region GetByCompany

        [HttpGet]
        public async Task<IActionResult> GetCompanyByType(string type)
        {
            var response = await _companyService.GetCompanyByType(type);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany(int companyId)
        {
            var response = await _companyService.GetCompany(companyId);
            if (response.IsSuccessful) return View(response.Entity);
            _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return Redirect("/");

        }


        #endregion

        #region GetArtist

        [HttpGet]
        public async Task<IActionResult> GetAllArtist()
        {
            var response = await _artistService.ArtistUserList();
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetArtist(string userId)
        {
            var response = await _artistService.GetArtistUserById(userId);
            var product = await _productService.GetArtistProduct(userId);
            var pro = product.Entity.Where(i => i.IsForSale && i.Status);
            var gallery= await _productService.GetArtistProduct(userId);
            var gal = gallery.Entity.Where(i => !i.IsForSale && i.Status);
            response.Entity.ProductViewModels = pro.ToList();
            response.Entity.Gallery = gal.ToList();
            if (response.IsSuccessful)
                return View();
            _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return Redirect("/");

        }

        #endregion

        #region Event

        [HttpGet]
        public async Task<IActionResult> GetAllEvent(string type)
        {
            var response = await _eventService.GetAllEventByType(type);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }
            return View(response.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var response = await _eventService.GetEvent(eventId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);
        }

        #endregion

        #region Course


        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            var response = await _courseService.GetAllCourse();
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);

        }

        [HttpGet]
        public async Task<IActionResult> GetCourse(int courseId)
        {
            var response = await _courseService.GetCourse(courseId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام",
                    Rtl = true
                });
            }

            return View(response.Entity);
        }


        #endregion

        #region Shop 
        public async Task<IActionResult> GetProduct(int productId)
        {

            var response = await _productService.GetProduct(productId);
            var productList = await _productService.GetProductsListByType(response.Entity.Type);
           var pro= _mapper.Map<IEnumerable<ProductListViewModel>>(productList.Entity);
           response.Entity.ProductListViewModels = pro.ToList();
           var cm = await _commentService.GetTypeComment("product", productId);
           var image = await _imageService.GetProductImage(productId);
           response.Entity.ImageViewModels = image.Entity.ToList();
           response.Entity.CommentViewModels = cm.Entity.ToList();
            if (response.IsSuccessful)
                return View(response.Entity);
            _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> GetShop()
        {
            var response = await _productTypeService.GetAllProductType();
           

            return View(response.Entity.ToList());
        }

        public async Task<IActionResult> GetShopByType(string type)
        {
            var response = await _productService.GetProductsListByType(type);
            foreach (var productViewModel in response.Entity)
            {
                var image = await _imageService.GetProductImage(productViewModel.ProductId);
               productViewModel.ImageViewModels = image.Entity.ToList();
            }
          
            response.Entity = response.Entity.Where(i => i.Status);
            return View(response.Entity);
        }
        #endregion
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(2) });

            return Redirect(Request.Headers["Referer"].ToString());
        }

        #region Comment

        [HttpPost]
        public async Task AddComment(string text, string type, int typeId)
        {
            var comment = new CommentViewModel()
            {
                Date = DateTime.Today,
                Status = true,
                Type = type,
                PeopleId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                Description = text,
                TypeId = typeId
            };

            var response = await _commentService.AddComment(comment);

        }

        #endregion

    }
}
