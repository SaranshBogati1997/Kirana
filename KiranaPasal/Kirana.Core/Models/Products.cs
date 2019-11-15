using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Models
{
    public class Products: BaseEntity
    {
        //Products properties definition
        

        //validation added to Name's Max length and display when scaffolding
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }

        //Range validation on price
        [Range(0,100000)]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        
    }
}
