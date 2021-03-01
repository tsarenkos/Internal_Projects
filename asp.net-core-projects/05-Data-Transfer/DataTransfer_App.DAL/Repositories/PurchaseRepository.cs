using DataTransfer_App.DAL.Entities;
using System;
using System.Collections.Generic;


namespace DataTransfer_App.DAL.Repositories
{
    public static class PurchaseRepository
    {
        public static List<Purchase> Purchases { get; set; }

        static PurchaseRepository()
        {
            Purchases = new List<Purchase>()
            {
                new Purchase(1,"Samsung S7",new DateTime(2020,10,05)),
                new Purchase(2,"Samsung S10",new DateTime(2020,10,05)),
                new Purchase(3,"iPhone 6S",new DateTime(2020,10,05)),
                new Purchase(4,"Xiaomi Redmi 9",new DateTime(2020,10,05))
            };
        }
    }
}
