using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Application.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IToastNotification _toastNotification;
        private readonly IArtistService _artistService;
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly IImageService _imageService;
        private IPeoplePackageService _peoplePackageService;
        private IPackageService _packageService;

        public ProductController(IProductService productService, IToastNotification toastNotification,
            IArtistService artistService, IProductTypeService productTypeService, IMapper mapper,
            ITagService tagService, IImageService imageService, IPeoplePackageService packageService, IPeoplePackageService peoplePackageService, IPackageService packageService1)
        {
            _productService = productService;
            _toastNotification = toastNotification;
            _artistService = artistService;
            _productTypeService = productTypeService;
            _mapper = mapper;
            _tagService = tagService;
            _imageService = imageService;
            _peoplePackageService = peoplePackageService;
            _packageService = packageService1;
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProduct = await _productService.GetArtistProduct(userId);
            var peoplePackage = await _peoplePackageService.GetPeoplePackage(userId);

            if (!peoplePackage.IsSuccessful)
            {
                if (userProduct.Entity.Count()>6)
                {
                    _toastNotification.AddErrorToastMessage("شما به حدنصاب اضافه کردن محصول رسیده اید! لطفا پکیج خود را بروزرسانی کنید!",new ToastrOptions()
                    {
                        Title = "پیام"
                    });
                }
            }

            var package = await _packageService.GetPackage(peoplePackage.Entity.PackageId);
            if (userProduct.Entity.Count() > package.Entity.ProductNum)
            {
                _toastNotification.AddErrorToastMessage("شما به حدنصاب اضافه کردن محصول رسیده اید! لطفا پکیج خود را بروزرسانی کنید!", new ToastrOptions()
                {
                    Title = "پیام"
                });
            }
            var productType = await _productTypeService.GetAllProductType();
            var product = new AddUpdateProductViewModel
            {
                PeopleId = userId,
                ProductTypeViewModels = productType.Entity.ToList()
            };
            return View(product);
        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddProduct(AddUpdateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var response = await _productService.AddProduct(product);
            if (response.IsSuccessful)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        if (!file.IsImage())
                        {
                            ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                            return View(product);
                        }

                        await _imageService.AddImage(new ImageViewModel()
                        {
                            ImageUrl = await ImageTools.Add(file, "Product"),
                            ProductFkId = response.Entity.ProductId,
                        });
                    }

                }
                else
                {
                    await _imageService.AddImage(new ImageViewModel()
                    {
                        ImageUrl = "box.png",
                        ProductFkId = response.Entity.ProductId,
                    });
                }
                _toastNotification.AddSuccessToastMessage(response.Message);
                return RedirectToAction("ProductList", "Product", new {Area = "Artist", userId = product.PeopleId});
            }

          

            ModelState.AddModelError("", response.Message);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {

            var product = await _productService.GetProduct(id);
            if (!product.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(product.Message);
                return RedirectToAction("ProductList", "Product", new {Area = "Artist"});
            }

            var productVm = new AddUpdateProductViewModel();
            productVm =_mapper.Map(product, productVm);
            var productType = await _productTypeService.GetAllProductType();
            productVm.ProductTypeViewModels = productType.Entity.ToList();
            //var productVm = new AddUpdateProductViewModel
            //{
            //    ProductTypeViewModels = productType.Entity.ToList(),
            //    Description = product.Entity.Description,
            //    PeopleId = product.Entity.PeopleId,
            //    Type = product.Entity.Type,
            //    IsForSale = product.Entity.IsForSale,
            //    Code = product.Entity.Code,
            //    DiscountPrice = product.Entity.DiscountPrice,
            //    HasDiscount = product.Entity.HasDiscount,
            //    ImageName = product.Entity.ImageName,
            //    Material = product.Entity.Material,
            //    Price = product.Entity.Price,
            //    ProductId = product.Entity.ProductId,
            //    Weight = product.Entity.Weight,
            //    Size = product.Entity.Size,
            //    ProductName = product.Entity.ProductName,
            //    Status = product.Entity.Status

            //};
            _toastNotification.AddSuccessToastMessage(product.Message);
            return View(productVm);
        }


        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct(AddUpdateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

         
            var response = await _productService.UpdateProduct(product);
            if (!response.IsSuccessful)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
                ModelState.AddModelError("", response.Message);
                return View(product);
            }

            _toastNotification.AddSuccessToastMessage(response.Message);
            return RedirectToAction("ProductList", "Product", new {Area = "Artist", userId = product.PeopleId});

        }

        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

       

      
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _productService.GetArtistProduct(userId);
            if (response.IsSuccessful)
            {
                _toastNotification.AddSuccessToastMessage(response.Message, new ToastrOptions()
                {
                    Title = "پیام"
                });
                return View(response.Entity);
            }

            _toastNotification.AddAlertToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return RedirectToAction("GetArtist", "Product", new {Area = "Artist"});

        }
        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddImage(int winId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateProduct", "Product", new { Area = "Artist", id = winId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateProduct", "Product", new { Area = "Artist", id = winId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "Product")

                });

            }
            return RedirectToAction("UpdateProduct", "Product", new { Area = "Artist", id = winId });
        }
        [HttpPost]
       // [AutoValidateAntiforgeryToken]
        public async Task DeleteImage(int imgId)
        {
            await _imageService.DeleteImage(imgId);
        }

    }
}
