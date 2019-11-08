using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.DataAccess.InMemory
{
   public  class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;//creating cache to store data in memory

        List<ProductCategory> productCategory;//init list of products 

        public ProductCategoryRepository()//constructor to check for in memory product on each run
        {
            productCategory = cache["productCategory"] as List<ProductCategory>;//if there is cache called products then return as list of products
            if (productCategory == null)//if null init products as list
            {
                productCategory = new List<ProductCategory>();
            }
        }

        public void Commit()//saves data in products object to in memory cache before saving in server
        {
            cache["productCategory"] = productCategory;
        }
        public void Insert(ProductCategory p)
        {
            productCategory.Add(p);
        }

        public void Update(ProductCategory prod)
        {
            ProductCategory ProductToUpdate = productCategory.Find(p => p.Id == prod.Id);//Search through list based on id

            if (ProductToUpdate != null)//if non empty then store product data on productToUpdate object
            {
                ProductToUpdate = prod;
            }
            else
            {
                throw new Exception("Product not found");//else throw exception
            };
        }

        public ProductCategory Find(String Id)
        {
            ProductCategory ProductToFind = productCategory.Find(p => p.Id == Id);//Search through list based on id

            if (ProductToFind != null)//if non empty then return productToFind
            {
                return ProductToFind;
            }
            else
            {
                throw new Exception("Category not found");//else throw exception
            };
        }

        public IQueryable<ProductCategory> Collection()//returns list of product. this will return a list that can be queryed
        {
            return productCategory.AsQueryable();
        }

        public void Delete(String Id)
        {
            ProductCategory ProductToDelete = productCategory.Find(p => p.Id == Id);//search through the list based on ID.

            if (ProductToDelete != null)
            {
                productCategory.Remove(ProductToDelete);//if product found on list will delete the list.
            }
            else
            {
                throw new Exception("Unable to find Category");//else will throw an exception.
            }
        }
    }
}
