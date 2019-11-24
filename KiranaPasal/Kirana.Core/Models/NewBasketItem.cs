using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Models
{
    public class NewBasketItem: BaseEntity
    {
        public string BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quanity { get; set; }
    }
}
