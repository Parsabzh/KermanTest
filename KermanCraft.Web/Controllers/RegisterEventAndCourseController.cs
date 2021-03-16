using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Dto.Payment;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Domain.Models.Event;
using NToastNotify;
using ZarinPal.Class;

namespace KermanCraft.Web.Controllers
{
    public class RegisterEventAndCourseController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ICourseService _courseService;
        private readonly IEventMemberService _eventMemberService;
        private readonly ICourseMemberService _courseMemberService;
        private readonly ICustomerService _customerService;
        private readonly IToastNotification _toastNotification;
        private IMapper _mapper;
        private Payment _payment;

        public RegisterEventAndCourseController(IEventService eventService, ICourseService courseService, IEventMemberService eventMemberService, ICourseMemberService courseMemberService, ICustomerService customerService, IToastNotification toastNotification, IMapper mapper, Payment payment)
        {
            var expose = new Expose();
            _eventService = eventService;
            _courseService = courseService;
            _eventMemberService = eventMemberService;
            _courseMemberService = courseMemberService;
            _customerService = customerService;
            _toastNotification = toastNotification;
            _mapper = mapper;
            _payment = expose.CreatePayment();
        }


        public async Task<IActionResult> RegisterEvent(int eventId)
        {

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _customerService.GetCustomerById(userId);
            if (!user.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(user.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return View();
            }

            var eve = await _eventService.GetEvent(eventId);
            var eveMember = new EventMemberViewModel()
            {
                EventId = eventId,
                PeopleFkId = userId,
                IsPaid = false
            };
            await _eventMemberService.AddEventMember(eveMember);
            //var payment = new Payment();
            //var res = new Payment();
            //object p = await res.Request(, Payment.Mode.zarinpal);
            //await res.Verification(new DtoVerification())
           //var res = payment.Request($"پرداخت فاکتور شماره {eve.Entity.Name} {eve.Entity.EventId}",
               //$"https://localhost:44307/RegisterEventAndCourse/PaymentResult/" + eve.Entity.EventId, $"{user.Entity.Email}", $"{user.Entity.PhoneNumber}");

            var res=  await  _payment.Request(new DtoRequest()
              {
                  Amount = (int)eve.Entity.Price,
                  Email = $"{user.Entity.Email}",
                  Description = $"پرداخت فاکتور شماره {eve.Entity.Name} {eve.Entity.EventId}",
                  Mobile = $"{user.Entity.PhoneNumber}",
                  CallbackUrl = $"https://Kermancrafts.com/RegisterEventAndCourse/PaymentResult/" + eve.Entity.EventId,
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

        public async Task<IActionResult> RegisterCourse(int courseId)
        {

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _customerService.GetCustomerById(userId);
            if (!user.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(user.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return View();
            }

            var course = await _courseService.GetCourse(courseId);
            var courseMember = new CourseMemberViewModel()
            {
                CourseId = courseId,
                PeopleFkId = userId,
                IsPaid = false
            };
            await _courseMemberService.AddCourseMember(courseMember);
            var res = await _payment.Request(new DtoRequest()
            {
                Amount = (int)course.Entity.Price,
                Email = $"{user.Entity.Email}",
                Description = $"پرداخت فاکتور شماره {course.Entity.Subject} {course.Entity.CourseId}",
                Mobile = $"{user.Entity.PhoneNumber}",
                CallbackUrl = $"https://Kermancrafts.com/RegisterEventAndCourse/PaymentResult/" + course.Entity.CourseId,
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

        public async Task<IActionResult> PaymentResult(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"].ToString();
                var eve = await _eventService.GetEvent(id);

                var res = await _payment.Verification(new DtoVerification()
                {
                    Amount = (int)eve.Entity.Price,
                    Authority = authority,
                    MerchantId = "689ee14c-e228-11e6-b200-000c295eb8fc"
                }, Payment.Mode.zarinpal);
                if (res.Status == 100)
                {
                    var response = await _eventMemberService.AddEventMember(new EventMemberViewModel()
                    {
                        Date = DateTime.Now,
                        EventId = id,
                        PeopleFkId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                        IsPaid = true,
                        ReferenceId = res.RefId.ToString("N")
                    });
                    ViewBag.code = res.RefId;
                    return View();
                }

            }
            return NotFound();
        }

        public async Task<IActionResult> CoursePaymentResult(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"].ToString();
                var course = await _courseService.GetCourse(id);
                var res = await _payment.Verification(new DtoVerification()
                {
                    Amount = (int)course.Entity.Price,
                    Authority = authority,
                    MerchantId = "689ee14c-e228-11e6-b200-000c295eb8fc"
                }, Payment.Mode.zarinpal);
                if (res.Status == 100)
                {
                    var response = await _courseMemberService.AddCourseMember(new CourseMemberViewModel()
                    {
                        Date = DateTime.Now,
                        CourseId = id,
                        PeopleFkId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
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
