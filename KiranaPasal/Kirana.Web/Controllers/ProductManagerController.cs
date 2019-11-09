using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kirana.Core.Models;
using Kirana.Core.View_Models;
using Kirana.DataAccess.InMemory;

namespace Kirana.Web.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Products> context;//creating instance of the product repository
        InMemoryRepository<ProductCategory> productCategories;//to load categories from the product category repository
        public ProductManagerController()//init of product repos object to new product repository
        {
            context = new InMemoryRepository<Products>();
            productCategories = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()//will return a list of product as list 
        {
            List<Products> products = context.Collection().ToList();//

            return View(products);
        }

        public ActionResult Create()//this create is for the user to fill in the details of the product
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();//creating instance of view model

            viewModel.product = new Products();//passing product info in viewModel object
            viewModel.ProductCategories = productCategories.Collection();//loading category info into viewModel Object.
            return View(viewModel);//passing view model onto create page
        }

        [HttpPost]
        public ActionResult Create(Products product)//this create method is for the details to be posted 
        {
            if (!ModelState.IsValid)//check if the model is valid for the given details
            {
                return View(product);//this is if not valid
            }
            else
            {
                context.Insert(product);//if valid then insert into object of product repository. it contains all the method defined in the class
                context.Commit();//saves the data to memory
                return RedirectToAction("Index");//redirect to index page once data is saved
            }
        }

        public ActionResult Edit(String Id)//shows Initial Edit page to edit
        {
            Products productToEdit = context.Find(Id);//finds the product based on id in the list
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();

                viewModel.product = productToEdit;//puting product info in viewModel
                viewModel.ProductCategories = productCategories.Collection();//Loading collection of category from collection
                return View(viewModel);//passing viewModel onto the edit page 
            }
            
        }
        [HttpPost]
        public ActionResult Edit(Products product, String Id)//Edit method to actually post the changes made
        {
            Products productToEdit = context.Find(Id);//finds the product in the repository
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
                productToEdit.Category = product.Category;
                productToEdit.description = product.description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.Image = product.Image;

                context.Commit();//saving the changes to memory

                return RedirectToAction("Index");
                }
                
            }
        }

        public ActionResult Delete(String Id)//Prompt delete page. This only returns view
        {
            Products productToDelete = context.Find(Id);

            if(productToDelete == null)
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
    public ActionResult ConfirmDelete(Products product, String Id)
        {
            Products productToDelete = context.Find(Id);

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