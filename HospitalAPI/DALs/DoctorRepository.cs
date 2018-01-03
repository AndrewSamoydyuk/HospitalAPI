using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalAPI.DTOs;
using HospitalAPI.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace HospitalAPI.DALs
{
    public class DoctorRepository : IDoctorRepository
    {
        private HospitalContext db;

        public DoctorRepository(HospitalContext context)
        {
            this.db = context;
        }

        private static readonly Expression<Func<Department, DepartmentDTO>> AsDepartmentDto = x => new DepartmentDTO
        {
            Id = x.Id,
            Name = x.Name
        };

        private static readonly Expression<Func<SchedulePerDay, ScheduleDTO>> AsScheduleDto = x => new ScheduleDTO
        {
            Id = x.Id,
            TimeStart = x.TimeStart.ToString().Substring(0,5),
            TimeEnd = x.TimeEnd.ToString().Substring(0, 5),
            DayNumber = x.DayNumber
        };

        public IEnumerable<DoctorDTO> GetDoctors()
        {
            var doctors = from d in db.Doctors
                          select new DoctorDTO
                          {
                              Id = d.Id,
                              FullName = d.FullName,
                              Speciality = d.Speciality
                          };

            return doctors;
        }

        public DoctorDetailDTO DoctorDetails(int id)
        {
            var doctor = (from d in db.Doctors
                          select new DoctorDetailDTO
                          {
                              Id = d.Id,
                              FullName = d.FullName,
                              Education = d.Education,
                              Sex = d.Sex,
                              Speciality = d.Speciality,
                              ImageUri = d.ImageUri,
                              RoomNumber = d.RoomNumber,
                              Department = db.Departments.Select(AsDepartmentDto).FirstOrDefault(dep => dep.Id == d.DepartmentID),
                              Schedule = db.Schedule.Where(s=>s.DoctorID == d.Id).Select(AsScheduleDto).ToList()
                              
                          }).SingleOrDefault(d=>d.Id == id);

            return doctor;
        }

        public Doctor GetDoctorById(int id)
        {
            return db.Doctors.Find(id);
        }

        public void AddDoctor (Doctor doctor)
        {
            db.Doctors.Add(doctor);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            db.Entry(doctor).State = EntityState.Modified;
        }

        public void DeleteDoctor(Doctor doctor)
        {
            db.Doctors.Remove(doctor);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}