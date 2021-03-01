using System.Collections.Generic;

namespace TaskTracker.BL.Interfaces
{
    public interface IGenericListBL<T> 
        where T : class
    {
        IEnumerable<T> GetAll();
    }
}
