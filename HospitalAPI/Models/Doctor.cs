using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HospitalAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string FullName { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public string Speciality { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int RoomNumber { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public ICollection<SchedulePerDay> Schedule { get; set; }
        public ICollection<PatientVisit> Patients { get; set; }

    }
}