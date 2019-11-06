using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Kirana.Core;
using Kirana.Core.Models;

namespace Kirana.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;//creating cache to store data in memory

        List<Products> products;//init list of products 

        public ProductRepository()//constructor to check for in memory product on each run
        {
            products = cache["products"] as List<Products>;//if there is cache called products then return as list of products
            if(products == null)//if null init products as list
            {
                products = new List<Products>();
            }
        }

        public void Commit()//saves data in products object to in memory cache before saving in server
        {
            cache["products"] = products;
        }
        public void Insert(Products p)
        {
            products.Add(p);
        }

        public void Update( Products prod)
        {
            Products ProductToUpdate = products.Find(p => p.Id == prod.Id);//Search through list based on id

            if(ProductToUpdate != null)//if non empty then store product data on productToUpdate object
            {
                ProductToUpdate = prod;
            }
            else
            {
                throw new Exception("Product not found");//else throw exception
            };
        }

        public Products Find(String Id)
        {
            Products ProductToFind = products.Find(p => p.Id == Id);//Search through list based on id

            if (ProductToFind != null)//if non empty then return productToFind
            {
                return ProductToFind;
            }
            else
            {
                throw new Exception("Product not found");//else throw exception
            };
        }

        public  IQueryable<Products> Collection()//returns list of product. this will return a list that can be queryed
        {
            return products.AsQueryable();
        }

        public void Delete(String Id)
        {   
            Products ProductToDelete = products.Find(p => p.Id == Id);//search through the list based on ID.

            if( ProductToDelete != null)
            {
                products.Remove(ProductToDelete);//if product found on list will delete the list.
            }
            else
            {
                throw new Exception("Unable to find Product");//else will throw an exception.
            }
        }
    }
}
