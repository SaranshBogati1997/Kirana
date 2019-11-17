using System.Linq;
using Kirana.Core.Models;

namespace Kirana.Core.Contracts
{
    public interface IRepository<generics> where generics : BaseEntity
    {
        /*This is a InMemory repository interface that contains all the methods that we want to use. */
        IQueryable<generics> Collection();//defined as Iqueryable to allow for filtering 
        void Commit();
        void Delete(string Id);
        generics Find(string Id);
        void Insert(generics g);
        void Update(generics g);
    }
}