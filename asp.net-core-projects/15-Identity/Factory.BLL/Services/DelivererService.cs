using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;


namespace Factory.BLL.Services
{
    public class DelivererService:IDelivererService
    {
        private readonly IUnitOfWork unitOfWork;
        public DelivererService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<DelivererModel> GetAllDeliverers()
        {
            var deliverers = unitOfWork.Deliverers.GetAll();
            if (deliverers == null)
            {
                return null;
            }

            List<DelivererModel> delivererModels = new List<DelivererModel>();            
            foreach (var item in deliverers)
            {
                DelivererModel deliverer = new DelivererModel();
                deliverer.DelivererId = item.DelivererId;
                deliverer.DelivererName = item.DelivererName;
                delivererModels.Add(deliverer);
            }
            
            return delivererModels;
        }        

        public void AddDeliverer(DelivererModel delivererModel)
        {
            try
            {
                if (delivererModel == null)
                {
                    throw new ArgumentNullException();
                }

                Deliverer deliverer = new Deliverer();
                deliverer.DelivererName = delivererModel.DelivererName;

                unitOfWork.Deliverers.Create(deliverer);
                unitOfWork.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }         
        }

        public DelivererModel GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            Deliverer deliverer = unitOfWork.Deliverers.GetById(id);
            if (deliverer == null)
            {
                return null;
            }
            DelivererModel delivererModel = new DelivererModel
            {
                DelivererId = deliverer.DelivererId,
                DelivererName = deliverer.DelivererName
            };
            return delivererModel;
        }
    }
}
