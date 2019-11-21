using Kirana.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kirana.Web.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;//init interface variable to use its methods
        public BasketController(IBasketService BasketService)//allows value of passed arguement to be stored in the variable 
        {
            this.basketService = BasketService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            /*using the getbasketItem method of the interface
            via basketService variable and passing the httpContext of the current object 'model'*/
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);//passing the model onto the view to display
        }
        public ActionResult AddToBasket(string id)
        {
            basketService.AddToBasket(this.HttpContext, id);//passing the id and httpContext of current object onto AddToBasket method
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string id)//same as above
        {
            basketService.RemoveFromBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }


    }
}