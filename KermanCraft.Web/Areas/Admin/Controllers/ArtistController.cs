using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Artist;
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
    public class ArtistController : Controller
    {
        private readonly IToastNotification _nToastNotify;
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IToastNotification nToastNotify, IArtistService artistService, IMapper mapper)
        {
            _nToastNotify = nToastNotify;

            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Policy = "AddArtist")]
        public IActionResult AddArtistUser()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = "AddArtist")]
        public async Task<IActionResult> AddArtistUser(AddArtistViewModel user)
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
                    imageUrl = await ImageTools.Add(file, "ArtistUser");
                }
                user.ImageUrl = imageUrl;
            }
            else
            {
                user.ImageUrl = "User.png";
            }

            user.UserName = user.PhoneNumber;
            var result = await _artistService.AddArtist(user);
            if (result.IsSuccessful)
            {
                _nToastNotify.AddSuccessToastMessage(result.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return RedirectToAction("ArtistUserList", "Artist", new { Area = "Admin" });
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
        //[Authorize(Policy = "EditArtist")]
        public async Task<IActionResult> UpdateArtistUser(string userId)
        {
            var response = await _artistService.GetArtistUserById(userId);
            var entity = _mapper.Map<UpdateArtistViewModel>(response.Entity);
            if (response.IsSuccessful) return View(entity);
            _nToastNotify.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("ArtistUserList", "Artist", new { Area = "Admin" });
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[Authorize(Policy = "EditArtist")]
        public async Task<IActionResult> UpdateArtistUser(UpdateArtistViewModel user)
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
                    _nToastNotify.AddSuccessToastMessage(response.Message, new ToastrOptions()
                    {
                        Title = "پیام"

                    });
                    return RedirectToAction("ArtistUserList", "Artist", new { Area = "Admin" });
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
        //[Authorize(Policy = "DeleteArtist")]
        public async Task DeleteArtistUser(string id)
        {
            await _artistService.DeleteArtist(id);
        }

        [HttpGet]
        //[Authorize(Policy = "ArtistList")]
        public async Task<IActionResult> ArtistUserList()
        {
            var result = await _artistService.ArtistUserList();
            return View(result.Entity);
        }
    }
}
