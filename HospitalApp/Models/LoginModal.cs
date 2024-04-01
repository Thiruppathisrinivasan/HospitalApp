using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HospitalApp.Models
{
    public class LoginModal
    {
        [Required(ErrorMessage = " username is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "username should between 5 to 20 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter the valid password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password atlest contain 5 to 20 characters")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }

    }
}