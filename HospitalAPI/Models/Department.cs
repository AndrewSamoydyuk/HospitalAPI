using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HospitalAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        public ICollection<Doctor> Doctors { get; set; }

    }
}