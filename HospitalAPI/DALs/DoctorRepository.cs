using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalAPI.DTOs;
using HospitalAPI.Models;
using System.Data.Entity;

namespace HospitalAPI.DALs
{
    public class DoctorRepository : IDoctorRepository
    {
        private HospitalContext db;

        public DoctorRepository(HospitalContext context)
        {
            this.db = context;
        }

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
            var doctor = (from d in db.Doctors.Include(doc=>doc.Department).Include(doc=>doc.Schedule)
                          select new DoctorDetailDTO
                          {
                              Id = d.Id,
                              FullName = d.FullName,
                              Education = d.Education,
                              Sex = d.Sex,
                              Speciality = d.Speciality,
                              ImageUri = d.ImageUri,
                              RoomNumber = d.RoomNumber,
                              Department = new DepartmentDTO
                              {
                                  Id = d.Department.Id,
                                  Name = d.Department.Name
                              },
                              Schedule = from s in d.Schedule
                                         select new ScheduleDTO
                                         {
                                             Id = s.Id,
                                             TimeStart = s.TimeStart.ToString().Substring(0, 5),
                                             TimeEnd = s.TimeEnd.ToString().Substring(0, 5),
                                             DayNumber = s.DayNumber
                                         }
                              
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