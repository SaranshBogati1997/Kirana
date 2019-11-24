using Kirana.Core.Contracts;
using Kirana.Core.Models;
using Kirana.Core.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> OrderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.OrderContext = OrderContext;
        }

        public void CreateOrder(Order baseOrder, List<NewBasketItemViewModel> basketItems)
        {
            foreach (var items in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = items.Id,
                    ProductName = items.ProductName,
                    Price = items.Price,
                    Quantity = items.Quanity,
                    Image = items.Image
                });
            }
            OrderContext.Insert(baseOrder);
            OrderContext.Commit();
        }

        
    }
}
