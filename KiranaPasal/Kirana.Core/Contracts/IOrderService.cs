using Kirana.Core.Models;
using Kirana.Core.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<NewBasketItemViewModel> basketItems);

    }
}
