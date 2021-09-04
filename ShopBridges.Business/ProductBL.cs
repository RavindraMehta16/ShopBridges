using ShopBridges.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ShopBridges.Business
{
    public class ProductBL
    {
        private readonly ShopBridgesContext appdbContext = null;
        public ProductBL(ShopBridgesContext _dbContext)
        {
            appdbContext = _dbContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = null;

            products = await appdbContext.Products.ToListAsync();

            return products;
        }

        public async Task<bool> SaveProduct(Product product)
        {
            bool result = false;

            appdbContext.Products.Add(product);
            var count = await appdbContext.SaveChangesAsync();

            if (count > 0)
            {
                return true;
            }

            return result;
        }

        public async Task<bool> ModifyProuct(Product product)
        {
            bool result = false;

            Product existingProduct = await appdbContext.Products.FindAsync(product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;

                appdbContext.Products.Update(existingProduct);
                var count = await appdbContext.SaveChangesAsync();

                if (count > 0)
                {
                    return true;
                }
            }
            else
            {
               throw new Exception("No product found with ProductID: " + product.ProductId);
            }
            
            return result;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            bool result = false;

            Product product = appdbContext.Products.Where(x => x.ProductId == productId).FirstOrDefault();

            if (product != null)
            {
                appdbContext.Products.Remove(product);
                var count = await appdbContext.SaveChangesAsync();

                if (count > 0)
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("No product found with ProductID: " + productId);
            }

            return result;
        }
    }
}
