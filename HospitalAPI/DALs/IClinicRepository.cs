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
        void Save();
    }
}
