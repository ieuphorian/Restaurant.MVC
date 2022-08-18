using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Data;

namespace Restaurant.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private AppDbContext _dbContext;
        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Categories Create(Categories category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        public void Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category != null)
            {
                category.Products.Clear();
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Categories> GetAllCategories()
        {
            return _dbContext.Categories;
        }

        public Categories GetCategoryById(int id)
        {
            return _dbContext.Categories.SingleOrDefault(f => f.Id == id);
        }

        public IQueryable<Products> GetProductsByCategoryId(int id)
        {
            return _dbContext.Products.Where(f => f.CategoryID == id);
        }

        public IEnumerable<Categories> GetSubCategoriesById(int id)
        {
            return _dbContext.Categories.SingleOrDefault(f => f.Id == id).SubCategories.OrderBy(f => f.SortOrder);
        }

        public Categories Update(Categories category)
        {
            _dbContext.Entry(category).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return category;
        }
    }
}
