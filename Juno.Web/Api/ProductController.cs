using AutoMapper;
using Juno.Common;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Infrastructure.Core;
using Juno.Web.Infrastructure.Extensions;
using Juno.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Juno.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        #region Initialize

        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }

        #endregion Initialize
        [Route("getbyid/{id:int}")]
        [HttpGet]
        //[HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetById(id);
                var responseData = Mapper.Map<Product, ProductViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        //[HttpGet]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var deleteIdProduct = _productService.Delete(id);
                    _productService.Save();
                    var responseData = Mapper.Map<Product, ProductViewModel>(deleteIdProduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
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
                    //chuyển chuổi danh sách id sang list danh sách
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var item in listProduct)
                    {
                        _productService.Delete(item);
                    }

                    _productService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return response;
            });
        }
        [Route("getallparent")]
        [HttpGet]
        //[HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getall")]
        [HttpGet]
        //[HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string KeyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _productService.GetAll(KeyWord);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
                var paginationSet = new PaginationSet<ProductViewModel>()
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
        [AllowAnonymous]// bỏ qua đăng nhập
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVm)
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

                    var newProduct = new Product();
                    newProduct.UpdateProduct(productVm);

                    newProduct.CreatedDate = DateTime.Now;
                    _productService.Add(newProduct);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [Route("update")]
        [HttpPut]
        [AllowAnonymous]// bỏ qua đăng nhập
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
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

                    var dbProduct = _productService.GetById(productVm.ID);
                    dbProduct.UpdateProduct(productVm);

                    dbProduct.UpdatedDate = DateTime.Now;
                    _productService.Update(dbProduct);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Product_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _productService.GetListProduct(filter).ToList();
                await ReportHelper.GenerateXls(data, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("ExportPdf")]
        public async Task<HttpResponseMessage> ExportPdf(HttpRequestMessage request, int id)
        {
            string fileName = string.Concat("Product" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".pdf");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var template = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/Admin/templates/product-detail.html"));
                var replaces = new Dictionary<string, string>();
                var product = _productService.GetById(id);

                replaces.Add("{{ProductName}}", product.Name);
                replaces.Add("{{Price}}", product.Price.ToString("N0"));
                replaces.Add("{{Description}}", product.Description);
                replaces.Add("{{Warranty}}", product.Warranty + " tháng");

                template = template.Parse(replaces);

                await ReportHelper.GeneratePdf(template, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        //private List<Product> ReadProductFromExcel(string fullPath, int categoryId)
        //{
        //    using (var package = new ExcelPackage(new FileInfo(fullPath)))
        //    {
        //        ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
        //        List<Product> listProduct = new List<Product>();
        //        ProductViewModel productViewModel;
        //        Product product;

        //        decimal originalPrice = 0;
        //        decimal price = 0;
        //        decimal promotionPrice;


        //        bool status = false;
        //        bool showHome = false;
        //        bool isHot = false;
        //        int warranty;

        //        for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
        //        {
        //            productViewModel = new ProductViewModel();
        //            product = new Product();

        //            productViewModel.Name = workSheet.Cells[i, 1].Value.ToString();
        //            productViewModel.Alias = StringHelper.ToUnsignString(productViewModel.Name);
        //            productViewModel.Description = workSheet.Cells[i, 2].Value.ToString();

        //            if (int.TryParse(workSheet.Cells[i, 3].Value.ToString(), out warranty))
        //            {
        //                productViewModel.Warranty = warranty;

        //            }

        //            decimal.TryParse(workSheet.Cells[i, 4].Value.ToString().Replace(",", ""), out originalPrice);
        //            productViewModel.OriginalPrice = originalPrice;

        //            decimal.TryParse(workSheet.Cells[i, 5].Value.ToString().Replace(",", ""), out price);
        //            productViewModel.Price = price;

        //            if (decimal.TryParse(workSheet.Cells[i, 6].Value.ToString(), out promotionPrice))
        //            {
        //                productViewModel.PromotionPrice = promotionPrice;

        //            }

        //            productViewModel.Content = workSheet.Cells[i, 7].Value.ToString();
        //            productViewModel.MetaKeyword = workSheet.Cells[i, 8].Value.ToString();
        //            productViewModel.MetaDescription = workSheet.Cells[i, 9].Value.ToString();

        //            productViewModel.CategoryID = categoryId;

        //            bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out status);
        //            productViewModel.Status = status;

        //            bool.TryParse(workSheet.Cells[i, 11].Value.ToString(), out showHome);
        //            productViewModel.HomeFlag = showHome;

        //            bool.TryParse(workSheet.Cells[i, 12].Value.ToString(), out isHot);
        //            productViewModel.HotFlag = isHot;

        //            product.UpdateProduct(productViewModel);
        //            listProduct.Add(product);
        //        }
        //        return listProduct;
        //    }
        //}
    }
}
