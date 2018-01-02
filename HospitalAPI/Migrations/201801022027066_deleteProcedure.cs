namespace HospitalAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteProcedure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientVisitProcedure", "PatientVisitID", "dbo.PatientVisit");
            DropForeignKey("dbo.PatientVisitProcedure", "ProcedureID", "dbo.Procedure");
            DropIndex("dbo.PatientVisitProcedure", new[] { "ProcedureID" });
            DropIndex("dbo.PatientVisitProcedure", new[] { "PatientVisitID" });
            DropTable("dbo.PatientVisitProcedure");
            DropTable("dbo.Procedure");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Procedure",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientVisitProcedure",
                c => new
                    {
                        ProcedureID = c.Int(nullable: false),
                        PatientVisitID = c.Int(nullable: false),
                        CountOfDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProcedureID, t.PatientVisitID });
            
            CreateIndex("dbo.PatientVisitProcedure", "PatientVisitID");
            CreateIndex("dbo.PatientVisitProcedure", "ProcedureID");
            AddForeignKey("dbo.PatientVisitProcedure", "ProcedureID", "dbo.Procedure", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PatientVisitProcedure", "PatientVisitID", "dbo.PatientVisit", "Id", cascadeDelete: true);
        }
    }
}
