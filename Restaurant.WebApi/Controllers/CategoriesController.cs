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
    public class CategoriesController : ApiController
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IQueryable<CategoryModel> GetAllCategories()
        {
            return _categoryService.GetAllCategories().Where(f => !f.ParentCategoryId.HasValue).OrderBy(f => f.SortOrder).Select(f => new CategoryModel
            {
                Description = f.Description,
                Id = f.Id,
                ImageUrl = f.ImageUrl,
                Name = f.Name,
                SortOrder = f.SortOrder,
                ProductCount = f.Products.Count,
                HasSubCategories = f.SubCategories.Any()
            });
        }

        [HttpGet]
        public IEnumerable<CategoryModel> GetAllCategories(int id)
        {
            return _categoryService.GetSubCategoriesById(id).Select(f => new CategoryModel
            {
                Description = f.Description,
                Id = f.Id,
                ImageUrl = f.ImageUrl,
                Name = f.Name,
                SortOrder = f.SortOrder,
                ProductCount = f.Products.Count,
                HasSubCategories = f.SubCategories.Any()
            });
        }
    }
}
