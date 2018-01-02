using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalAPI.Models;
using System.Data.Entity;
using HospitalAPI.DTOs;
using System.Linq.Expressions;

namespace HospitalAPI.DALs
{
    public class MedicationRepository: IMedicationRepository
    {
        private HospitalContext db;

        public MedicationRepository(HospitalContext context)
        {
            this.db = context;
        }

        private static readonly Expression<Func<Medication, MedicationDTO>> AsMedicationDto = x => new MedicationDTO
        {
            Id = x.Id,
            Name = x.Name,
            AveragePrice = x.AveragePrice,
            Description = x.Description            
        };

        public IEnumerable<MedicationDTO> GetMedications()
        {
            return db.Medications.Select(AsMedicationDto);
        }

        public MedicationDTO MedicationDetails(int id)
        {
            return db.Medications.Select(AsMedicationDto).SingleOrDefault(m => m.Id == id);
        }

        public Medication GetMedicationById (int id)
        {
            return db.Medications.Find(id);
        }

        public void AddMedication(Medication medication)
        {
            db.Medications.Add(medication);
        }

        public void DeleteMedication(Medication medication)
        {
            db.Medications.Remove(medication);
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