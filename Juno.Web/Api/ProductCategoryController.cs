﻿using AutoMapper;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Infrastructure.Core;
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
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) :
            base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request,()=>
             {
                 var model = _productCategoryService.GetAll();
                 var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                 var response = request.CreateResponse(HttpStatusCode.OK, model);
                 return response;
             });           
        }
    }
}
