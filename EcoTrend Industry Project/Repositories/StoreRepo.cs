using EcoTrend_Industry_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.Repositories
{
    public class StoreRepo
    {

        EcotrendEntities context = new EcotrendEntities();

        public string GetStoreNameByID(int storeID)
        {
            string storeName;
            Store store = (from s in context.Stores
                           where storeID == s.storeID
                           select s).FirstOrDefault();
            storeName = store.name;
            return storeName;
        }

        public ContactVM GetStoresUnderContactVM()
        {
            EcotrendEntities context = new EcotrendEntities();
            ContactVM stores = (from s in context.Stores
                                select new ContactVM
                                {
                                    Stores = from c in context.Stores
                                             select c
                                }).FirstOrDefault();
            return stores;
        }


    }
}