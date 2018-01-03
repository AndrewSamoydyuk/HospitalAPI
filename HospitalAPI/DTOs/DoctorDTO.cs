using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Speciality { get; set; }
    }
}