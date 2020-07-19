using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VMS.Models
{
    public class Opportunity
    {
        public int Id { get; set; }

        [Required]
        public string OpportunityName { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public string CenterType { get; set; }
        
        public bool IsOpen { get; set; }

        public int VolunteerId{ get; set; }
        

    }
}