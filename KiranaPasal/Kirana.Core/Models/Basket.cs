using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Models
{
    public class Basket : BaseEntity
    {
    public virtual ICollection<BasketItem> BasketItems { get; set; }
        /*LazyLoading
         * By setting this to virtual ICollection whenever Entity Framework will try to load Basket from the db it 
         will automatically load all the basketItems as well*/

        public Basket()//constructor that creates an empty list of basketItem on creation
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
