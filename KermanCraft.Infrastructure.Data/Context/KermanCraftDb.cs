using KermanCraft.Domain.Models.ArtGallery;
using KermanCraft.Domain.Models.Article;
using KermanCraft.Domain.Models.Comment;
using KermanCraft.Domain.Models.Company;
using KermanCraft.Domain.Models.Course;
using KermanCraft.Domain.Models.Event;
using KermanCraft.Domain.Models.Gallery;
using KermanCraft.Domain.Models.Image;
using KermanCraft.Domain.Models.Language;
using KermanCraft.Domain.Models.News;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.Models.Share;
using KermanCraft.Domain.Models.Sms;
using KermanCraft.Domain.Models.Tag;
using KermanCraft.Domain.Models.Window;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Context
{
   public class KermanCraftDb:IdentityDbContext<People>
    {
        public KermanCraftDb(DbContextOptions<KermanCraftDb> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ArtGallery> ArtGalleries { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PeoplePackage> PeoplePackages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<EventMember> EventMembers { get; set; }
        public DbSet<CourseMember> CourseMembers { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<CompanyMember> CompanyMembers { get; set; }
        public DbSet<Lang> Langs { get; set; }
        //public DbSet<ShareResource> ShareResources { get; set; }

    }
}
