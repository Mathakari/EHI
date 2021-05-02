using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInformationAPI.Models
{
    public class ContactInfo
    {
        /// <summary>
        /// First Name of Contact 
        /// </summary>
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Key]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool ISActive { get; set; }
    }
}
