using EcoTrend_Industry_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.Repositories
{
    public class SupplierRepo
    {
        EcotrendEntities context = new EcotrendEntities();
        public IEnumerable<SupplierEmailVM> GetAll()
        {
            var query = from s in context.Suppliers
                        join e in context.Emails on s.supplierID equals e.supplierID
                        select new SupplierEmailVM
                        {
                            supplierID = s.supplierID,
                            name = s.name,
                            email = s.email,
                            approved = e.approved,
                            emailID = e.emailID,
                            subject = e.subject,
                            body = e.body
                        };
            return query;
        }

        public SupplierAndSalesRepVM GetAllSalesRepsLockoutDetails()
        {
            SupplierAndSalesRepVM suppliersAndSalesReps = new SupplierAndSalesRepVM();

            suppliersAndSalesReps.SuppliersLockout = from s in context.Suppliers
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

        public IEnumerable<SupplierDetailsVM> GetAllSuppliers()
        {
            IEnumerable<SupplierDetailsVM> suppliers;

            suppliers = (from s in context.Suppliers
                         select new SupplierDetailsVM()
                         {
                             SupplierID = s.supplierID,
                             Name = s.name,
                             Email = s.email,
                             UserRole = "Supplier",
                             LockedOut = (from c in context.AspNetUsers
                                          where s.email == c.Email
                                          select c.LockoutEndDateUtc).FirstOrDefault()
                         });
            return suppliers;
        }


        public SupplierDetailsVM GetSupplier(int supplierID)
        {
            SupplierDetailsVM supplier = (from s in context.Suppliers
                                          where s.supplierID == supplierID
                                          select new SupplierDetailsVM()
                                          {
                                              SupplierID = s.supplierID,
                                              Name = s.name,
                                              Email = s.email,
                                              UserRole = "Supplier",
                                              LockedOut = (from c in context.AspNetUsers
                                                           where s.email == c.Email
                                                           select c.LockoutEndDateUtc).FirstOrDefault(),
                                              EmailHistory = (from eh in context.Emails
                                                              where supplierID == eh.supplierID
                                                              select new EmailVM
                                                              {
                                                                  EmailID = eh.emailID,
                                                                  Subject = eh.subject,
                                                                  Body = eh.body,
                                                                  Approved = eh.approved,
                                                                  DateCreated = eh.dateCreated,
                                                                  DateSent = eh.dateSent
                                                              })
                                   }).FirstOrDefault();
            return supplier;
        }

        public void UpdateSupplier(SupplierDetailsVM supplierVM)
        {
            Supplier supplier = (from s in context.Suppliers
                                 where s.supplierID == supplierVM.SupplierID
                                 select s).FirstOrDefault();

            if (supplier.email != supplierVM.Email)
            {
                AspNetUser user = (from a in context.AspNetUsers
                                   where a.Email == supplier.email
                                   select a).FirstOrDefault();
                user.Email = supplierVM.Email;
                context.SaveChanges();
            }

            supplier.email = supplierVM.Email;
            supplier.name = supplierVM.Name;

            context.SaveChanges();

        }

        public IEnumerable<EmailVM> GetSupplierEmails(int supplierID)
        {
            IEnumerable<EmailVM> emails = (from e in context.Emails
                                           where e.supplierID == supplierID
                                           select new EmailVM
                                           {
                                               EmailID = e.emailID,
                                               Subject = e.subject,
                                               Body = e.body,
                                               Approved = e.approved
                                           });
            return emails;               
        }
    }
}