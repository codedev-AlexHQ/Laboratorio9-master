using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApplication1.Models
{
    public class PersonModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Apellido { get; set; }

    }
}