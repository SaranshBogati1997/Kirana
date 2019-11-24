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
        public IEnumerable<Products> Products { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
