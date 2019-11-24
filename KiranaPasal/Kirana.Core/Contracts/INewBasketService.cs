using Kirana.Core.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kirana.Core.Contracts
{
    public interface INewBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string Id);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        List<NewBasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        NewBasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        void ClearBasket(HttpContextBase httpContext);
    }
}
