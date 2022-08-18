using Restaurant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Catalog
{
    public interface ICategoryService
    {
        IQueryable<Categories> GetAllCategories();
        Categories GetCategoryById(int id);
        IQueryable<Products> GetProductsByCategoryId(int id);
        Categories Create(Categories category);
        Categories Update(Categories category);
        void Delete(int id);
        IEnumerable<Categories> GetSubCategoriesById(int id);
    }
}
