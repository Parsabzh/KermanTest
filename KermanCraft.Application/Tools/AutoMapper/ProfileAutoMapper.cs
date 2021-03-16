
using AutoMapper;
using KermanCraft.Application.ViewModels.Admin;
using KermanCraft.Application.ViewModels.ArtGallery;
using KermanCraft.Application.ViewModels.Article;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Application.ViewModels.Company;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Customer;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Application.ViewModels.Gallery;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Application.ViewModels.News;
using KermanCraft.Application.ViewModels.Package;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Application.ViewModels.Sms;
using KermanCraft.Application.ViewModels.Window;
using KermanCraft.Domain.Models.ArtGallery;
using KermanCraft.Domain.Models.Article;
using KermanCraft.Domain.Models.Comment;
using KermanCraft.Domain.Models.Company;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.Models.Gallery;
using KermanCraft.Domain.Models.News;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.Models.Window;

namespace KermanCraft.Application.Tools.AutoMapper
{
   public class ProfileAutoMapper:Profile
    {
        public ProfileAutoMapper()
        {
            //Admin
            CreateMap<AddAdminViewModel, People>();
            CreateMap<People, AddAdminViewModel>();
            CreateMap<UpdateAdminViewModel, People>();
            CreateMap<People, UpdateAdminViewModel>();
            CreateMap<People, AdminListViewModel>();
            CreateMap<AdminListViewModel, People>();
            CreateMap<People, AdminViewModel>();
            CreateMap<AdminViewModel, People>();
            CreateMap<AdminViewModel, UpdateAdminViewModel>();
            CreateMap<UpdateAdminViewModel, AdminViewModel>();
            CreateMap<People, People>();
            /////////////////////////////

            //Artist
            CreateMap<AddArtistViewModel, People>();
            CreateMap<People, AddArtistViewModel>();
            CreateMap<UpdateArtistViewModel, People>();
            CreateMap<People, UpdateArtistViewModel>();
            CreateMap<People, ArtistListViewModel>();
            CreateMap<ArtistListViewModel, People>();
            CreateMap<People, ArtistViewModel>();
            CreateMap<ArtistViewModel, People>();
            CreateMap<ArtistViewModel, UpdateArtistViewModel>();
            CreateMap<UpdateArtistViewModel, ArtistViewModel>();
            CreateMap<AddArtistViewModel, AddArtistFrontViewModel>();
            CreateMap<AddArtistFrontViewModel, AddArtistViewModel>();
            /////////////////////////////

            //Customer
            CreateMap<AddCustomerViewModel, People>();
            CreateMap<People, AddCustomerViewModel>();
            CreateMap<UpdateCustomerViewModel, People>();
            CreateMap<People, UpdateCustomerViewModel>();
            CreateMap<People, CustomerListViewModel>();
            CreateMap<CustomerListViewModel, People>();
            CreateMap<People, CustomerViewModel>();
            CreateMap<CustomerViewModel, People>();
            CreateMap<CustomerViewModel, UpdateCustomerViewModel>();
            CreateMap<UpdateCustomerViewModel, CustomerViewModel>();
            CreateMap<AddCustomerViewModel, AddCustomerFrontViewModel>();
            CreateMap<AddCustomerFrontViewModel, AddCustomerViewModel>();
            CreateMap<People, People>();
            /////////////////////////////


            //Product
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, AddUpdateProductViewModel>();
            CreateMap<AddUpdateProductViewModel, Product>();
            CreateMap<ProductViewModel, AddUpdateProductViewModel>();
            CreateMap<AddUpdateProductViewModel, ProductViewModel>();
            CreateMap<ProductViewModel, ProductListViewModel>();
            CreateMap<ProductListViewModel, ProductViewModel>();
            CreateMap<Product, Product>();
            /////////////////////////////
            

            //Event
            CreateMap<Event, EventViewModel>();
            CreateMap<EventViewModel, Event>();
            CreateMap<Event, Event>();
            /////////////////////////////
            

            //Comment
            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentViewModel, Comment>();
            CreateMap<Comment, Comment>();
            /////////////////////////////

            //Window
            CreateMap<WindowViewModel, Window>();
            CreateMap<Window, WindowViewModel>();
            CreateMap<UpdateWindowViewModel, Window>();
            CreateMap<Window, UpdateWindowViewModel>();
            CreateMap<Window, Window>();
            /////////////////////////////
            
            //Image
            CreateMap<Domain.Models.Image.Image, ImageViewModel>();
            CreateMap<ImageViewModel, Domain.Models.Image.Image>();
            /////////////////////////////
            
            //News
            CreateMap<News, NewsViewModel>();
            CreateMap<NewsViewModel, News>();
            CreateMap<UpdateNewsViewModel, News>();
            CreateMap<News, UpdateNewsViewModel>();
            CreateMap<News, News>();
            /////////////////////////////

            //ArtGallery
            CreateMap<ArtGallery, ArtGalleryViewModel>();
            CreateMap<ArtGalleryViewModel, ArtGallery>();
            CreateMap<ArtGallery, ArtGallery>();
            /////////////////////////////

            //Gallery
            CreateMap<Gallery, GalleryViewModel>();
            CreateMap<GalleryViewModel, Gallery>();
            CreateMap<Gallery, Gallery>();

            //Company
            CreateMap<Company, CompanyViewModel>();
            CreateMap<CompanyViewModel, Company>();
            CreateMap<Company, Company>();
            /////////////////////////////

            //CompanyMember
            CreateMap<CompanyMember, CompanyMemberViewModel>();
            CreateMap<CompanyMemberViewModel, CompanyMember>();
            CreateMap<CompanyMember, CompanyMember>();
            /////////////////////////////


            //Course
            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();
            CreateMap<Course, Course>();
            /////////////////////////////
            
            //Package
            CreateMap<Package, PackageViewModel>();
            CreateMap<PackageViewModel, Package>();
            CreateMap<Package, Package>();
            /////////////////////////////
          
            //PeoplePackage
            CreateMap<PeoplePackage, PeoplePackageViewModel>();
            CreateMap<PeoplePackageViewModel, PeoplePackage>();
            CreateMap<PeoplePackage, PeoplePackage>();
            /////////////////////////////

            //Article
            CreateMap<Article, ArticleViewModel>();
            CreateMap<ArticleViewModel, Article>();
            CreateMap<Article, Article>();
            /////////////////////////////

            //Sms
            CreateMap<Domain.Models.Sms.Sms, SmsViewModel>();
            CreateMap<SmsViewModel, Domain.Models.Sms.Sms>();

            //EventMember
            CreateMap<EventMember, EventMemberViewModel>();
            CreateMap<EventMemberViewModel, EventMember>();
            CreateMap<EventMember, EventMember>();
            /////////////////////////////

            //CourseMember
            CreateMap<CourseMember, CourseMemberViewModel>();
            CreateMap<CourseMemberViewModel, CourseMember>();
            CreateMap<CourseMember, CourseMember>();
            /////////////////////////////

            //ProductType
            CreateMap<ProductType, ProductTypeViewModel>();
            CreateMap<ProductTypeViewModel, ProductType>();
            CreateMap<ProductType, ProductType>();
            /////////////////////////////
        }
    }
}
