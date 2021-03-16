using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Account;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Domain.Models.People;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]

    public class IndexController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<People> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IProductService _productService;
        private readonly ICourseService _courseService;
        private readonly IEventService _eventService;
        private readonly ICompanyService _companyService;
        private readonly IPeoplePackageService _peoplePackageService;
        private readonly IPackageService _packageService;

        public IndexController(IArtistService artistService,
            IToastNotification toastNotification,
            IMapper mapper, SignInManager<People> signInManager,
            IAccountService accountService, IProductService productService, ICourseService courseService, IEventService eventService, ICompanyService companyService, IPeoplePackageService peoplePackageService, IPackageService packageService)
        {
            _artistService = artistService;
            _toastNotification = toastNotification;
            _mapper = mapper;
            _signInManager = signInManager;
            _accountService = accountService;
            _productService = productService;
            _courseService = courseService;
            _eventService = eventService;
            _companyService = companyService;
            _peoplePackageService = peoplePackageService;
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = (await _productService.GetArtistProduct(userId)).Entity.Count();
            var course = (await _courseService.GetUserCourse(userId)).Entity.Count();
            var company = (await _companyService.GetUserCompany(userId)).Entity.Count();
            var eve = (await _eventService.GetArtistEvent(userId)).Entity.Count();
            var peoplePackage = await _peoplePackageService.GetPeoplePackage(userId);

            var vm = new ArtistIndexViewModel()
            {
                Company = company,
                Course = course,
                Product = product,
                Event = eve

            };
            if (peoplePackage.IsSuccessful)
            {
                var package = await _packageService.GetPackage(peoplePackage.Entity.PackageId);
                vm.PackageName = package.Entity.Subject;
            }
            else
            {
                if (peoplePackage.Entity!=null)
                {
                    if (peoplePackage.Entity.EndDate.Date == DateTime.Today.Date)
                    {
                        peoplePackage.Entity.Status = false;
                        await _peoplePackageService.DeletePeoplePackage(peoplePackage.Entity.PeoplePackageId);
                        vm.Package = false;
                        return View(vm);
                    }
                }

                vm.Package = false;
                return View(vm);

            }
            vm.Package = true;
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArtist(string userId)
        {
            var response = await _artistService.GetArtistUserById(userId);
            if (response.IsSuccessful)
            {

                var entity = _mapper.Map<UpdateArtistViewModel>(response.Entity);
                return View(entity);
            }
            _toastNotification.AddErrorToastMessage(response.Message);
            return RedirectToAction("Index", "Index", new { Area = "Artist", userId = userId });

        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateArtist(UpdateArtistViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                var currentImage = await _artistService.GetArtistUserById(user.Id);
                if (currentImage.Entity.ImageUrl != null)
                {
                    ImageTools.Delete(currentImage.Entity.ImageUrl, "ArtistUser");
                }
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(user);
                    }
                    imageUrl = await ImageTools.Add(file, "ArtistUser");
                }
                user.ImageUrl = imageUrl;
            }

            user.UserName = user.PhoneNumber;
            var response = await _artistService.UpdateArtist(user);

            switch (response.IsSuccessful)
            {
                case true:
                    _toastNotification.AddSuccessToastMessage(response.Message, new ToastrOptions()
                    {
                        Title = "پیام"

                    });
                    return RedirectToAction("ArtistUserList", "Artist", new { Area = "Admin" });
                case false when response.MessageList != null:
                    {
                        foreach (var message in response.MessageList)
                        {
                            _toastNotification.AddAlertToastMessage(message, new ToastrOptions()
                            {
                                Title = "پیام اخطار"
                            });
                        }

                        return View(user);
                    }

                case false when response.Message != null:
                    {

                        _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
                        {
                            Title = "پیام اخطار"
                        });
                        return View(user);
                    }
            }

            _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام اخطار"
            });
            return View(user);
        }


        public async Task<IActionResult> LogoutArtist()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult ChangePassword(string userId)
        {

            return View(new ChangePasswordViewModel()
            {
                UserId = userId
            });
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel pass)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _accountService.ChangeArtistPassword(pass);
            if (response.IsSuccessful)
                return RedirectToAction("Index", "Index", new { Area = "Artist", userId = pass.UserId });
            foreach (var error in response.MessageList)
            {
                ModelState.AddModelError("", error);
            }
            return View();

        }

      

    }
}
