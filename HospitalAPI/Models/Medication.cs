using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class Medication
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int AveragePrice { get; set; }

        public ICollection<PatientVisitMedication> PatientVisitMedication { get; set; }

    }
}