using System;
using System.Collections.Generic;

#nullable disable

namespace DeveloperAPI.Models
{
    public partial class Developer
    {
        public int DeveloperId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Enabled { get; set; }
    }
}
