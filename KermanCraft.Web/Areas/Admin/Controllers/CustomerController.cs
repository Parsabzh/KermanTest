using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Customer;
using KermanCraft.Domain.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class CustomerController : Controller
    {
        private readonly IToastNotification _nToastNotify;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(IToastNotification nToastNotify, ICustomerService customerService, IMapper mapper)
        {
            _nToastNotify = nToastNotify;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Policy = "AddCustomer")]
        public IActionResult AddCustomer()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = "AddCustomer")]
        public async Task<IActionResult> AddCustomer(AddCustomerViewModel user)
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
                    imageUrl = await ImageTools.Add(file, "Customer");
                }
                user.ImageUrl = imageUrl;
            }
            else
            {
                user.ImageUrl = "User.png";
            }

            user.UserName = user.PhoneNumber;
            var result = await _customerService.AddCustomer(user);
            if (result.IsSuccessful)
            {
                _nToastNotify.AddSuccessToastMessage(result.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return RedirectToAction("CustomerList", "Customer", new { Area = "Admin" });
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
        //[Authorize(Policy = "EditCustomer")]
        public async Task<IActionResult> UpdateCustomer(string userId)
        {
            var response = await _customerService.GetCustomerById(userId);
            var entity = _mapper.Map<UpdateCustomerViewModel>(response.Entity);
            if (response.IsSuccessful) return View(entity);
            _nToastNotify.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("CustomerList", "Customer", new { Area = "Admin" });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = "EditCustomer")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                var currentImage = await _customerService.GetCustomerById(user.Id);
                if (currentImage.Entity.ImageUrl != null)
                {
                    ImageTools.Delete(currentImage.Entity.ImageUrl, "Customer");
                }
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(user);
                    }
                    imageUrl = await ImageTools.Add(file, "Customer");
                }
                user.ImageUrl = imageUrl;
            }

            user.UserName = user.PhoneNumber;
            var response = await _customerService.UpdateCustomer(user);

            switch (response.IsSuccessful)
            {
                case true:
                    _nToastNotify.AddSuccessToastMessage(response.Message, new ToastrOptions()
                    {
                        Title = "پیام"

                    });
                    return RedirectToAction("CustomerList", "Customer", new { Area = "Admin" });
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
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = "DeleteCustomer")]
        public async Task DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomer(id);
        }

        [HttpGet]
        //[Authorize(Policy = "CustomerList")]
        public async Task<IActionResult> CustomerList()
        {
            var result = await _customerService.CustomerList();
            return View(result.Entity);
        }
    }
}
