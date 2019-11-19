using Kirana.Core.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kirana.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);
        void RemoveFromBasket(HttpContextBase httpContext, string ItemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);

    }
}
