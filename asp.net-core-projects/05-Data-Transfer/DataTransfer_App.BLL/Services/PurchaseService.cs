using DataTransfer_App.BLL.Interfaces;
using DataTransfer_App.DAL.Entities;
using DataTransfer_App.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataTransfer_App.BLL.Services
{
    public class PurchaseService:IPurchaseService
    {
        public IEnumerable<Purchase> GetAll()
        {
            return PurchaseRepository.Purchases;
        }
        public Purchase GetById(int id)
        {            
            return PurchaseRepository.Purchases.FirstOrDefault(p => p.Id == id); ;
        }
        public void Add(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            Purchase purchase = new Purchase(new Random().Next(1, 1000), name, DateTime.Now);

            while (PurchaseRepository.Purchases.Contains(purchase))
            {
                purchase = new Purchase(new Random().Next(1, 1000), name, DateTime.Now);
            }

            PurchaseRepository.Purchases.Add(purchase);
        }
        public void Edit(int id, string name)
        {
            Purchase purchase = PurchaseRepository.Purchases.FirstOrDefault(p => p.Id == id);
            if (purchase != null)
            {
                purchase.Name = name;
                purchase.DateEdit = DateTime.Now;
            }            
        }
        public void Delete(int id)
        {
            Purchase purchase = PurchaseRepository.Purchases.FirstOrDefault(p => p.Id == id);
            if (purchase != null)
            {
                PurchaseRepository.Purchases.Remove(purchase);
            }            
        }
    }
}
