using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.Image;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Application.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NToastNotify;
using ImageTools = KermanCraft.Application.Tools.Image.ImageTools;

namespace KermanCraft.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    [Area("Admin")]
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IToastNotification _toastNotification;
        private readonly IArtistService _artistService;
        private readonly IProductTypeService _productTypeService;
        private IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, IToastNotification toastNotification, IArtistService artistService, IProductTypeService productTypeService, IMapper mapper, ITagService tagService, IImageService imageService)
        {
            _productService = productService;
            _toastNotification = toastNotification;
            _artistService = artistService;
            _productTypeService = productTypeService;
            _mapper = mapper;
            _tagService = tagService;
            _imageService = imageService;
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var productType = await _productTypeService.GetAllProductType();
            var product = new AddUpdateProductViewModel
            {
                ProductTypeViewModels = productType.Entity.ToList()
            };
            return View(product);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddProduct(AddUpdateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var user = _artistService.GetArtistUserByMobile(product.OwnerMobile);
            if (!user.IsSuccessful)
            {
                ModelState.AddModelError("", user.Message);
                return View(product);
            }

            product.PeopleId = user.Entity.Id;

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
                return RedirectToAction("ProductList", "Product", new { Area = "Admin", userId = product.PeopleId });
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
                return RedirectToAction("ProductList", "Product", new { Area = "Admin" });
            }

            var productType = await _productTypeService.GetAllProductType();
            var productVm = new AddUpdateProductViewModel
            {
                ProductTypeViewModels = productType.Entity.ToList(),
                Description = product.Entity.Description,
                PeopleId = product.Entity.PeopleId,
                Type = product.Entity.Type,
                IsForSale = product.Entity.IsForSale,
                Code = product.Entity.Code,
                DiscountPrice = product.Entity.DiscountPrice,
                HasDiscount = product.Entity.HasDiscount,
                ImageName = product.Entity.ImageName,
                Material = product.Entity.Material,
                Price = product.Entity.Price,
                ProductId = product.Entity.ProductId,
                Weight = product.Entity.Weight,
                Size = product.Entity.Size,
                ProductName = product.Entity.ProductName,
                Status = product.Entity.Status

            };
            _toastNotification.AddSuccessToastMessage(product.Message);
            return View(productVm);
        }


        [HttpPost]
        //[AutoValidateAntiforgeryToken]
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
            return RedirectToAction("ProductList", "Product", new { Area = "Admin", userId = product.PeopleId });

        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            _toastNotification.AddSuccessToastMessage(response.Message);

        }

        [HttpGet]
        public IActionResult GetArtist()
        {
            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public IActionResult GetArtist(GetArtistUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var response = _artistService.GetArtistUserByMobile(user.Mobile);
            if (response.IsSuccessful)
            {


                return RedirectToAction("ProductList", "Product", new { Area = "Admin", userId = response.Entity.Id });
            }
            _toastNotification.AddWarningToastMessage(response.Message, new ToastrOptions()
            {
                Title = "پیام"
            });
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> ProductList(string userId)
        {
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
            return RedirectToAction("GetArtist", "Product", new { Area = "Admin" });

        }

        public async Task<IActionResult> AllProductList()
        {
            var response = await _productService.GetProductsList();
            return View(response.Entity);
        }

        [HttpGet]
        public IActionResult AddProductType()
        {
            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddProductType(ProductTypeViewModel productType)
        {
            if (!ModelState.IsValid)
            {
                return View(productType);
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
                        return View(productType);
                    }
                    imageUrl = await ImageTools.Add(file, "ProductType");
                }
                productType.Name = imageUrl;
            }
            var response = await _productTypeService.AddProductType(productType);
            if (response.IsSuccessful)
            {
                return RedirectToAction("ProductTypeList", "Product");
            }
            ModelState.AddModelError("", response.Message);
            return View(productType);
        }

        [HttpPost]
        public async Task DeleteProductType(int id)
        {
            await _productTypeService.DeleteProductType(id);
        }

        public async Task<IActionResult> ProductTypeList()
        {
            var response = await _productTypeService.GetAllProductType();
            return View(response.Entity);
        }


        [HttpGet]
        public IActionResult AddTag(int productId)
        {
            var tag = new TagViewModel()
            {
                ProductFkId = productId
            };
            return View(tag);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddTag(TagViewModel Tag)
        {
            if (!ModelState.IsValid)
            {
                return View(Tag);
            }


            var response = await _tagService.AddTag(Tag);
            if (response.IsSuccessful)
            {
                return RedirectToAction("TagList", "Product");
            }
            ModelState.AddModelError("", response.Message);
            return View(Tag);
        }

        [HttpGet]
        public async Task<IActionResult> TagList(int productId)
        {

            var response = await _tagService.GetProductTag(productId);
            return View(response.Entity);
        }

        [HttpPost]
        public async Task DeleteTag(int id)
        {
            await _tagService.DeleteTag(id);
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddImage(int winId)
        {
            var files = HttpContext.Request.Form.Files;
            if (!files.Any()) return RedirectToAction("UpdateProduct", "Product", new { Area = "Admin", id = winId });
            foreach (var file in files)
            {
                if (!file.IsImage())
                {
                    ModelState.AddModelError("", "فرمت فایل نامعتبر است");
                    return RedirectToAction("UpdateProduct", "Product", new { Area = "Admin", id = winId });
                }

                await _imageService.AddImage(new ImageViewModel()
                {
                    ImageUrl = await ImageTools.Add(file, "Product")

                });

            }
            return RedirectToAction("UpdateProduct", "Product", new { Area = "Admin", id = winId });
        }
        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task DeleteImage(int imgId)
        {
            await _imageService.DeleteImage(imgId);
        }


    }
}
