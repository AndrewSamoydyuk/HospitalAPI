using System;
using System.Collections.Generic;
using HospitalAPI.DTOs;
using HospitalAPI.Models;

namespace HospitalAPI.DALs
{
    public interface IMedicationRepository : IDisposable
    {
        IEnumerable<MedicationDTO> GetMedications();
        MedicationDTO MedicationDetails(int id);
        Medication GetMedicationById(int id);
        void AddMedication(Medication medication);
        void DeleteMedication(Medication medication);
        void Save();
    }
}
