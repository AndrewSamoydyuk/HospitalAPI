using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalAPI.Models;
using System.Data.Entity;
using HospitalAPI.DTOs;

namespace HospitalAPI.DALs
{
    public class PatientRepository : IPatientRepository
    {
        private HospitalContext db;

        public PatientRepository(HospitalContext context)
        {
            this.db = context;
        }

        public IEnumerable<PatientDTO> GetPatients()
        {
            var patients = from p in db.Patients
                           select new PatientDTO
                           {
                               id = p.Id,
                               FullName = p.FullName,
                               Address = p.Address
                           };

            return patients;
        }

        public IEnumerable<VisitDTO> GetVisitsByDate(DateTime date)
        {
            var visits = from visit in db.PatientVisits
                         where DbFunctions.TruncateTime(visit.Date) == DbFunctions.TruncateTime(date)
                         select new VisitDTO
                         {
                             Date = visit.Date,
                             Diagnosis = visit.Diagnosis,
                             DoctorName = visit.Doctor.FullName,
                             Id = visit.Id,
                             Status = visit.Status,
                             Medications = visit.Medications
                                         .Select(vs => new VisitMedicationDTO
                                         {
                                             CountOfDays = vs.CountOfDays,
                                             Id = vs.Medication.Id,
                                             Name = vs.Medication.Name
                                         })
                         };

            return visits;
        }

        public PatientVisit GetVisitById(int id)
        {
            return db.PatientVisits.Find(id);
        }

        public PatientDetailDTO GetPatientDetails(int id)
        {
            var patient = (from p in db.Patients.Include(pat => pat.Visits)
                           select new PatientDetailDTO
                           {
                               Id = p.Id,
                               FullName = p.FullName,
                               Address = p.Address,
                               DateOfBirth = p.DateOfBirth,
                               ImageUri = p.ImageUri,
                               Phone = p.Phone,
                               Sex = p.Sex,
                               Visits = from visit in p.Visits
                                        select new VisitDTO
                                        {
                                            Date = visit.Date,
                                            Diagnosis = visit.Diagnosis,
                                            DoctorName = visit.Doctor.FullName,
                                            Id = visit.Id,
                                            Status = visit.Status,
                                            Medications = visit.Medications
                                                        .Select(vs => new VisitMedicationDTO
                                                        {
                                                            CountOfDays = vs.CountOfDays,
                                                            Id = vs.Medication.Id,
                                                            Name = vs.Medication.Name
                                                        })
                                        }
                           }).FirstOrDefault(pat => pat.Id == id);

            return patient;
        }

        public Patient GetPatientById(int id)
        {
            return db.Patients.Find(id);
        }

        public void AddVisit(PatientVisit visit)
        {
            db.PatientVisits.Add(visit);
        }

        public void AddMedication(PatientVisitMedication medication)
        {
            db.PatientVisitMedication.Add(medication);
        }

        public void UpdateVisitStatus(PatientVisit visit)
        {
            db.Entry(visit).State = EntityState.Modified;
        }

        public void DeletePatient(Patient patient)
        {
            db.Patients.Remove(patient);
        }

        public void AddPatient(Patient patient)
        {
            db.Patients.Add(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            db.Entry(patient).State = EntityState.Modified;
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