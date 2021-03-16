using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Application.ViewModels.ArtGallery;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Application.ViewModels.Company;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Event;
using KermanCraft.Application.ViewModels.News;
using KermanCraft.Application.ViewModels.Window;

namespace KermanCraft.Application.ViewModels.IndexView
{
    public class FirstIndexViewModel
    {



        public List<ArtistListViewModel> ArtistViewModels { get; set; }
        public List<ArtGalleryViewModel> ArtGalleryViewModels { get; set; }
        public List<CompanyViewModel> CompanyViewModels { get; set; }
        public List<WindowViewModel> WindowViewModels { get; set; }
        public List<CourseViewModel> CourseViewModels { get; set; }
        public List<EventViewModel> EventViewModels { get; set; }
        public List<NewsViewModel> NewsViewModels { get; set; }
    }
}
