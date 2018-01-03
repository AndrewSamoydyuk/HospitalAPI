using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HospitalAPI.DTOs;
using HospitalAPI.Models;
using System.Threading.Tasks;

namespace HospitalAPI.DALs
{
    public interface IDoctorRepository : IDisposable
    {
        IEnumerable<DoctorDTO> GetDoctors();
        Doctor GetDoctorById(int id);
        DoctorDetailDTO DoctorDetails(int id);
        void DeleteDoctor(Doctor doctor);
        void AddDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        void Save();
    }
}
