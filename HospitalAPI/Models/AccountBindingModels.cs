using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalAPI.Models
{
    public class RegisterBindingModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "Password must contain at least 6 characters and not more than 18", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}