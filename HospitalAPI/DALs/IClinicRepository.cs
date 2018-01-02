using System;
using System.Collections.Generic;
using HospitalAPI.Models;
using HospitalAPI.DTOs;

namespace HospitalAPI.DALs
{
    public interface IClinicRepository: IDisposable
    {
        IEnumerable<ClinicDTO> GetClinics();
        ClinicDetailDTO GetClinicById(int id);
        Clinic GetClinic(int id);
        void DeleteClinic(Clinic clinic);
        void UpdateClinic(Clinic clinic);
        void AddClinic(Clinic clinic);
        void Save();
    }
}
