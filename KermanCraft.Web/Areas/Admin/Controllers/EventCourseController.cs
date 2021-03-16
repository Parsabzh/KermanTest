using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Application.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class EventCourseController : Controller
    {

        private readonly IToastNotification _toastNotification;
        private readonly IEventService _eventService;
        private readonly ICourseService _courseService;
        private readonly IArtistService _artistService;
        private readonly IEventMemberService _eventMemberService;
        private readonly ICourseMemberService _courseMemberService;
       

        public EventCourseController(IToastNotification toastNotification,
            IEventService eventService, ICourseService courseService, 
            IArtistService artistService, 
            IEventMemberService eventMemberService,
            ICourseMemberService courseMemberService)
        {
            _toastNotification = toastNotification;
            _eventService = eventService;
            _courseService = courseService;
            _artistService = artistService;
            _eventMemberService = eventMemberService;
            _courseMemberService = courseMemberService;
        }

        [HttpGet]
        public IActionResult AddEvent()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddEvent(EventViewModel eve)
        {
            if (!ModelState.IsValid)
            {
                return View(eve);
            }

            //var user =  _artistService.GetArtistUserByMobile(eve.PhoneNumber);
            //if (!user.IsSuccessful)
            //{
            //    ModelState.AddModelError("",user.Message);
            //    return View(eve);
            //}

            eve.PeopleId = /*HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);*/ "2";
            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(eve);
                    }
                    imageUrl = await ImageTools.Add(file, "Event");
                }
                eve.ImageUrl = imageUrl;
            }
            else
            {
                eve.ImageUrl = "Event.png";
            }
            var response = await _eventService.AddEvent(eve);
            if (response.IsSuccessful) return RedirectToAction("EventList","EventCourse",new {Area="Admin"});
            _toastNotification.AddErrorToastMessage(response.Message);
            return View(eve);

        }

        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int eventId)
        {
            var eve = await _eventService.GetEvent(eventId);
            if (!eve.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(eve.Message);
                return RedirectToAction("EventList", "EventCourse");
            }

            _toastNotification.AddSuccessToastMessage(eve.Message);
            return View(eve.Entity);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateEvent(EventViewModel eve)
        {
            if (!ModelState.IsValid)
            {
                return View(eve);
            }

            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    var currentEvent = await _eventService.GetEvent(eve.EventId);
                    if (currentEvent!=null)
                    {
                        ImageTools.Delete(currentEvent.Entity.ImageUrl,"Event");
                    }
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(eve);
                    }
                    imageUrl = await ImageTools.Add(file, "Event");
                }
                eve.ImageUrl = imageUrl;
            }
            var response = await _eventService.UpdateEvent(eve);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(eve);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("EventList", "EventCourse", new { Area = "Admin" });

        }
        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteEvent(int id)
        {
            var response = await _eventService.DeleteEvent(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> EventList()
        {
            var response = await _eventService.GetAllEvent();
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
            return View(response.Entity);
        }

        //[HttpGet]
        //public async Task<IActionResult> EventMemberList(int id)
        //{
        //    var response = await _eventMemberService.GetEventMembers(id);
        //    if (response.IsSuccessful)
        //    {
        //        _toastNotification.AddSuccessToastMessage(response.Message, new ToastrOptions()
        //        {
        //            Title = "پیام"
        //        });

        //    }

        //    _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
        //    {
        //        Title = "پیام"
        //    });
        //    return View(response.Entity);
        //}

        [HttpGet]
        public IActionResult AddCourse()
        {

            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddCourse(CourseViewModel course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }
            //var user = _artistService.GetArtistUserByMobile(course.PhoneNumber);
            //if (!user.IsSuccessful)
            //{
            //    ModelState.AddModelError("", user.Message);
            //    return View(course);
            //}
            course.PeopleId = "2";
            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(course);
                    }
                    imageUrl = await ImageTools.Add(file, "Course");
                }
                course.ImageUrl = imageUrl;
            }
            else
            {
                course.ImageUrl = "Course.png";
            }
            var response = await _courseService.AddCourse(course);
            if (response.IsSuccessful) return RedirectToAction("CourseList", "EventCourse", new { Area = "Admin" });
            _toastNotification.AddErrorToastMessage(response.Message);
            return View(course);

        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int courseId)
        {
            var course = await _courseService.GetCourse(courseId);
            if (!course.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(course.Message);
                return RedirectToAction("CourseList", "EventCourse", new { Area = "Admin" });
            }

            _toastNotification.AddSuccessToastMessage(course.Message);
            return View(course.Entity);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateCourse(CourseViewModel course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            var files = HttpContext.Request.Form.Files;
            string imageUrl = null;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    var currentCourse = await _courseService.GetCourse(course.CourseId);
                    if (currentCourse != null)
                    {
                        ImageTools.Delete(currentCourse.Entity.ImageUrl, "Course");
                    }
                    if (!file.IsImage())
                    {
                        ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                        return View(course);
                    }
                    imageUrl = await ImageTools.Add(file, "Course");
                }
                course.ImageUrl = imageUrl;
            }
            var response = await _courseService.UpdateCourse(course);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(course);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("CourseList", "EventCourse", new { Area = "Admin" });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteCourse(int id)
        {
            var response = await _courseService.DeleteCourse(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> CourseList()
        {
            var response = await _courseService.GetAllCourse();
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
            return View(response.Entity);
        }

        public async Task<IActionResult> GetEventMember(int eventId)
        {
            var response = await _eventMemberService.GetEventMembers(eventId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message,new ToastrOptions()
                {
                    Title = "پیام"
                });
            }

            return View(response.Entity);
        }

        public async Task<IActionResult> GetCourseMembers(int courseId)
        {
            var response = await _courseMemberService.GetCourseMembers(courseId);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
            }

            return View(response.Entity);
        }

    }
}
