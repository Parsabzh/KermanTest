using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Account;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Customer;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Domain.Models.People;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

namespace KermanCraft.Web.Areas.Customer.Controllers
{
    public class CustomerAccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<People> _signInManager;
        private readonly IAccountService _accountService;
        private readonly ICourseService _courseService;
        private readonly IEventService _eventService;
        private readonly ICourseMemberService _courseMemberService;
        private readonly IEventMemberService _eventMemberService;

        public CustomerAccountController(ICustomerService customerService, IMapper mapper, IToastNotification toastNotification, SignInManager<People> signInManager, IAccountService accountService, ICourseService courseService, IEventService eventService, ICourseMemberService courseMemberService, IEventMemberService eventMemberService)
        {
            _customerService = customerService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _signInManager = signInManager;
            _accountService = accountService;
            _courseService = courseService;
            _eventService = eventService;
            _courseMemberService = courseMemberService;
            _eventMemberService = eventMemberService;
        }
        //private UserManager<People> _userManager;

        //public CustomerAccountController(UserManager<People> userManager)
        //{
        //    _userManager = userManager;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Policy = "EditCustomer")]
        public async Task<IActionResult> UpdateCustomer(string userId)
        {
            var response = await _customerService.GetCustomerById(userId);
            var entity = _mapper.Map<UpdateCustomerViewModel>(response.Entity);
            if (response.IsSuccessful) return View(entity);
            _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("Index", "CustomerAccount", new { Area = "Customer" });
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
                return RedirectToAction("Index", "CustomerAccount", new { Area = "Customer", userId = pass.UserId });
            foreach (var error in response.MessageList)
            {
                ModelState.AddModelError("", error);
            }
            return View();

        }

        public  async Task<IActionResult> GetCustomerCourse()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _courseMemberService.GetCustomerCourse(userId);
            var course = new List<CourseViewModel>();
            foreach (var courseMemberViewModel in user.Entity)
            {
                var c= await _courseService.GetCourse(courseMemberViewModel.CourseId);
                course.Add(c.Entity);
            }
            return View(course);

        }

        public async Task<IActionResult> GetCustomerEvent()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _eventMemberService.GetCustomerEvent(userId);
            var course = new List<EventViewModel>();
            foreach (var courseMemberViewModel in user.Entity)
            {
                var c = await _eventService.GetEvent(courseMemberViewModel.EventId);
                course.Add(c.Entity);
            }
            return View(course);

        }




    }
}
