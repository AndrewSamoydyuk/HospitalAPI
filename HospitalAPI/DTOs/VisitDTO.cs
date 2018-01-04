using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalAPI.Models;

namespace HospitalAPI.DTOs
{
    public class VisitDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Diagnosis { get; set; }

        public string DoctorName { get; set; }

        public Status Status { get; set; } 

        public IEnumerable<VisitMedicationDTO> Medications { get; set; }
    }
}