using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Models
{
    public class NewBasket : BaseEntity
    {
        
        public virtual ICollection<NewBasketItem> BasketItems { get; set; }

        public NewBasket()
        {
            this.BasketItems = new List<NewBasketItem>();
        }
    }
}
