using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalAPI.Models
{
    public class HospitalContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalContext() : base("HospitalContext", throwIfV1Schema: false) { }
        public static HospitalContext Create()
        {
            return new HospitalContext();
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<SchedulePerDay> Schedule { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientVisit> PatientVisits { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<PatientVisitMedication> PatientVisitMedication { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}