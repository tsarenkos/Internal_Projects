using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.DAL;
using Factory.DAL.Entities;


namespace Factory.BLL.Services
{
    public class DelivererService:IDelivererService
    {       
        public void AddDeliverer(DelivererModel delivererModel)
        {
            if (delivererModel != null)
            {
                using (FactoryContext context = new FactoryContext())
                {
                    Deliverer deliverer = new Deliverer();
                    deliverer.DelivererName = delivererModel.DelivererName;
                    context.Deliverers.Add(deliverer);
                    context.SaveChanges();
                }
            }            
        }
    }
}
