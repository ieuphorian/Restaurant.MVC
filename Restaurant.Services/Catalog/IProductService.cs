using Restaurant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Catalog
{
    public interface IProductService
    {
        IQueryable<Products> GetAllProducts();
        Products GetProductById(int id);
        IQueryable<Products> GetProductsByCategoryId(int id);
        Products Create(Products product);
        Products Update(Products product);
        void Delete(int id);
    }
}
