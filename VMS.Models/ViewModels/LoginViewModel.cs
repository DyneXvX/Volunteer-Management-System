using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VMS.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [NotMapped]//no point in pushing these
        public string UserName { get; set; }
        [Required]
        [NotMapped]
        [MaxLength(25)]
        public string Password { get; set; }

       
    }
}
