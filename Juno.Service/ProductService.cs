﻿using Juno.Common;
using Juno.Data.Infrastructure;
using Juno.Data.Repositories;
using Juno.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Juno.server
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product ProductCategory);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string Keyword);
        IEnumerable<Product> GetListProductByCategoryIdPageing(int categoryId, int page, int pageSize,string sort, out int totalRow);
        IEnumerable<Product> GetReatedProducts(int id, int top);//sản phẫm liên quan
        IEnumerable<string> GetListProductByName(string name);
        IEnumerable<Product> Search(string Keyword, int page, int pageSize, string sort, out int totalRow);
        Product GetById(int id);
        IEnumerable<Product> Getlastest(int top);
        IEnumerable<Product> GetHotProduct(int top);
        IEnumerable<Tag> GetListTagByProductId(int id);
        IEnumerable<Product> GetListProduct(string keyword);
        //viewCount
        void IncreaseView(int id);
        IEnumerable<Product> GetListProductByTag(string Tagid, int page, int pageSize, out int totalRow);

        Tag GetTag(string tagid);
        bool SellProduct(int productId, int quantity);
        void Save();
    }

    public class ProductService : IProductService
    {
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IProductRepository _ProductRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ITagRepository _tagRepository, IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
        {
            this._ProductRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._tagRepository = _tagRepository;
            this._productTagRepository = productTagRepository;
        }

        public Product Add(Product Product)
        {
            var product = _ProductRepository.Add(Product);
            _unitOfWork.Commit();
            if(!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
                _unitOfWork.Commit();
            }
            return product;
        }

        public Product Delete(int id)
        {
            return _ProductRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _ProductRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string Keyword)
        {
            if (!string.IsNullOrEmpty(Keyword))
                return _ProductRepository.GetMulti(x => x.Name.Contains(Keyword) || x.Description.Contains(Keyword));
            else
                return _ProductRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _ProductRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _ProductRepository.GetMulti(x => x.Status == true && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> Getlastest(int top)
        {
            return _ProductRepository.GetMulti(x => x.Status == true ).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            //var model = _ProductRepository.GetMulti(x=>x.Status &&x.ProductTags.Count(y=>y.ProductID==x.ID)>0,new string[] { "ProductCategory", "ProductTags" });
            //totalRow = model.Count();

            var model = _ProductRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);
            return model;
        }

        public IEnumerable<Product> GetListProductByCategoryIdPageing(int categoryId, int page, int pageSize,string sort, out int totalRow)
        {
            var query = _ProductRepository.GetMulti(x => x.Status == true && x.CategoryID == categoryId);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _ProductRepository.GetMulti(x => x.Status==true && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int top)
        {
            var product = _ProductRepository.GetSingleById(id);
            return _ProductRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public void IncreaseView(int id)
        {
            var product = _ProductRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string Keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _ProductRepository.GetMulti(x => x.Status == true && x.Name.Contains(Keyword));
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Product Product)
        {
             _ProductRepository.Update(Product);      
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
               
            }
        }

        public Tag GetTag(string tagid)
        {
            return _tagRepository.GetSingleByCondition(x =>x.ID==tagid);
        }

        public IEnumerable<Product> GetListProduct(string keyword)
        {
            IEnumerable<Product> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _ProductRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                query = _ProductRepository.GetAll();
            return query;
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = _ProductRepository.GetSingleById(productId);
            if (product.Quantity < quantity)
                return false;
            product.Quantity -= quantity;
            return true;
        }
    }
}