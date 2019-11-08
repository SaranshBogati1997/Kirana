using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.View_Models
{
    public class ProductManagerViewModel//view model to use in product manager view
    {
        public  Products product { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
