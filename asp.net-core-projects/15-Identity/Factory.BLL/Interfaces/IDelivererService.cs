using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IDelivererService
    {
        List<DelivererModel> GetAllDeliverers();
        DelivererModel GetById(int id);        
        void AddDeliverer(DelivererModel delivererModel);        
    }
}
