using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HospitalAPI.Models
{
    public class Clinic
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Address { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}