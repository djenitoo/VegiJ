namespace VegiJ.Web.MVC.Areas.Users.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;
    using DataAccess;

    public class IndexViewModel
    {
        public User user { get; set; }
    }

    public class SettingsViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Display(Name = "Birth date")]
        public DateTime? BirthDate { get; set; }
        
        [ForeignKey("Gender")]
        public Guid? GenderID { get; set; }
        
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "GenderList")]
        public List<SelectListItem> ListItems { get; set; }
    }
}