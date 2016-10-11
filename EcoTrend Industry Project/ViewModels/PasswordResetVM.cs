using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class PasswordResetVM
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "User Type")]
        public string UserRole { get; set; }
        [Display(Name = "Lockout Status")]
        public DateTime? LockoutStatus { get; set; }
        [Display(Name = "Failed Access Count")]
        public int AccessFailedCount { get; set; }
    }
}