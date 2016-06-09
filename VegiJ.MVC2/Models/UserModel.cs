namespace VegiJ.MVC2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Principal;
    using DataAccess;

    public class UserModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        [ForeignKey("Gender")]
        public Guid GenderID { get; set; }
        public virtual Gender Gender { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Password { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Tip> Tips { get; set; }
        public ICollection<Event> Events { get; set; }
        public IIdentity Identity { get; set; }
    }
}