using FactoryApp.BLL.BusinessModels;
using FactoryApp.BLL.Interfaces;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;
using System;


namespace FactoryApp.BLL.Services
{
    public class DelivererService:IDelivererService
    {
        private readonly IRepository<DelivererEntity> repository;

        public DelivererService(IRepository<DelivererEntity> repository)
        {
            this.repository = repository;
        }

        public void AddDeliverer(DelivererModel deliverer)
        {
            try
            {
                if (deliverer == null)
                {
                    throw new ArgumentNullException();
                }
                DelivererEntity delivererEntity = new DelivererEntity();
                delivererEntity.DelivererId = deliverer.DelivererId;
                delivererEntity.DelivererName = deliverer.DelivererName;

                repository.Create(delivererEntity);
            }
            catch (Exception exc)
            {
                throw exc;
            }            
        }
    }
}
