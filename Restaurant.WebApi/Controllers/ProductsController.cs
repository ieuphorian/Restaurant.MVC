using Restaurant.Data;
using Restaurant.Services.Catalog;
using Restaurant.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restaurant.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IQueryable<ProductModel> GetAllProducts(int id)
        {
            return _productService.GetProductsByCategoryId(id).OrderBy(f => f.SortOrder).Select(f => new ProductModel
            {
                Description = f.Description,
                Id = f.Id,
                ImageUrl = f.ImageUrl,
                Name = f.Name,
                SortOrder = f.SortOrder,
                DiscountedPrice = f.DiscountedPrice,
                Price = f.Price
            });
        }

        public ProductModel GetProduct(int id)
        {
            return PrepareProductModel(_productService.GetProductById(id));
        }

        [NonAction]
        private static ProductModel PrepareProductModel(Products product)
        {
            var model = new ProductModel();
            model.Description = product.Description;
            model.Id = product.Id;
            model.ImageUrl = product.ImageUrl;
            model.Name = product.Name;
            model.SortOrder = product.SortOrder;
            model.DiscountedPrice = product.DiscountedPrice;
            model.Price = product.Price;
            return model;
        }
    }
}
