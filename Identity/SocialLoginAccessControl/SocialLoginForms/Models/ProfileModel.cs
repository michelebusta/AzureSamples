using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialLoginModules.Models
{
    public class ProfileModel
    {
        [Display(Name = "Name Id")]
        public string NameId { get; set; }

        [Display(Name = "Issuer")]
        public string Issuer { get; set; }

        [Required]
        [Display(Name = "Friendly Name")]
        public string FriendlyName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}