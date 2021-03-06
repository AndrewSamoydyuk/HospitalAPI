﻿namespace HospitalAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using HospitalAPI.Models;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<HospitalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HospitalContext context)
        {

            var clinics = new List<Clinic>
            {
                new Clinic() { Name = "The Pennsylvania Hospital", Address = "800 Spruce St", ImageUri="Pennsylvania_Hospital.png" },
                new Clinic() { Name = "Cleveland Clinic", Address = "2049 East 100th Street ", ImageUri="ClevelandClinic.jpg" },
                new Clinic() { Name = "Hospital of St. Cross", Address = "St Cross Back St ", ImageUri="HospitalofStCross .jpg" },
                new Clinic() { Name = "The American Hospital of Paris", Address = "63 Boulevard Victor Hugo ", ImageUri="AmericanHospital.jpg" }
            };

            clinics.ForEach(s => context.Clinics.AddOrUpdate(c => c.Id, s));
            context.SaveChanges();

            var departments = new List<Department>
                {
                    //The Pennsylvania Hospital
                    new Department(){ Name = "Anaesthetics", ClinicID = clinics.Single(c=>c.Name == "The Pennsylvania Hospital").Id},
                    new Department(){ Name = "Cardiology", ClinicID = clinics.Single(c=>c.Name == "The Pennsylvania Hospital").Id},
                    new Department(){ Name = "Critical care", ClinicID = clinics.Single(c=>c.Name == "The Pennsylvania Hospital").Id},
                    new Department(){ Name = "Gynaecology", ClinicID = clinics.Single(c=>c.Name == "The Pennsylvania Hospital").Id},
                    //Cleveland Clinic
                    new Department(){ Name = "Oncology", ClinicID = clinics.Single(c=>c.Name == "Cleveland Clinic").Id},
                    new Department(){ Name = "Ophthalmology", ClinicID = clinics.Single(c=>c.Name == "Cleveland Clinic").Id},
                    new Department(){ Name = "Orthopaedics", ClinicID = clinics.Single(c=>c.Name == "Cleveland Clinic").Id},
                    new Department(){ Name = "Physiotherapy", ClinicID = clinics.Single(c=>c.Name == "Cleveland Clinic").Id},
                    //Hospital of St. Cross
                    new Department(){ Name = "Cardiology", ClinicID = clinics.Single(c=>c.Name == "Hospital of St. Cross").Id},
                    new Department(){ Name = "Rheumatology", ClinicID = clinics.Single(c=>c.Name == "Hospital of St. Cross").Id},
                    new Department(){ Name = "Oncology", ClinicID = clinics.Single(c=>c.Name == "Hospital of St. Cross").Id},
                    //The American Hospital of Paris
                    new Department(){ Name = "Microbiology", ClinicID = clinics.Single(c=>c.Name == "The American Hospital of Paris").Id},
                    new Department(){ Name = "Rheumatology", ClinicID = clinics.Single(c=>c.Name == "The American Hospital of Paris").Id},
                    new Department(){ Name = "Haematology", ClinicID = clinics.Single(c=>c.Name == "The American Hospital of Paris").Id},
                    new Department(){ Name = "Critical care", ClinicID = clinics.Single(c=>c.Name == "The American Hospital of Paris").Id},
                    new Department(){ Name = "Gynaecology", ClinicID = clinics.Single(c=>c.Name == "The American Hospital of Paris").Id,},

                };

            departments.ForEach(d => context.Departments.AddOrUpdate(s => s.Id, d));
            context.SaveChanges();

            var doctors = new List<Doctor>
                {
                    new Doctor(){ FullName = "Benedict of Nursia" , Education = "Primary education", Sex = "Male", Speciality = "Cardiologists‎", RoomNumber = 221,
                        DepartmentID = departments.Single(d=> d.Name == "Anaesthetics" && d.Clinic.Name == "The Pennsylvania Hospital" ).Id, ImageUri = "Doctor.png" },
                    new Doctor(){ FullName = "Max Wilms" , Education = "Basic education", Sex = "Male", Speciality = "Diabetologists‎‎", RoomNumber = 107,
                        DepartmentID = departments.Single(d=> d.Name == "Cardiology" && d.Clinic.Name == "The Pennsylvania Hospital" ).Id, ImageUri = "Doctor.png"},
                    new Doctor(){ FullName = "John Bodkin Adams" , Education = "Upper secondary education", Sex = "Male", Speciality = "Psychiatrists‎‎‎", RoomNumber = 102,
                        DepartmentID = departments.Single(d=> d.Name == "Anaesthetics" && d.Clinic.Name == "The Pennsylvania Hospital" ).Id, ImageUri = "Doctor.png"},
                    new Doctor(){ FullName = "Mikhail Bulgakov" , Education = "Primary education", Sex = "Male", Speciality = "Gynaecologists‎", RoomNumber = 87,
                        DepartmentID = departments.Single(d=> d.Name == "Gynaecology" && d.Clinic.Name == "The Pennsylvania Hospital" ).Id, ImageUri = "Doctor.png"},
                    new Doctor(){ FullName = "Bob Brown" , Education = "Basic education", Sex = "Male", Speciality = "Emergency physicians‎‎", RoomNumber = 12,
                        DepartmentID = departments.Single(d=> d.Name == "Critical care" && d.Clinic.Name == "The Pennsylvania Hospital" ).Id, ImageUri = "Doctor.png"}
                };

            doctors.ForEach(d => context.Doctors.AddOrUpdate(p => p.Id, d));
            context.SaveChanges();

            var schedules = new List<SchedulePerDay>
                {
                    new SchedulePerDay(){ TimeStart = TimeSpan.Parse("7:30"), TimeEnd = TimeSpan.Parse("15:30"), DoctorID = doctors.Single(d=>d.FullName=="Benedict of Nursia").Id, DayNumber = DayNumber.Monday},
                    new SchedulePerDay(){ TimeStart = TimeSpan.Parse("8:00"), TimeEnd = TimeSpan.Parse("16:30"), DoctorID = doctors.Single(d=>d.FullName=="Benedict of Nursia").Id, DayNumber = DayNumber.Wednesday},
                    new SchedulePerDay(){ TimeStart = TimeSpan.Parse("10:00"), TimeEnd = TimeSpan.Parse("17:00"), DoctorID = doctors.Single(d=>d.FullName=="Benedict of Nursia").Id,DayNumber = DayNumber.Sunday},
                    new SchedulePerDay(){ TimeStart = TimeSpan.Parse("8:30"), TimeEnd = TimeSpan.Parse("13:30"), DoctorID = doctors.Single(d=>d.FullName=="Max Wilms").Id, DayNumber = DayNumber.Tuesday},
                };

            schedules.ForEach(s => context.Schedule.AddOrUpdate(c => c.Id, s));
            context.SaveChanges();

            var RoleStore = new RoleStore<IdentityRole>(context);
            var RoleManager = new RoleManager<IdentityRole>(RoleStore);
            var roles = new List<ApplicationRole>
                {
                    new ApplicationRole(){ Name = "Admin", Description = "Manage the application"},
                    new ApplicationRole(){ Name = "Doctor", Description = "Can see patient information and change it"},
                    new ApplicationRole(){ Name = "Patient", Description = "Can see only his own information"}
                };
            roles.ForEach(r => RoleManager.Create(r));
            RoleStore.Context.SaveChanges();

            var UserStore = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);
            var admin = new ApplicationUser() { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            UserManager.Create(admin, "password");
            UserStore.Context.SaveChanges();
            admin = UserManager.Find("admin@gmail.com", "password");
            UserManager.AddToRole(admin.Id, "Admin");

            var doctor = new ApplicationUser { Email = "doctor@gmail.com", UserName = "doctor@gmail.com" };
            UserManager.Create(doctor, "password");
            UserStore.Context.SaveChanges();
            doctor = UserManager.Find("doctor@gmail.com", "password");
            UserManager.AddToRole(doctor.Id, "Doctor");

            var users = new List<ApplicationUser>
                {
                    new ApplicationUser(){ Email = "George@gmail.com", UserName ="George@gmail.com"},
                    new ApplicationUser(){ Email = "Jeffrey@ukr.net", UserName ="Jeffrey@ukr.net"},
                    new ApplicationUser(){ Email = "Brian@gmail.com", UserName ="Brian@gmail.com"},
                    new ApplicationUser(){ Email = "Marjorie@rambler.ru", UserName ="Marjorie@rambler.ru"},
                    new ApplicationUser(){ Email = "Lynn@gmail.com", UserName ="Lynn@gmail.com"},
                    new ApplicationUser(){ Email = "Marva@mail.ru", UserName ="Marva@mail.ru"},
                    new ApplicationUser(){ Email = "Vivian@gmail.com", UserName ="Vivian@gmail.com"}
                };
            users.ForEach(u => UserManager.Create(u, "password"));
            UserStore.Context.SaveChanges();
            for (int i = 0; i < users.Count; i++)
            {
                users[i] = UserManager.Find(users[i].Email, "password");
            }
            users.ForEach(u => UserManager.AddToRole(u.Id, "Patient"));
            UserStore.Context.SaveChanges();

            var patients = new List<Patient>
                {
                    new Patient(){ FullName = "George A. Grantham", Address = "2124 Atha Drive", DateOfBirth= DateTime.Parse("March 6, 1996"), Phone = "661-762-9592", Sex = "Male",
                        ApplicationUserID = users.Single(u=>u.Email=="George@gmail.com").Id, ImageUri = "DefaulImageForPatient.jpg"},
                    new Patient(){ FullName = "Jeffrey S. Morgan", Address = "3793 James Street", DateOfBirth= DateTime.Parse("May 25, 1973"), Phone = "585-593-7478", Sex = "Male",
                        ApplicationUserID = users.Single(u=>u.Email=="Jeffrey@ukr.net").Id, ImageUri = "DefaulImageForPatient.jpg"},
                    new Patient(){ FullName = "Brian L. Bower", Address = "1718 Goldleaf Lane", DateOfBirth= DateTime.Parse("March 11, 1965"), Phone = "201-585-5032", Sex = "Male",
                        ApplicationUserID = users.Single(u=>u.Email=="Brian@gmail.com").Id, ImageUri = "DefaulImageForPatient.jpg"},
                    new Patient(){ FullName = "Marjorie S. Lombard", Address = "2950 Hall Valley Drive", DateOfBirth= DateTime.Parse("January 7, 1948"), Phone = "304-639-8050", Sex = "Female",
                        ApplicationUserID = users.Single(u=>u.Email=="Marjorie@rambler.ru").Id, ImageUri = "DefaulImageForPatient.jpg"},
                    new Patient(){ FullName = "Lynn J. Larson", Address = "1012 Buck Drive", DateOfBirth= DateTime.Parse("April 1, 1984"), Phone = "802-221-2858", Sex = "Female",
                        ApplicationUserID = users.Single(u=>u.Email=="Lynn@gmail.com").Id, ImageUri = "DefaulImageForPatient.jpg"},
                    new Patient(){ FullName = "Marva R. Orr", Address = "1259 River Road", DateOfBirth= DateTime.Parse("March 10, 1967"), Phone = "719-503-7851", Sex = "Female",
                        ApplicationUserID = users.Single(u=>u.Email=="Marva@mail.ru").Id, ImageUri = "DefaulImageForPatient.jpg"},
                    new Patient(){ FullName = "Vivian J. Mott", Address = "963 Washington Street", DateOfBirth= DateTime.Parse("April 19, 1985"), Phone = "361-439-5374", Sex = "Female",
                        ApplicationUserID = users.Single(u=>u.Email=="Vivian@gmail.com").Id, ImageUri = "DefaulImageForPatient.jpg"},
                };

            patients.ForEach(p => context.Patients.AddOrUpdate(d => d.Id, p));
            context.SaveChanges();

            var patientVisits = new List<PatientVisit>
                {
                    new PatientVisit(){ Date = DateTime.Parse("February 14, 2017"), Diagnosis = "Arthritis" , Status = Status.OnTreatment,
                        DoctorID = doctors.Single(d=>d.FullName == "Benedict of Nursia").Id, PatientID = patients.Single(p=>p.FullName=="George A. Grantham").Id },

                    new PatientVisit(){ Date = DateTime.Parse("February 20, 2017"), Diagnosis = "Arthritis" , Status = Status.OnTreatment,
                        DoctorID = doctors.Single(d=>d.FullName == "Benedict of Nursia").Id, PatientID = patients.Single(p=>p.FullName=="George A. Grantham").Id },

                    new PatientVisit(){ Date = DateTime.Parse("February 23, 2017"), Diagnosis = "Chronic Pain" , Status = Status.Cured,
                        DoctorID = doctors.Single(d=>d.FullName == "Max Wilms").Id, PatientID = patients.Single(p=>p.FullName=="Jeffrey S. Morgan").Id },

                    new PatientVisit(){ Date = DateTime.Parse("March 26, 2017"), Diagnosis = "Diabetes" , Status = Status.Cured,
                        DoctorID = doctors.Single(d=>d.FullName == "Bob Brown").Id, PatientID = patients.Single(p=>p.FullName=="Marva R. Orr").Id },

                    new PatientVisit(){ Date = DateTime.Parse("May 22, 2017"), Diagnosis = "Depression" , Status = Status.NotCured,
                        DoctorID = doctors.Single(d=>d.FullName == "Bob Brown").Id, PatientID = patients.Single(p=>p.FullName=="Marjorie S. Lombard").Id },
                };

            patientVisits.ForEach(p => context.PatientVisits.AddOrUpdate(s => s.Id, p));
            context.SaveChanges();

            var medications = new List<Medication>
                {
                    new Medication(){ Name = "Abacavir Sulfate", AveragePrice = 150, Description= "Abacavir is an antiviral medication that prevents human immunodeficiency virus (HIV) cells from multiplying in your body."},
                    new Medication(){ Name = "Acarbose", AveragePrice = 50, Description= "Acarbose slows the digestion of carbohydrates in the body, which helps control blood sugar levels."},
                    new Medication(){ Name = "Baclofen", AveragePrice = 56, Description= "Baclofen orally disintegrating tablets is a muscle relaxant and antispastic"},
                    new Medication(){ Name = "Baraclude", AveragePrice = 74, Description= "BARACLUDE is the tradename for entecavir, a guanosine nucleoside analogue with selective activity against HBV. "},
                    new Medication(){ Name = "Botox", AveragePrice = 34, Description= "OnabotulinumtoxinA (Botox), also called botulinum toxin type A, is made from the bacteria that causes botulism. Botulinum toxin blocks nerve activity in the muscles, causing a temporary reduction in muscle activity"},
                    new Medication(){ Name = "Butrans", AveragePrice = 15, Description= "Buprenorphine is an opioid pain medication. An opioid is sometimes called a narcotic"},
                    new Medication(){ Name = "Halcion", AveragePrice = 84, Description= "Triazolam is a benzodiazepine (ben-zoe-dye-AZE-eh-peen) similar to Valium. Triazolam affects chemicals in the brain that may become unbalanced and cause sleep problems (insomnia)."},
                    new Medication(){ Name = "Hyzaar", AveragePrice = 12, Description= "HYZAAR 50/12.5 (losartan potassium-hydrochlorothiazide), HYZAAR 100/12.5 (losartan potassiumhydrochlorothiazide) and HYZAAR 100/25 (losartan potassium-hydrochlorothiazide) tablets combine an angiotensin II receptor blocker acting on the AT 1 receptor subtype and a diuretic, hydrochlorothiazide."},
                    new Medication(){ Name = "Paraplatin", AveragePrice = 32, Description= "Carboplatin is a cancer medication that interferes with the growth of cancer cells and slows their growth and spread in the body."}
                };

            medications.ForEach(m => context.Medications.AddOrUpdate(d => d.Id, m));
            context.SaveChanges();

            var patientVisitMedication = new List<PatientVisitMedication> {

                new PatientVisitMedication() { CountOfDays = 10, MedicationID = medications.Single(m => m.Name == "Halcion").Id, PatientVisitID = patientVisits.Single(v => v.Id == 1).Id },
                new PatientVisitMedication() { CountOfDays = 13, MedicationID = medications.Single(m => m.Name == "Butrans").Id, PatientVisitID = patientVisits.Single(v => v.Id == 1).Id },
                new PatientVisitMedication() { CountOfDays = 30, MedicationID = medications.Single(m => m.Name == "Acarbose").Id, PatientVisitID = patientVisits.Single(v => v.Id == 1).Id },
                new PatientVisitMedication() { CountOfDays = 3, MedicationID = medications.Single(m => m.Name == "Abacavir Sulfate").Id, PatientVisitID = patientVisits.Single(v => v.Id == 2).Id },
                new PatientVisitMedication() { CountOfDays = 4, MedicationID = medications.Single(m => m.Name == "Acarbose").Id, PatientVisitID = patientVisits.Single(v => v.Id == 3).Id },
                new PatientVisitMedication() { CountOfDays = 5, MedicationID = medications.Single(m => m.Name == "Baraclude").Id, PatientVisitID = patientVisits.Single(v => v.Id == 4).Id },
                new PatientVisitMedication() { CountOfDays = 7, MedicationID = medications.Single(m => m.Name == "Halcion").Id, PatientVisitID = patientVisits.Single(v => v.Id == 4).Id },
                new PatientVisitMedication() { CountOfDays = 12, MedicationID = medications.Single(m => m.Name == "Paraplatin").Id, PatientVisitID = patientVisits.Single(v => v.Id == 5).Id },
                new PatientVisitMedication() { CountOfDays = 17, MedicationID = medications.Single(m => m.Name == "Abacavir Sulfate").Id, PatientVisitID = patientVisits.Single(v => v.Id == 5).Id },
                new PatientVisitMedication() { CountOfDays = 10, MedicationID = medications.Single(m => m.Name == "Halcion").Id, PatientVisitID = patientVisits.Single(v => v.Id == 5).Id }
            };

            patientVisitMedication.ForEach(pv => context.PatientVisitMedication.AddOrUpdate(v => v.MedicationID, pv));
            context.SaveChanges();

        }
    }
}
