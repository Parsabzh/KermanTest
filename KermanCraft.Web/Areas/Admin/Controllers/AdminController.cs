using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Account;
using KermanCraft.Application.ViewModels.Admin;
using KermanCraft.Domain.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;
using LoginViewModel = KermanCraft.Application.ViewModels.Sign.LoginViewModel;

namespace KermanCraft.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class AdminController : Controller
    {
        private readonly IToastNotification _nToastNotify;
        private readonly SignInManager<People> _signInManager;
        private readonly IAdminService _adminService;
        private readonly IArtistService _artistService;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public AdminController(IToastNotification nToastNotify,
            SignInManager<People> signInManager,
            IAdminService adminService, IMapper mapper, IAccountService accountService, IArtistService artistService, ICustomerService customerService, ICompanyService companyService, IProductService productService)
        {
            _nToastNotify = nToastNotify;
            _signInManager = signInManager;
            _adminService = adminService;
            _mapper = mapper;
            _accountService = accountService;
            _artistService = artistService;
            _customerService = customerService;
            _companyService = companyService;
            _productService = productService;
        }
        [HttpGet]
        //[Route("/Admin/Index")]
        public async Task<IActionResult> Index()
        {
            var artist = await _artistService.ArtistUserList();
            var product = await _productService.GetProductsList();
            var customer = await _customerService.CustomerList();
            var company = await _companyService.GetAllCompany();
            var count = new AdminIndex()
            {
                ArtistCount = artist.Entity.Count(),
                CompanyCount = company.Entity.Count(),
                CustomerCount = customer.Entity.Count(),
                ProductCount = product.Entity.Count()
            };
            _nToastNotify.AddInfoToastMessage("خوش امدید", new ToastrOptions()
            {
                Title = "سلام"
            });
            return View(count);
        }

        [HttpGet]
        //[Route("/Admin/AddAdminUser")]
        //[Authorize(Policy = "AddAdmin")]
        public IActionResult AddAdminUser()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = AddAdmin")]
        public async Task<IActionResult> AddAdminUser(AddAdminViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(user);
                    }
                    imageUrl = await ImageTools.Add(file, "AdminUser");
                }
                user.ImageUrl = imageUrl;
            }
            else
            {
                user.ImageUrl = "User.png";
            }

            user.UserName = user.PhoneNumber;
            var result = await _adminService.AddAdmin(user);
            if (result.IsSuccessful)
            {
                _nToastNotify.AddSuccessToastMessage(result.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return RedirectToAction("AdminUserList", "Admin", new { Area = "Admin" });
            }


            if (result.MessageList != null)
            {
                foreach (var message in result.MessageList)
                {
                    ModelState.AddModelError("", message);
                }
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }

            return View(user);

        }

        [HttpGet]
        //[Authorize(Policy = "EditAdmin")]
        public async Task<IActionResult> UpdateAdminUser(string userId)
        {
            var response = await _adminService.GetAdminUserById(userId);
            var entity = _mapper.Map<UpdateAdminViewModel>(response.Entity);
            if (response.IsSuccessful) return View(entity);
            _nToastNotify.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("AdminUserList", "Admin", new { Area = "Admin" });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = "EditArtist")]
        public async Task<IActionResult> UpdateAdminUser(UpdateAdminViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                var currentImage = await _adminService.GetAdminUserById(user.Id);
                if (currentImage.Entity.ImageUrl != null)
                {
                    ImageTools.Delete(currentImage.Entity.ImageUrl, "AdminUser");

                }
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(user);
                    }
                    imageUrl = await ImageTools.Add(file, "AdminUser");
                }
                user.ImageUrl = imageUrl;
            }

            user.UserName = user.PhoneNumber;
            var response = await _adminService.UpdateAdmin(user);

            switch (response.IsSuccessful)
            {
                case true:
                    _nToastNotify.AddSuccessToastMessage(response.Message, new ToastrOptions()
                    {
                        Title = "پیام"

                    });
                    return RedirectToAction("AdminUserList", "Admin", new { Area = "Admin" });
                case false when response.MessageList != null:
                {
                    foreach (var message in response.MessageList)
                    {
                        _nToastNotify.AddAlertToastMessage(message, new ToastrOptions()
                        {
                            Title = "پیام اخطار"
                        });
                    }

                    return View(user);
                }
                case false when response.Message != null:
                {

                    _nToastNotify.AddAlertToastMessage(response.Message, new ToastrOptions()
                    {
                        Title = "پیام اخطار"
                    });
                    return View(user);
                }
            }

            _nToastNotify.AddAlertToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام اخطار"
            });
            return View(user);

        }

        [HttpPost]
        //[ant]
        //[Authorize(Policy = "DeleteAdmin")]
        public async Task DeleteAdminUser(string id)
        {
            await _adminService.Delete(id);
        }

        [HttpGet]
        //[Authorize(Policy = "AdminList")]
        public async Task<IActionResult> AdminUserList()
        {
            var result = await _adminService.UserList();
            return View(result.Entity);
        }

        [HttpGet]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken(Order = 2000)]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        [IgnoreAntiforgeryToken(Order=2000)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var model = await _adminService.IsInRole(loginViewModel.UserName);
            if (!model.Entity)
            {
                ModelState.AddModelError("", model.Message);
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password,
                loginViewModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin", new { Area = "Admin" });
            }
            ModelState.AddModelError("", "شماره موبایل یا رمزعبور اشتباه است.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
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
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel pass)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _accountService.ChangeArtistPassword(pass);
            if (response.IsSuccessful)
                return RedirectToAction("Index", "Admin", new { Area = "Admin", userId = pass.UserId });
            foreach (var error in response.MessageList)
            {
                ModelState.AddModelError("", error);
            }
            return View();

        }
    }
}
