﻿using AutoMapper;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Infrastructure.Core;
using Juno.Web.Infrastructure.Extensions;
using Juno.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Juno.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Initialize

        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        #endregion Initialize
        [Route("getallparent")]
        [HttpGet]
        //[HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                 var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getall")]
        [HttpGet]
        //[HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request,string KeyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _productCategoryService.GetAll(KeyWord);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 if (!ModelState.IsValid)
                 {
                     response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                 }
                 else
                 {
                   
                     var newProductCategory = new ProductCategory();
                     newProductCategory.UpdateProductCategory(productCategoryVm);

                     newProductCategory.CreatedDate = DateTime.Now;
                     _productCategoryService.Add(newProductCategory);
                     _productCategoryService.Save();

                     var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                     response = request.CreateResponse(HttpStatusCode.Created, responseData);
                 }
                 
                 return response;
             });
        }
    }
}