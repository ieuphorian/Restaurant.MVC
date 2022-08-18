using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Data;

namespace Restaurant.Services.Catalog
{
    public class ProductService : IProductService
    {
        private AppDbContext _dbContext;
        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Products Create(Products product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Products> GetAllProducts()
        {
            return _dbContext.Products;
        }

        public Products GetProductById(int id)
        {
            return _dbContext.Products.SingleOrDefault(f => f.Id == id);
        }

        public IQueryable<Products> GetProductsByCategoryId(int id)
        {
            return _dbContext.Products.Where(f => f.CategoryID == id);
        }

        public Products Update(Products product)
        {
            _dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return product;
        }
    }
}
