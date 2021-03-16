using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Dto.Payment;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Package;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ZarinPal.Class;


namespace KermanCraft.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]

    public class PackageController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IPackageService _packageService;
        private readonly IPeoplePackageService _peoplePackageService;
        private readonly IArtistService _artistService;
        private readonly Payment _payment;
        public PackageController(IToastNotification toastNotification,
            IPackageService packageService, 
            IPeoplePackageService peoplePackageService, IArtistService artistService)
        {
            var expose = new Expose();
            _toastNotification = toastNotification;
            _packageService = packageService;
            _peoplePackageService = peoplePackageService;
            _artistService = artistService;
            _payment = expose.CreatePayment();
        }
        [HttpGet]
        public IActionResult AddPackage()
        {

            return View();
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddPackage(PackageViewModel package)
        {
            if (!ModelState.IsValid)
            {
                return View(package);
            }
            var response = await _packageService.AddPackage(package);
            if (response.IsSuccessful) return RedirectToAction("PackageList", "Package", new { Area = "Artist" });
            _toastNotification.AddErrorToastMessage(response.Message);
            return View(package);

        }

        [HttpGet]
        public async Task<IActionResult> UpdatePackage(int id)
        {
            var package = await _packageService.GetPackage(id);
            if (!package.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(package.Message);
                return RedirectToAction("PackageList", "Package");
            }

            _toastNotification.AddSuccessToastMessage(package.Message);
            return View(package.Entity);
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdatePackage(PackageViewModel package)
        {
            if (!ModelState.IsValid)
            {
                return View(package);
            }

            var response = await _packageService.UpdatePackage(package);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(package);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("PackageList", "Package", new { Area = "Artist" });

        }
        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task DeletePackage(int id)
        {
            var response = await _packageService.DeletePackage(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> PackageList()
        {
            var response = await _packageService.GetAllPackage();
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

       
        public async Task<IActionResult> AddPeopleToPackage( int packageId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var package = await _packageService.GetPackage(packageId);
            if (!package.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(package.Message,new ToastrOptions()
                {
                    Title = "پیام"
                });
                return RedirectToAction("PackageList", "Package", new {Area = "Artist", userId = userId});
            }
            var vm = new PeoplePackageViewModel()
            {
                PackageId =packageId,
                PeopleId = userId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(package.Entity.Period),
                Status = true

            };
            var response = await _peoplePackageService.AddPeoplePackage(vm);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(package.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return RedirectToAction("PackageList", "Package", new { Area = "Artist", userId = userId });
            }
            _toastNotification.AddSuccessToastMessage(package.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("Index", "Index", new { Area = "Artist", userId = userId });
        }

        public async Task<IActionResult> PackagePayment(int packageId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _artistService.GetArtistUserById(userId);
            var peoplePackage = new PeoplePackageViewModel()
            {
                PackageId = packageId,
                PeopleId = userId,
                IsPaid = false
            };
            var package = await _packageService.GetPackage(packageId);
            var res = await _payment.Request(new DtoRequest()
            {
                Amount = (int)package.Entity.Price,
                Email = $"{user.Entity.Email}",
                Description = $"پرداخت فاکتور شماره {package.Entity.Subject} {package.Entity.PackageId}",
                Mobile = $"{user.Entity.PhoneNumber}",
                CallbackUrl = $"https://Kermancrafts.com/RegisterEventAndCourse/PaymentResult/" + package.Entity.PackageId,
                MerchantId = "689ee14c-e228-11e6-b200-000c295eb8fc"

            }, Payment.Mode.zarinpal);

            if (res.Status == 100)
            {
                return Redirect("https://zarinpal.com/pg/StartPay/" + res.Authority);
            }
            else
            {
                return BadRequest();
            }

        }

        public async Task<IActionResult> PackagePaymentResult(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"].ToString();
                var package = await _packageService.GetPackage(id);
                var res = await _payment.Verification(new DtoVerification()
                {
                    Amount = (int)package.Entity.Price,
                    Authority = authority,
                    MerchantId = "689ee14c-e228-11e6-b200-000c295eb8fc"
                }, Payment.Mode.zarinpal);
                if (res.Status == 100)
                {
                    var response = await _peoplePackageService.AddPeoplePackage(new PeoplePackageViewModel()
                    {
                        StartDate = DateTime.Now,
                        PackageId = id,
                        PeopleId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                        IsPaid = true,
                        ReferenceId = res.RefId.ToString("N")
                    });
                    ViewBag.code = res.RefId;
                    return View();
                }

            }
            return NotFound();
        }
    }
}
