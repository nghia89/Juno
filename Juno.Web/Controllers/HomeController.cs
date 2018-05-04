using AutoMapper;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Juno.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService,IProductService productService)
          
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _commonService = commonService;
     
        }

        // GET: Home
        [OutputCache(Duration =60,Location =System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var sliderModel = _commonService.GetSlides();
            var sliderView = Mapper.Map<IEnumerable< Slide>, IEnumerable<SlideViewModel>>(sliderModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = sliderView;

            var lastestProductModel = _productService.Getlastest(3);
            var topsaleProductModel = _productService.GetHotProduct(6);
            var lastestPeoductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topsaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topsaleProductModel);
            homeViewModel.LastestProducts = lastestPeoductViewModel;
            homeViewModel.TopSaleProducts = topsaleProductViewModel;
            return View(homeViewModel);
        }
        [ChildActionOnly]
        [OutputCache(Duration=3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            ViewBag.time = DateTime.Now.ToString("T");
            return PartialView(footerViewModel);
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}