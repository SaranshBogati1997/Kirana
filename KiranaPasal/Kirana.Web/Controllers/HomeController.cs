using Kirana.Core.Contracts;
using Kirana.Core.Models;
using Kirana.Core.View_Models;
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

        public ActionResult Index(string Category = null)
        {

            List<Products> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = products,
                ProductCategories = categories
            };


            return View(model);
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