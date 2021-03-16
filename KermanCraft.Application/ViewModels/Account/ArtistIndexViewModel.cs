using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Application.ViewModels.Product;

namespace KermanCraft.Application.ViewModels.Account
{
    public class ArtistIndexViewModel
    {
        public int Product { get; set; }
        public int Company { get; set; }
        public int Course { get; set; }
        public int Event { get; set; }
        public bool Package { get; set; }
        public string PackageName { get; set; }
        public int LanguageId { get; set; }

    }
}
