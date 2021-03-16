using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Services;
using KermanCraft.Application.Tools.AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KermanCraft.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            //Application Layer
          
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProfileAutoMapper());
            });
            var mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
            service.AddTransient<IAdminService, AdminService>();
            service.AddTransient<IArtistService, ArtistService>();
            service.AddTransient<ICustomerService, CustomerService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IWindowService, WindowService>();
            service.AddTransient<IImageService, ImageService>();
            service.AddTransient<INewsService, NewsService>();
            service.AddTransient<IArtGalleryService, ArtGalleryService>();
            service.AddTransient<IGalleryService, GalleryService>();
            service.AddTransient<ICompanyService, CompanyService>();
            service.AddTransient<IEventService, EventService>();
            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<IPackageService, PackageService>();
            service.AddTransient<IPeoplePackageService, PeoplePackageService>();
            service.AddTransient<IProductTypeService, ProductTypeService>();
            service.AddTransient<IArticleService, ArticleService>();
            service.AddTransient<ITagService, TagService>();
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<ISmsService, SmsService>();
            service.AddTransient<ICompanyMemberService, CompanyMemberService>();
            service.AddTransient<IEventMemberService, EventMemberService>();
            service.AddTransient<ICourseMemberService, CourseMemberService>();
            service.AddTransient<ICommentService, CommentService>();
            



            //Infrastructure Data Layer
            service.AddTransient<IAdminRepository, AdminRepository>();
            service.AddTransient<IArtistRepository, ArtistRepository>();
            service.AddTransient<ICustomerRepository, CustomerRepository>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IWindowRepository, WindowRepository>();
            service.AddTransient<IImageRepository, ImageRepository>();
            service.AddTransient<INewsRepository, NewsRepository>();
            service.AddTransient<IArtGalleryRepository, ArtGalleryRepository>();
            service.AddTransient<IGalleryRepository, GalleryRepository>();
            service.AddTransient<ICompanyRepository, CompanyRepository>();
            service.AddTransient<IEventRepository, EventRepository>();
            service.AddTransient<ICourseRepository, CourseRepository>();
            service.AddTransient<IPackageRepository, PackageRepository>();
            service.AddTransient<IPeoplePackageRepository, PeoplePackageRepository>();
            service.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            service.AddTransient<IArticleRepository, ArticleRepository>();
            service.AddTransient<ITagRepository, TagRepository>();
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<ISmsRepository, SmsRepository>();
            service.AddTransient<ICompanyMemberRepository, CompanyMemberRepository>();
            service.AddTransient<IEventMemberRepository, EventMemberRepository>();
            service.AddTransient<ICourseMemberRepository, CourseMemberRepository>();
            service.AddTransient<ICommentRepository, CommentRepository>();

        }
    }
}
