﻿using Juno.Data.Infrastructure;
using Juno.Data.Repositories;
using Juno.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juno.server
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);
        void Update(ProductCategory ProductCategory);
        ProductCategory Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string Keyword);
        IEnumerable<ProductCategory> GetAllByParentId(int parentId);
        ProductCategory GetById(int id);
        void Save();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _ProductCategoryRepository;
        IUnitOfWork _unitOfWork;
        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
        public ProductCategory Add(ProductCategory ProductCategory)
        {
            return _ProductCategoryRepository.Add(ProductCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _ProductCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _ProductCategoryRepository.GetMulti(x=>x.HomeFlag==true);
        }

        public IEnumerable<ProductCategory> GetAll(string Keyword)
        {
            if (!string.IsNullOrEmpty(Keyword))
                return _ProductCategoryRepository.GetMulti(x => x.Name.Contains(Keyword) || x.Description.Contains(Keyword));
            else
                return _ProductCategoryRepository.GetAll();

        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public ProductCategory GetById(int id)
        {
            return _ProductCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            _ProductCategoryRepository.Update(ProductCategory);
        }


    }
}
