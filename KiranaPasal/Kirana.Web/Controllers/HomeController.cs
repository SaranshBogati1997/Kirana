using Kirana.Core.Contracts;
using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kirana.Web.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Products> context;//creating instance of the product repository
        IRepository<ProductCategory> productCategories;//to load categories from the product category repository
        public HomeController(IRepository<Products> prodContext, IRepository<ProductCategory> prodCategoryContext)//init of product repos object to new product repository
        {
            context = prodContext;
            productCategories = prodCategoryContext;
        }

        public ActionResult Index()
        {
            List<Products> products = context.Collection().ToList();//finds products on the db and returns them as list
            return View(products);//the object is passed onto the view to display
        }

        public ActionResult Details(string Id)
        {
            Products product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
            
        }
       
    }
}