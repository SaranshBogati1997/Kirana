using Kirana.Core.Contracts;
using Kirana.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.DataAccess.Sql
{
    //Generic class with placeholder T which implements IRepository where T implements BaseEntity
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity

    {
        /*To make this repository work we need to inject into it our data context class.
         We also need a way of mapping underlying product to product table*/
        internal DataContext context;//this is the context from datacontext class implementing DBContext
        internal DbSet<T> dbSet;//dbset is the underlying table that we want to access
        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {

            var t = Find(Id);//using internal find object to find the object based on id
            if (context.Entry(t).State == EntityState.Detached)//checking the state of the entry
                dbSet.Attach(t);
            /*once the object that we are passing gets attached to the underlying ef we then remove it */

            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T g)
        {
            dbSet.Add(g);
        }

        public void Update(T g)
        {
            /*We are passing in an object and attaching that object to the entity framework table*/
            dbSet.Attach(g);//first we attach our entity that we want to update
            context.Entry(g).State = EntityState.Modified;//then we set the entry to a state of modified.
        }
    }
}
