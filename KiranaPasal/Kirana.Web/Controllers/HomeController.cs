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

        public ActionResult Index(string Category = null)//this makes the category variable optional
        {
            List<Products> products;//initializing empty list of products
            List<ProductCategory> categories = productCategories.Collection().ToList();//loading category list from the db as list

            if(Category == null)
            /*this checks if the category at the start of index is null(default) or certain category is passed*/
            {
                products = context.Collection().ToList(); //if category is null display all the products

            }
            else//if some category is selected view only the filtered products under that category
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();//returns filtered content as list
                /*Collection is IQuereyable which allows for the use of filters.
                Whereis a LINQ expression that allows to filter based on the conditions inside parenthesis
                inside parenthesis is lambda expression.
                
             */
            }
            ProductListViewModel model = new ProductListViewModel();// init of view model to pass onto the view since two models are being used
            model.products = products;//assigning products data onto the viewmodel's products
            model.ProductCategories = categories;//same as above

            return View(model);//the object is passed onto the view to display
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