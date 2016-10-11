using EcoTrend_Industry_Project.BusinessLogic;
using EcoTrend_Industry_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.Repositories
{
    public class EmailRepo
    {
        EcotrendEntities context = new EcotrendEntities();
        public void MassSend(List<string> contactList, string sender, string title, string body)
        {
            var test = contactList.ToArray();
            for (int i = 0; i < contactList.Count(); i++)
            {
                MailHelper mailer = new MailHelper();
                string response = mailer.EmailFromArvixe(new Message(sender, title, body, contactList[i]));
            }
        }

        public void DeleteEmail(int emailID)
        {
            Email email = (from e in context.Emails
                     where emailID == e.emailID
                     select e).FirstOrDefault();

            if (email != null) {
                context.Emails.Remove(email);
                context.SaveChanges();
            }
        }

        public EmailVM GetEmail(int emailID)
        {
            EmailVM email = (from e in context.Emails
                           where emailID == e.emailID
                           select new EmailVM
                           {
                               EmailID = e.emailID,
                               Subject = e.subject,
                               Body = e.body, 
                               Approved = e.approved, 
                               DateCreated = e.dateCreated,
                               DateSent = e.dateSent,
                               SupplierID = e.supplierID
                           }).FirstOrDefault();
            return email;
        }

        public void UpdateEmail(EmailVM updatedEmail)
        {
            Email email = (from e in context.Emails
                           where updatedEmail.EmailID == e.emailID
                           select e).FirstOrDefault();

            if (updatedEmail.Subject != null && updatedEmail.Body != null)
            {
                email.subject = updatedEmail.Subject;
                email.body = updatedEmail.Body;
                context.SaveChanges();
            }



        }

        public void CreateEmail(EmailVM newEmail)
        {
            Email email = new Email();
            email.subject = newEmail.Subject;
            email.body = newEmail.Body;
            email.approved = false;
            email.dateCreated = DateTime.Now;
            email.supplierID = newEmail.SupplierID;
            if (newEmail.Subject != null && newEmail.Body != null)
            {
                context.Emails.Add(email);
                context.SaveChanges();
            }
        }
    }
}