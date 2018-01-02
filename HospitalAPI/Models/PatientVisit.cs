using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class PatientVisit
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public Status Status { get; set; }

        public ICollection<PatientVisitMedication> Medications { get; set; }

    }

    public enum Status
    {
        Cured,
        OnTreatment,
        NotCured
    }
}