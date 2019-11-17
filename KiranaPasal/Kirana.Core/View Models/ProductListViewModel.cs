using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.View_Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Products> products { get; set; }//IEnumerable because we want a list of product

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
