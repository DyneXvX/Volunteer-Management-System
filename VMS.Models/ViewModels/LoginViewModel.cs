using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VMS.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string User { get; set; }
        public string Password { get; set; }
    }
}
