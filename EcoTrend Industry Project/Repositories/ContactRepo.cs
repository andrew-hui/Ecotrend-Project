using EcoTrend_Industry_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.Repositories
{
    public class ContactRepo
    {
        public const string LASTNAME = "LastName";
        public const string FIRSTNAME = "FirstName";
        public const string COMPANY = "Company";

        public IEnumerable<ContactVM> GetAll()
        {
            EcotrendEntities db = new EcotrendEntities();
            IEnumerable<ContactVM> contactSummary;

            contactSummary = from s in db.Stores
                             from c in s.Contacts
                             where (s.storeID == c.storeID)
                             select new ContactVM()
                             {
                                 ContactID = c.contactID,
                                 StoreName = s.shortName,
                                 FirstName = c.firstName,
                                 LastName = c.lastName,
                                 Email = c.email,
                                 Telephone = c.telephone,
                                 Branch = c.branch,
                                 Department = c.department,
                                 StoreID = c.storeID
                             };
            return contactSummary;
        }

        public IEnumerable<ContactVM> GetContactsBySalesRepID(string searchString, string sortOrder, int salesRepID)
        {
            EcotrendEntities context = new EcotrendEntities();
            IEnumerable<ContactVM> contacts = from c in context.Contacts
                                              from s in context.Stores
                                              where c.salesRepID == salesRepID && c.storeID == s.storeID
                                              select new ContactVM
                                              {
                                                  ContactID = c.contactID,
                                                  FirstName = c.firstName,
                                                  LastName = c.lastName,
                                                  Email = c.email,
                                                  Telephone = c.telephone,
                                                  Branch = c.branch,
                                                  Department = c.department,
                                                  StoreID = c.storeID,
                                                  StoreName = s.name
                                              };

            contacts = FilterContacts(contacts, searchString);
            contacts = SortContacts(contacts, sortOrder);

            return contacts;
        }
        public IEnumerable<ContactVM> GetContactsBySalesRep(int salesRepID)
        {
            EcotrendEntities db = new EcotrendEntities();
            IEnumerable<ContactVM> contactSummary;

            contactSummary = from s in db.Stores
                             from c in s.Contacts
                             where (s.storeID == c.storeID)
                             select new ContactVM()
                             {
                                 ContactID = c.contactID,
                                 StoreName = s.shortName,
                                 FirstName = c.firstName,
                                 LastName = c.lastName,
                                 Email = c.email,
                                 Telephone = c.telephone,
                                 Branch = c.branch,
                                 Department = c.department,
                                 StoreID = c.storeID
                             };
            return contactSummary;
        }

        // Being used as a limited alternative where only particular fields are updated by a sales rep
        public ContactVM LimitedUpdate(ContactVM contactVM)
        {
            EcotrendEntities db = new EcotrendEntities();
            Contact contact = db.Contacts.Where(c => c.contactID == contactVM.ContactID).FirstOrDefault();
            contact.firstName = contactVM.FirstName;
            contact.lastName = contactVM.LastName;
            contact.email = contactVM.Email;
            contact.department = contactVM.Department;
            contact.telephone = contactVM.Telephone;
            contact.storeID = (int)contactVM.StoreID;
            db.SaveChanges();
            return contactVM;
        }

        // Being used by Admin/EditContact
        public ContactVM Update(ContactVM contactVM)
        {
            EcotrendEntities db = new EcotrendEntities();
            Contact contact = db.Contacts.Where(c => c.contactID == contactVM.ContactID).FirstOrDefault();
            contact.firstName = contactVM.FirstName;
            contact.lastName = contactVM.LastName;
            contact.email = contactVM.Email;
            contact.department = contactVM.Department;
            contact.branch = contactVM.Branch;
            contact.telephone = contactVM.Telephone;
            contact.Store.shortName = contactVM.StoreName;
            db.SaveChanges();
            return contactVM;
        }

        public bool Delete(int id)
        {
            EcotrendEntities db = new EcotrendEntities();
            Contact contact = db.Contacts.Where(c => c.contactID == id ).FirstOrDefault();
            try
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ContactVM GetContactDetail(int id)
        {
            EcotrendEntities db = new EcotrendEntities();
            Contact contact = db.Contacts.Where(c => c.contactID == id).FirstOrDefault();
            ContactVM contactVm = new ContactVM();
            contactVm.ContactID = contact.contactID;
            contactVm.FirstName = contact.firstName;
            contactVm.LastName = contact.lastName;
            contactVm.Email = contact.email;
            contactVm.Telephone = contact.telephone;
            contactVm.Branch = contact.branch;
            contactVm.Department = contact.department;
            contactVm.StoreName = contact.Store.shortName;

            return contactVm;
        }


        public void AddContact(ContactVM contactVM)
        {
            EcotrendEntities db = new EcotrendEntities();
            Contact contact = new Contact();
            contact.contactID = contactVM.ContactID;
            contact.firstName = contactVM.FirstName;
            contact.lastName = contactVM.LastName;
            contact.email = contactVM.Email;
            contact.telephone = contactVM.Telephone;
            contact.branch = contactVM.Branch;
            contact.department = contactVM.Department;
            //  contact.Store.shortName = contactVM.StoreName;
            contact.storeID = contactVM.StoreID;
            contact.salesRepID = contactVM.SalesRepID;
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public List<string> GetAllEmail()
        {
            EcotrendEntities db = new EcotrendEntities();
            List<Contact> contactList = db.Contacts.ToList();
            // string emailList = null;
            List<string> emailList = new List<string>();
            foreach (Contact item in contactList)
            {
                //string email = item.email;
                emailList.Add(item.email);
                //contactEmailList.Add(email);
                // emailList += email + ";";
                
            }         
            return emailList;
        }

        IEnumerable<ContactVM> SortContacts(IEnumerable<ContactVM> contacts, string sortOrder)
        {
            switch (sortOrder)
            {
                case LASTNAME:
                    contacts = contacts.OrderByDescending(s => s.LastName);
                    break;
                case FIRSTNAME:
                    contacts = contacts.OrderBy(s => s.FirstName);
                    break;
                case COMPANY:
                    contacts = contacts.OrderByDescending(s => s.LastName);
                    break;
                default:
                    contacts = contacts.OrderBy(s => s.FirstName);
                    break;
            }
            return contacts;
        }

        ///
        IEnumerable<ContactVM> FilterContacts(IEnumerable<ContactVM> contacts, string searchString)
        {
            // Filter results based on search.
            if (!String.IsNullOrEmpty(searchString))
                contacts = contacts.Where(
                            s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                              || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            return contacts;
        }

        public IEnumerable<ContactVM> GetContacts(string searchString, string sortOrder)
        {
            EcotrendEntities context = new EcotrendEntities();
            IEnumerable<ContactVM> contacts = from c in context.Contacts
                                              select new ContactVM
                                              {
                                                  SalesRepID = (int)c.salesRepID,
                                                  ContactID = c.contactID,
                                                  FirstName = c.firstName,
                                                  LastName = c.lastName,
                                                  Email = c.email,
                                                  Telephone = c.telephone,
                                                  Branch = c.branch,
                                                  Department = c.department,
                                                  StoreID = c.storeID
                                              };

            contacts = FilterContacts(contacts, searchString);
            contacts = SortContacts(contacts, sortOrder);

            return contacts;
        }

      //  public IEnumerable<AdminAddContactVM> AdminAddContact
     //   {
      //      EcotrendEntities context = new EcotrendEntities();
            
   // }


    //////

    }
}