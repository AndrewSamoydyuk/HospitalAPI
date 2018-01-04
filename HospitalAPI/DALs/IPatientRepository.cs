using System;
using System.Collections.Generic;
using HospitalAPI.Models;
using HospitalAPI.DTOs;

namespace HospitalAPI.DALs
{
    public interface IPatientRepository : IDisposable
    {
        IEnumerable<PatientDTO> GetPatients();
        Patient GetPatientById(int id);
        PatientDetailsDTO GetPatientDetails(int id);
        PatientVisit GetVisitById(int id);
        void AddVisit(PatientVisit visit);
        void AddMedication(PatientVisitMedication medication);
        void UpdateVisitStatus(PatientVisit visit);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);
        void Save();
    }
}
