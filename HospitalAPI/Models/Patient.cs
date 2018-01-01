using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Sex { get; set; }

        public string Address { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<PatientVisit> Visits { get; set; }

    }
}