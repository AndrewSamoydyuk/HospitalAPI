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
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SchedulePerDay", "DoctorID", "dbo.Doctor");
            DropForeignKey("dbo.PatientVisit", "PatientID", "dbo.Patient");
            DropForeignKey("dbo.Patient", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PatientVisitMedication", "PatientVisitID", "dbo.PatientVisit");
            DropForeignKey("dbo.PatientVisitMedication", "MedicationID", "dbo.Medication");
            DropForeignKey("dbo.PatientVisit", "DoctorID", "dbo.Doctor");
            DropForeignKey("dbo.Doctor", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "ClinicID", "dbo.Clinic");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SchedulePerDay", new[] { "DoctorID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Patient", new[] { "ApplicationUserID" });
            DropIndex("dbo.PatientVisitMedication", new[] { "PatientVisitID" });
            DropIndex("dbo.PatientVisitMedication", new[] { "MedicationID" });
            DropIndex("dbo.PatientVisit", new[] { "DoctorID" });
            DropIndex("dbo.PatientVisit", new[] { "PatientID" });
            DropIndex("dbo.Doctor", new[] { "DepartmentID" });
            DropIndex("dbo.Department", new[] { "ClinicID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SchedulePerDay");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
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
