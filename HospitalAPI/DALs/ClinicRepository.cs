using System;
using System.Collections.Generic;
using System.Data.Entity;
using HospitalAPI.Models;
using HospitalAPI.DTOs;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace HospitalAPI.DALs
{
    public class ClinicRepository : IClinicRepository
    {
        private HospitalContext db;

        public ClinicRepository(HospitalContext context)
        {
            this.db = context;
        }

        public IEnumerable<ClinicDTO> GetClinics()
        {
            var clinics = from c in db.Clinics
                          select new ClinicDTO
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Address = c.Address,
                              ImageUri = c.ImageUri
                          };

            return clinics;
        }

        public ClinicDetailDTO ClinicDetails(int id)
        {
            var clinic = (from c in db.Clinics.Include(cl => cl.Departments)
                          where c.Id == id
                          select new ClinicDetailDTO
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Address = c.Address,
                              ImageUri = c.ImageUri,
                              Departments = from d in c.Departments
                                            select new DepartmentDTO
                                            {
                                                Id = d.Id,
                                                Name = d.Name
                                            },
                              CountOfDepartments = c.Departments.Count(),
                              CountOfDoctors = (from d in c.Departments
                                                select d.Doctors.Count()).Sum()


                          }).SingleOrDefault();

            clinic?.Departments.ToList().ForEach(d => d.ResultsOfTreatment = GetResultOfTreatment(d.Id));


            return clinic;
        }

        private ResultOfTreatment GetResultOfTreatment(int depId)
        {
            var group = from r in db.PatientVisits.Include(v => v.Doctor)
                        where r.Doctor.DepartmentID == depId
                        group r by r.Status into res
                        select new
                        {
                            Name = res.Key,
                            Count = res.Count()
                        };

            ResultOfTreatment result = new ResultOfTreatment()
            {
                CountOfCured = group.SingleOrDefault(g => g.Name == Status.Cured)?.Count ?? 0,
                CountOfNotCured = group.SingleOrDefault(g => g.Name == Status.NotCured)?.Count ?? 0,
                CountOfOnTreatment = group.SingleOrDefault(g => g.Name == Status.OnTreatment)?.Count ?? 0
            };

            return result;
        }

        public Clinic GetClinicById(int id)
        {
            return db.Clinics.Find(id);
        }

        public void DeleteClinic(Clinic clinic)
        {
            db.Clinics.Remove(clinic);
        }

        public void UpdateClinic(Clinic clinic)
        {
            db.Entry(clinic).State = EntityState.Modified;
        }

        public void AddClinic(Clinic clinic)
        {
            db.Clinics.Add(clinic);
        }

        public void AddDepartment(Department department)
        {
            db.Departments.Add(department);
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