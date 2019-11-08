using Kirana.Core.Models;
using Kirana.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kirana.Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        ProductCategoryRepository context;//creating instance of the product repository
        public ProductCategoryController()//init of product repos object to new product repository
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()//will return a list of product as list 
        {
            List<ProductCategory> productCategory = context.Collection().ToList();//

            return View(productCategory);
        }

        public ActionResult Create()//this create is for the user to fill in the details of the product
        {
            ProductCategory productCategory = new ProductCategory();

            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)//this create method is for the details to be posted 
        {
            if (!ModelState.IsValid)//check if the model is valid for the given details
            {
                return View(productCategory);//this is if not valid
            }
            else
            {
                context.Insert(productCategory);//if valid then insert into object of product repository. it contains all the method defined in the class
                context.Commit();//saves the data to memory
                return RedirectToAction("Index");//redirect to index page once data is saved
            }
        }

        public ActionResult Edit(String Id)//shows Initial Edit page to edit
        {
            ProductCategory productToEdit = context.Find(Id);//finds the product based on id in the list
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToEdit);//if product found loads edit page with the product 
            }

        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, String Id)//Edit method to actually post the changes made
        {
            ProductCategory productToEdit = context.Find(Id);//finds the product in the repository
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else//if found first checks if the product detail is correctly validated
            {
                if (!ModelState.IsValid)//if not valid return View only
                {
                    return View(productToEdit);
                }
                else//if model is valid manually update the product to edit object with the details provided by the user via the UI.
                {
                    productToEdit.Category = productCategory.Category;
                    

                    context.Commit();//saving the changes to memory

                    return RedirectToAction("Index");
                }

            }
        }

        public ActionResult Delete(String Id)//Prompt delete page. This only returns view
        {
            ProductCategory productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]//this selector states that delete and ConfirmDelete are the same method 
        public ActionResult ConfirmDelete(ProductCategory productCategory, String Id)
        {
            ProductCategory productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else//if product is found delete the product and commit changes.
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        } 
    }
}