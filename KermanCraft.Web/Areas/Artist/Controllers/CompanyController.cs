using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]

    public class CompanyController : Controller
    {

        private readonly IToastNotification _toastNotification;
        private readonly ICompanyService _companyService;
       

        public CompanyController(IToastNotification toastNotification,
            ICompanyService companyService)
        {
            _toastNotification = toastNotification;
            _companyService = companyService;
          
        }

        [HttpGet]
        public IActionResult AddCompany(string userId)
        {
            var company = new CompanyViewModel()
            {
                PeopleId = userId
            };
            return View(company);
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddCompany(CompanyViewModel company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }

            company.Status = false;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            company.PeopleId = userId;
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
            if (response.IsSuccessful) return RedirectToAction("CompanyList","Company",new {Area="Artist"});
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
                return RedirectToAction("CompanyList", "Company", new { Area = "Artist" });
            }

            _toastNotification.AddSuccessToastMessage(company.Message);
            return View(company.Entity);
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
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
            return RedirectToAction("CompanyList", "Company", new { Area = "Artist" });

        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task DeleteCompany(int id)
        {
            var response = await _companyService.DeleteCompany(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> CompanyList()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _companyService.GetUserCompany(userId);
            if (response.IsSuccessful)
            {
                _toastNotification.AddSuccessToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
               
            }

            _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            //return RedirectToAction("index", "Index", new { Area = "Artist" });
            return View(response.Entity);

        }

    }
}
