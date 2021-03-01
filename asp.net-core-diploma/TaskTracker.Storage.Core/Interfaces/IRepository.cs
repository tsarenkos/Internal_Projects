using System.Collections.Generic;


namespace TaskTracker.Storage.Core.Interfaces
{
    public interface IRepository<T, S> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(S id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
