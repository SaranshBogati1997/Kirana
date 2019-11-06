using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Models
{
    public class Products
    {
        //Products properties definition
        public string Id { get; set; }

        //validation added to Name's Max length and display when scaffolding
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string description { get; set; }

        //Range validation on price
        [Range(0,10000)]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        public Products()//Constructor to initialize the id 
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
