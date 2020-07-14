using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VMS.Models
{
    public class Volunteer
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [PasswordPropertyText]
        [MaxLength(25)]
        public string Password { get; set; }
        public string VolunteerPrefers { get; set; }
        public string Skills { get; set; }
        public string Times { get; set; }

        [Required]
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Education { get; set; }
        public string License { get; set; }

        [Required]
        public string EmergencyContactName { get; set; }
        public string EmergencyContactHomePhone { get; set; }
        public string EmergencyContactCellPhone{ get; set; }
        
        [EmailAddress]
        public string EmergencyContactEmail { get; set; }
        public bool VolunteerDriversLicense { get; set; }
        public bool VolunteerSsCard { get; set; }
        public bool ApprovalStatus { get; set; }
        
        

    }
}
