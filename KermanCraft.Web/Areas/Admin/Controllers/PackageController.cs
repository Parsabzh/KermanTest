using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Package;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class PackageController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IPackageService _packageService;

        public PackageController(IToastNotification toastNotification, IPackageService packageService)
        {
            _toastNotification = toastNotification;
            _packageService = packageService;
        }
        [HttpGet]
        public IActionResult AddPackage()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddPackage(PackageViewModel package)
        {
            if (!ModelState.IsValid)
            {
                return View(package);
            }
            var response = await _packageService.AddPackage(package);
            if (response.IsSuccessful) return RedirectToAction("PackageList", "Package", new { Area = "Admin" });
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
        //[AutoValidateAntiforgeryToken]
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
            return RedirectToAction("PackageList", "Package", new { Area = "Admin" });

        }
        [HttpPost]
        //[AutoValidateAntiforgeryToken]
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
            return RedirectToAction("index", "Admin", new { Area = "Admin" });


        }
    }
}
