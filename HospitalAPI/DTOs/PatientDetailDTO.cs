using System;
using System.Collections.Generic;
using HospitalAPI.Models;

namespace HospitalAPI.DTOs
{
    public class PatientDetailDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Sex { get; set; }

        public string Address { get; set; }

        public string ImageUri { get; set; }

        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IEnumerable<VisitDTO> Visits { get; set; }

        public string UserName { get; set; }
    }
}