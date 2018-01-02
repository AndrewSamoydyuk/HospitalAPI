using System;

namespace HospitalAPI.DTOs
{
    public class MedicationDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AveragePrice { get; set; }
    }
}