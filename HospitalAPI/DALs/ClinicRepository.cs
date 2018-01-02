using System;
using System.Collections.Generic;
using System.Data.Entity;
using HospitalAPI.Models;
using HospitalAPI.DTOs;
using System.Linq;
using System.Linq.Expressions;

namespace HospitalAPI.DALs
{
    public class ClinicRepository: IClinicRepository
    {
        private HospitalContext db;

        public ClinicRepository(HospitalContext context)
        {
            this.db = context;
        }

        private static readonly Expression<Func<Department, DepartmentDTO>> AsDepartmentDto = x => new DepartmentDTO
        {
            Id = x.Id,
            Name = x.Name
        };

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
            var clinic = (from c in db.Clinics
                          where c.Id == id
                          select new ClinicDetailDTO
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Address = c.Address,
                              ImageUri = c.ImageUri,
                              Departments = db.Departments.Where(d=>d.ClinicID == id).Select(AsDepartmentDto).ToList(),
                              CountOfDepartments = db.Departments.Where(d => d.ClinicID == id).Count()

                          }).SingleOrDefault();

            return clinic;
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