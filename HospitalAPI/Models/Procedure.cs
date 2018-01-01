using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace HospitalAPI.Models
{
    public class Procedure
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        public ICollection<PatientVisitProcedure> PatientVisitProcedure { get; set; }

    }
}