using System;
using System.Collections.Generic;
using System.Text;

namespace VMS.Utility
{
    public static class SD
    {
        // roles available
        public const string Role_User_Indi = "Individual User";
        public const string Role_Admin = "Admin";

        // values for the Volunteer ApprovalStatus property 
        public const string Status_Approved = "Approved";
        public const string Status_Pending = "Pending Approval";
        public const string Status_Disapproved = "Disapproved";

        // Center types available 
        public const string Center_Emergency = "Medical Emergency";
        public const string Center_Medical = "Medical";
        public const string Center_Mental = "Mental Health";
        public const string Center_Hospice = "Hospice";
        public const string Center_Senior = "Senior Care";
        public const string Center_Youth = "Youth Development";
        public const string Center_Homeless = "Homeless Shelter";
        public const string Center_Library = "Library";
        public const string Center_Music = "Music";
        public const string Center_Pet = "Pet";

        // Possible Organization Names:
        //"Red Cross"
        //"Heartland Hospice Services"
        //"Youth Literacy Foundation"
        //"Jacksonville Public Library"
        //"Music Tutoring Inc"
        //"Jax Mental Health"
        //"Jacksonville Humane Society"

        // filters 
        public const string Filter_Inactive = "inactive";
        public const string Filter_Approved = "approved";
        public const string Filter_Pending = "pending";
        public const string Filter_Disapproved = "disapproved";
        public const string Filter_Approved_Pending = "approved-pending";

    }
}
