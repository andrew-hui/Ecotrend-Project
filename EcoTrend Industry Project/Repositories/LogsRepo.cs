using EcoTrend_Industry_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.Models
{
    public class LogsRepo
    {
        EcotrendEntities context = new EcotrendEntities();

        public IEnumerable<LogContactVM> GetAllVM(int id, int contactId)
        {
            EcotrendEntities context = new EcotrendEntities();
            var logs = (from l in context.Logs
                        join c in context.Contacts on l.contactID equals c.contactID
                        where l.salesRepID == id && l.contactID == contactId
                        select new LogContactVM
                        {
                            logID = l.logID,
                            logDate = l.logDate,
                            salesRepID = l.salesRepID,
                            title = l.title,
                            body = l.body,
                            storeName = l.Contact.Store.name,
                            contactName = c.firstName + " " + c.lastName,
                            contactID = l.contactID
                        });
            return logs;
        }

        public LogContactVM GetLogVM(int id)
        {
            EcotrendEntities context = new EcotrendEntities();
            var log = (from l in context.Logs
                       join c in context.Contacts on l.contactID equals c.contactID
                       where l.logID == id
                       select new LogContactVM
                       {
                           logID = l.logID,
                           logDate = l.logDate,
                           salesRepID = l.salesRepID,
                           contactID = l.contactID,
                           title = l.title,
                           body = l.body,
                           storeName = l.Contact.Store.name,
                           contactName = l.Contact.firstName + " " + l.Contact.lastName
                       }).FirstOrDefault();
            return log;
        }
        public Log AddLog(string title, string body, int contactID, string salesRepName)
        {
            EcotrendEntities context = new EcotrendEntities();
            Log log = new Log();
            log.title = title;
            log.body = body;
            log.contactID = contactID;
            log.logDate = DateTime.Now;
            log.salesRepName = salesRepName;
            return log;
        }

        public IEnumerable<LogContactVM> ContactName(string firstName, string lastName)
        {
            EcotrendEntities context = new EcotrendEntities();

            var log = (from l in context.Logs
                       select new LogContactVM
                       {
                           contactName = firstName + " " + lastName
                       });
            return log;
        }

        public string GetSalesRepName(int username)
        {

            var salesRepName = (from s in context.SalesReps
                                where s.salesRepID == username
                                select new 
                                {
                                    salesRepName = s.name
                                }).FirstOrDefault();

            return salesRepName.salesRepName;
        }

        public IEnumerable<NotesVM> GetLogsBySalesID(int salesRepID)
        {
            IEnumerable<NotesVM> notes = (from n in context.Logs
                                          where n.salesRepID == salesRepID
                                          select new NotesVM
                                          {
                                              NoteID = n.logID,
                                              FirstName = n.Contact.firstName,
                                              LastName = n.Contact.lastName,
                                              Title = n.title,
                                              Body = n.body,
                                              StoreName = n.Contact.Store.name
                                          });
            return notes;
        }

        public IEnumerable<NotesVM> GetLogsBySalesIDContactID(int salesRepID, int contactID)
        {
            IEnumerable<NotesVM> notes = (from n in context.Logs
                                          where n.salesRepID == salesRepID && n.contactID == contactID
                                          select new NotesVM
                                          {
                                              NoteID = n.logID,
                                              FirstName = n.Contact.firstName,
                                              LastName = n.Contact.lastName,
                                              Title = n.title,
                                              Body = n.body,
                                              StoreName = n.Contact.Store.name
                                          });
            return notes;
        }

        public IEnumerable<NotesVM> GetLogsByContactID(int contactID)
        {
            IEnumerable<NotesVM> notes = (from n in context.Logs
                                          where n.contactID == contactID
                                          select new NotesVM
                                          {
                                              NoteID = n.logID,
                                              FirstName = n.Contact.firstName,
                                              LastName = n.Contact.lastName,
                                              Title = n.title,
                                              Body = n.body,
                                              StoreName = n.Contact.Store.name
                                          });
            return notes;
        }
    }
}