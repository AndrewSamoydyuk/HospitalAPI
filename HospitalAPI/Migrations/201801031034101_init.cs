namespace HospitalAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clinic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ImageUri = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClinicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinic", t => t.ClinicID, cascadeDelete: true)
                .Index(t => t.ClinicID);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Education = c.String(nullable: false),
                        Sex = c.String(nullable: false),
                        Speciality = c.String(nullable: false),
                        ImageUri = c.String(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.PatientVisit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Diagnosis = c.String(nullable: false),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctor", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patient", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.PatientVisitMedication",
                c => new
                    {
                        MedicationID = c.Int(nullable: false),
                        PatientVisitID = c.Int(nullable: false),
                        CountOfDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicationID, t.PatientVisitID })
                .ForeignKey("dbo.Medication", t => t.MedicationID, cascadeDelete: true)
                .ForeignKey("dbo.PatientVisit", t => t.PatientVisitID, cascadeDelete: true)
                .Index(t => t.MedicationID)
                .Index(t => t.PatientVisitID);
            
            CreateTable(
                "dbo.Medication",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        AveragePrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Sex = c.String(nullable: false),
                        Address = c.String(),
                        ImageUri = c.String(nullable: false),
                        Phone = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchedulePerDay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStart = c.Time(nullable: false, precision: 7),
                        TimeEnd = c.Time(nullable: false, precision: 7),
                        DoctorID = c.Int(nullable: false),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctor", t => t.DoctorID, cascadeDelete: true)
                .Index(t => t.DoctorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulePerDay", "DoctorID", "dbo.Doctor");
            DropForeignKey("dbo.PatientVisit", "PatientID", "dbo.Patient");
            DropForeignKey("dbo.PatientVisitMedication", "PatientVisitID", "dbo.PatientVisit");
            DropForeignKey("dbo.PatientVisitMedication", "MedicationID", "dbo.Medication");
            DropForeignKey("dbo.PatientVisit", "DoctorID", "dbo.Doctor");
            DropForeignKey("dbo.Doctor", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "ClinicID", "dbo.Clinic");
            DropIndex("dbo.SchedulePerDay", new[] { "DoctorID" });
            DropIndex("dbo.PatientVisitMedication", new[] { "PatientVisitID" });
            DropIndex("dbo.PatientVisitMedication", new[] { "MedicationID" });
            DropIndex("dbo.PatientVisit", new[] { "DoctorID" });
            DropIndex("dbo.PatientVisit", new[] { "PatientID" });
            DropIndex("dbo.Doctor", new[] { "DepartmentID" });
            DropIndex("dbo.Department", new[] { "ClinicID" });
            DropTable("dbo.SchedulePerDay");
            DropTable("dbo.Patient");
            DropTable("dbo.Medication");
            DropTable("dbo.PatientVisitMedication");
            DropTable("dbo.PatientVisit");
            DropTable("dbo.Doctor");
            DropTable("dbo.Department");
            DropTable("dbo.Clinic");
        }
    }
}
