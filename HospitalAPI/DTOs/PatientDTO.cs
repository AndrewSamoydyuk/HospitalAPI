using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class PatientDTO
    {
        public int id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }
    }
}