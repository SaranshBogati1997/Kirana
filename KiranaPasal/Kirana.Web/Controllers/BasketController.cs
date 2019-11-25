using Kirana.Core.Contracts;
using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kirana.Web.Controllers
{
    public class BasketController : Controller
    {
        INewBasketService basketService;
        IOrderService orderService;
        IRepository<Customer> customers;

        public BasketController(INewBasketService BasketService,IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }
        // GET: Basket2
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if(customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    Country = customer.Country,
                    District = customer.District,
                    City = customer.City,
                    ZipCode = customer.ZipCode,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName

                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItem = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Processing Order";
            order.Email = User.Identity.Name;

            //payment
            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItem);
            basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou",new { OrderId = order.Id});
        }
        public ActionResult ThankYou(String OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}