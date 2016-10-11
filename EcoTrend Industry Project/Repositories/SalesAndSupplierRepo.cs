using EcoTrend_Industry_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoTrend_Industry_Project.Repositories
{
    class SalesAndSupplierRepo
    {
        EcotrendEntities context = new EcotrendEntities();

        public SupplierAndSalesRepVM GetAllSalesRepsPasswordReset()
        {
            SupplierAndSalesRepVM suppliersAndSalesReps = new SupplierAndSalesRepVM();
            suppliersAndSalesReps.SalesRepsPasswordReset = from s in context.SalesReps
                        select new PasswordResetVM()
                        {
                            Id = s.salesRepID,
                            Email = s.email,
                            AccessFailedCount = (from c in context.AspNetUsers
                                                 where s.email == c.Email
                                                 select c.AccessFailedCount).FirstOrDefault(),
                            UserRole = "Sales Representative",
                            LockoutStatus = (from c in context.AspNetUsers
                                             where s.email == c.Email
                                             select c.LockoutEndDateUtc).FirstOrDefault()
                        };
            suppliersAndSalesReps.SuppliersPasswordReset = from s in context.Suppliers
                                              select new PasswordResetVM()
                                              {
                                                  Id = s.supplierID,
                                                  Email = s.email,
                                                  AccessFailedCount = (from c in context.AspNetUsers
                                                                       where s.email == c.Email
                                                                       select c.AccessFailedCount).FirstOrDefault(),
                                                  UserRole = "Supplier",
                                                  LockoutStatus = (from c in context.AspNetUsers
                                                                   where s.email == c.Email
                                                                   select c.LockoutEndDateUtc).FirstOrDefault()
                                              };
            return suppliersAndSalesReps;
        }
    }
}
