using Kirana.Core.Contracts;
using Kirana.Core.Models;
using Kirana.Core.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kirana.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Products> productContext;//variable declaration to use the underling database of product and basket
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";
        //to maintain consistency while reading or writing cookies we use a constant string 
        
        public BasketService(IRepository<Products> ProductContext, IRepository<Basket> BasketContext)//constructor 
        {
            this.basketContext = BasketContext;
            this.productContext = ProductContext;
        }
        /*HttpContext is used to gather info of the user's connection to the server like the Ip add,list of cookies
         we want to load the cookies of the user to read the basketId anduse that basketId to then read the basektId from the db
             */
        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)    
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);//reading the cookie
            Basket basket = new Basket();//new basket creation
            if(cookie != null)//checking if previous cookie exists or is the user visiting for the first time
            {//if the user has visited before
                string basketId = cookie.Value;
                //if they have a cookie getting the value from the cookie and storing in basketId var

                if (!string.IsNullOrEmpty(basketId))//recheck of the value of cookie
                {//if non empty i.e. the cookie's value exist 
                    basket = basketContext.Find(basketId);//loading the data from the db based on basketId from the cookie

                }
                else//if the basketId was null
                {
                    if (createIfNull)//if createIfNull is true create a new basket
                    {
                        basket = CreateNewBasket(httpContext);//method to create new basket 
                    }
                }
            }
            else//same as above
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;//after creation of basket returns the basket
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();//creating new basket  
            basketContext.Insert(basket);//inserting basket into db
            basketContext.Commit();//and update db
            //following method is for writing a cookie onto the user's machine

            HttpCookie cookie = new HttpCookie(BasketSessionName);//first we define a cookie with default name
            cookie.Value = basket.Id;//adding the value to the cookie
            cookie.Expires = DateTime.Now.AddDays(2);//adding an expiration date for 2 days
            httpContext.Response.Cookies.Add(cookie);//adding that cookie to the httpcontext response 

            return basket;//returning the basket we just created
        }
        //method for adding the item onto basket
        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            //we always want the basket to be created first to add items onto basket so bool part is always true

            //checking to see if there is already an item in the basketitem in the user's basket with this product id
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)//does that item exist in the basket
            {
                item = new BasketItem()//if it doesnt then we want to create a new item
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                basket.BasketItems.Add(item);//adding onto the basket
            }
            else//if item already exist in the basket then just increament the count 
            {
                item.Quantity = item.Quantity + 1;

            }
            basketContext.Commit();//commiting all the changes we've made 
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string ItemId)
        {
            Basket basket = GetBasket(httpContext, true);//getting the basket
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == ItemId);//checking if item exists in the basket
            if(item != null)//if exist remove it
            {   
                basket.BasketItems.Remove(item);
                basketContext.Commit();//and commit changes
            }
           
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);//get our basket from the db
                                                          //because we want to retrieve the items from the basket if there's no basket we don't want to create one so false 

            if (basket != null)//if there's basket available 
            {
                /*LINQ Expressions
                 inner joining basketItems and productContext in the db on the basis of product id present on both the table
                 */
                var result = (from b in basket.BasketItems
                              join p in productContext.Collection() on b.ProductId equals p.Id
                              //once we have our join we then tell what we want from the collection
                              select new BasketItemViewModel()//we are creating a new view model w/ the following props
                              {
                                  id = b.Id,//id from the basketItems
                                  Quantity = b.Quantity,//also from basketItem
                                  ProductName = p.Name,//the rest from the products table in the db
                                  Image = p.Image,
                                  Price = p.Price
                              }

                              ).ToList();//and return as list
                return result;//returning the viewmodel
            }
            else
            {
                return new List<BasketItemViewModel>();//if basket is null create new empty list of basket items
            }
        } 
         public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)//gets the summary of the basket
        {
            Basket basket = GetBasket(httpContext, false);//getting our basket from the db
            BasketSummaryViewModel model = new BasketSummaryViewModel(0,0);
            //creating a summary of basket with 0 quantity and 0 value
            if(basket != null)//if abasket is non empty
            {
                /* ? after data type declaration allows for storing of null values*/
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();//select only the no. of items in the basketItems and sum them

                /* for total value of basket innerjoining basketItem table and product table on id.
                 then selecting quantity of item in basket item multiplied by price of item present in product table and 
                 summing them
                 */
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection() on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();
                
                model.BasketCount = basketCount ?? 0;//if basketCount has value return the value else return zero 
                model.BasketTotal = basketTotal ?? decimal.Zero;//same as above

                return model;//model return gareko 
            }
            else
            {
                return model;//if basket is empty return the empty model 
            }
        }

    }
}
