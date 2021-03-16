using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Company;
using KermanCraft.Application.ViewModels.Image;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class CompanyController : Controller
    {

        private readonly IToastNotification _toastNotification;
        private readonly ICompanyService _companyService;
        private readonly IArtistService _artistService;

        public CompanyController(IToastNotification toastNotification,
            ICompanyService companyService,
            IArtistService artistService)
        {
            _toastNotification = toastNotification;
            _companyService = companyService;
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult AddCompany()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddCompany(CompanyViewModel company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }

            var user =  _artistService.GetArtistUserByMobile(company.OwnerPhoneNumber);
            if (!user.IsSuccessful)
            {
                ModelState.AddModelError("",user.Message);
                return View(company);
            }

            company.PeopleId = user.Entity.Id;
            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(company);
                    }
                    imageUrl = await ImageTools.Add(file, "Company");
                }
                company.ImageUrl = imageUrl;
            }
            else
            {
                company.ImageUrl = "User.png";
            }
            var response = await _companyService.AddCompany(company);
            if (response.IsSuccessful) return RedirectToAction("CompanyList","Company",new {Area="Admin"});
            _toastNotification.AddErrorToastMessage(response.Message);
            return View(company);


        }

        [HttpGet]
        public async Task<IActionResult> UpdateCompany(int companyId)
        {
            var company = await _companyService.GetCompany(companyId);
            if (!company.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(company.Message);
                return RedirectToAction("CompanyList", "Company");
            }

            _toastNotification.AddSuccessToastMessage(company.Message);
            return View(company.Entity);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateCompany(CompanyViewModel company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }
            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                var currentCompany = await _companyService.GetCompany(company.CompanyId);
                if (currentCompany.Entity.ImageUrl!=null)
                {
                    ImageTools.Delete(currentCompany.Entity.ImageUrl,"Company");
                }
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(company);
                    }
                    imageUrl = await ImageTools.Add(file, "Company");
                }
                company.ImageUrl = imageUrl;
            }
            var response = await _companyService.UpdateCompany(company);
            if (!response.IsSuccessful)
            {

                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(company);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("CompanyList", "Company", new { Area = "Admin" });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteCompany(int id)
        {
            var response = await _companyService.DeleteCompany(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> CompanyList()
        {
            var response = await _companyService.GetAllCompany();
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
