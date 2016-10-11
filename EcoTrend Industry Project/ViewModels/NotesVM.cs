using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class NotesVM
    {
        [DisplayName("Note ID")]
        public int NoteID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date Logged")]
        public DateTime NoteDate { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Body")]
        public string Body { get; set; }
        [DisplayName("Company Name")]
        public string StoreName { get; set; }


    }
}