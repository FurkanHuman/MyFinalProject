using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entites.Concrete;
using Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        const sbyte hours = 5;
        readonly IProductDal _productDal;
        readonly ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]

        public IResult Add(Product product)
        {
            BusinessRules.Run(CheckİfProductCountOfCategoryId(product.CategoryId),
                NoEquelName(product.ProductName),CheckİfCategoryLimitExd());
            _productDal.Add(product);
            return new Result(true, Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == hours)
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == Id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == hours)
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new Result(true, Messages.ProductAdded);
        }

        private IResult CheckİfProductCountOfCategoryId(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                if (true)
                {
                    return new ErrorResult(Messages.ProductOfCountCategoryError);
                }
            }
            return new SuccessResult();
        }

        private IResult NoEquelName(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckİfCategoryLimitExd()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
                return new ErrorResult(Messages.CategoryLimitExp);
            return new SuccessResult();
        }

    }
}
