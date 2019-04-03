using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter_Clone.Models
{
    public class Person
    {
        [Key]
        [Required (ErrorMessage ="Unique user Name is required")]
        [DisplayName("User Name")]
        public string User_Id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Joined Date is required")]
        public DateTime Joined { get; set; } = DateTime.Now;        

        [DefaultValue(1)]
        [Required(ErrorMessage = "Active field is required")]
        public bool Active { get; set; }

        public ICollection<Tweet> Tweet { get; set; }
        public ICollection<Following> Following { get; set; }

      



    }
}