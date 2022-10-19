using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PSTU_Automation1.Models.Enums;

namespace PSTU_Automation1.ViewModels
{
    public class RegisterViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        //[ValidEmailDomain(allowedDomain: "pstu.ac.bd", 
        //    ErrorMessage ="Email domain must be pstu.ac.bd")]
        public string Email { get; set; }
        //[Display(Name = "Confirm Email")]
        //[Compare("Email",
        //    ErrorMessage = "Email and confirm Email do not match.")]
        //public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }


        //[Display(Name = "Contact No.")]
        public string PhoneNumber { get; set; }

        public string UniversityId { get; set; }
        public string RegNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        
        [Required]
       public string CovidVaccine { get; set; }


        public AccountType AccountType { get; set; }


        //[DisplayName("Name")]
        //public string Name { get; set; }


        [Required]
        [Display(Name = "Username")]
        [Remote(action: "IsUserNameInUse", controller: "Account")]

        public string UserName { get; set; }

    }
}
