using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeveloperClient.Models
{
    public class Developer
    {
        public int DeveloperId { get; set; }

        [StringLength(50, ErrorMessage = "El primer Nombre no debe exceder los 50 caracteres")]
        public string FirstName { get; set; }
        
        [StringLength(50, ErrorMessage = "El segundo Nombre no debe exceder los 50 caracteres")]
        public string SecondName { get; set; }
        
        [StringLength(50, ErrorMessage = "El primer Apellido no debe exceder los 50 caracteres")]
        public string FirstSurname { get; set; }
        
        //[StringLength(50, ErrorMessage = "El segundo Apellido no debe exceder los 50 caracteres")]
        public string SecondSurname { get; set; }

        [StringLength(8, ErrorMessage = "El numero de telefono debe ser de maximo 8 caracteres.")]
        public string Phone { get; set; }

        [StringLength(50, ErrorMessage = "El Email no debe exceder los 50 caracteres")]
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Enabled { get; set; }
    }
}
