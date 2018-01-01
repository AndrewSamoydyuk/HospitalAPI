using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalAPI.Models
{
    public class PatientVisitMedication
    {
        [Required]
        public int CountOfDays { get; set; }

        [Key]
        [Column(Order = 0)]
        public int MedicationID { get; set; }
        public Medication Medication { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PatientVisitID { get; set; }
        public PatientVisit PatientVisit { get; set; }
    }
}