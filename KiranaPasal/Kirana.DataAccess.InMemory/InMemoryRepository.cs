using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.DataAccess.InMemory
{
    /*This is a generic class made for reuse when needed later*/
    public class InMemoryRepository<generics> where generics : BaseEntity//creating a base repository for reuse where object generics inherits from BaseEntity class
    {
        ObjectCache cache = MemoryCache.Default;//creating default cache memory object
        List<generics> items;//init of internal list 
        string className;//internal objectfor storing different classname in the cache

        public InMemoryRepository()
        {
            className = typeof(generics).Name;//reflection.This line of code gets the Name of the class
            items = cache[className] as List<generics>;//init of items and checking to see if there is anything in the cache and return if any
            if(items == null)
            {
                items = new List<generics>();//if cache is empty initialize the items as list
            }
           
        }
        public void Commit()//method to store inside the cache
        {
            cache[className] = items;//stores items in the cache 
        }

        public void Insert(generics g)//inserts items 
        {
            items.Add(g);
        }
        public void Update(generics g)
        {
            generics itemToUpdate = items.Find(i => i.Id == g.Id);//finds the item based on id in the list
            if (itemToUpdate != null)//if item is returned update the itemToUpdate object
            {
                itemToUpdate = g;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

        public generics Find(String Id) 
        {
            generics g = items.Find(i => i.Id == Id);
            if(g != null)
            {
                return g;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

        public IQueryable<generics> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(String Id)
        {
            generics itemToDelete = items.Find(i => i.Id == Id);
            if(itemToDelete != null)
            {
                items.Remove(itemToDelete);
            } 
            else
            {
                throw new Exception(className + "Not Found");
            }
        }
         
    }

}
