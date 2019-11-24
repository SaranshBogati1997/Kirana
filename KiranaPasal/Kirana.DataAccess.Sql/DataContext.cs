using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.DataAccess.Sql
{
    public class DataContext:DbContext
    {
        public DataContext()
            :base("DefaultConnection")
            //created so that we can capture and pass in the connection string onto the base class is expecting
        {
            
        }
        /*DbSet is used to store only the required models in the database*/
        //for wg we don't need to store viewmodels in the database
         public DbSet<Products> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<NewBasket> NewBaskets { get; set; }

        public DbSet<NewBasketItem> NewBasketItems { get; set; }


    }
}
