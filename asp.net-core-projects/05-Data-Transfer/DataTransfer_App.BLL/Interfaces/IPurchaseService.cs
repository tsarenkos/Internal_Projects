using DataTransfer_App.DAL.Entities;
using System.Collections.Generic;


namespace DataTransfer_App.BLL.Interfaces
{
    public interface IPurchaseService
    {
        IEnumerable<Purchase> GetAll();
        Purchase GetById(int id);
        void Add(string name);
        void Edit(int id, string name);
        void Delete(int id);
    }
}
