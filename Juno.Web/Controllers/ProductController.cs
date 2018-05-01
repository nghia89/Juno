using AutoMapper;
using Juno.Common;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Infrastructure.Core;
using Juno.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Juno.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }
        public ActionResult Detail(int id)
        {
            return View();
        }
        public ActionResult Category(int id, int page=1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPageing(id,page,pageSize,out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable < ProductViewModel >> (productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var category = _productCategoryService.GetById(id);
            ViewBag.category= Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
    }
}