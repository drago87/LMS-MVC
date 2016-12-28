namespace LMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vad : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Files", newName: "Dossiers");
            DropForeignKey("dbo.ApplicationUserClassUnits", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserClassUnits", "ClassUnit_ClassUnitID", "dbo.ClassUnits");
            DropIndex("dbo.ApplicationUserClassUnits", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClassUnits", new[] { "ClassUnit_ClassUnitID" });
            AddColumn("dbo.ClassUnits", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ClassUnits", "ApplicationUser_Id");
            AddForeignKey("dbo.ClassUnits", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ApplicationUserClassUnits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserClassUnits",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ClassUnit_ClassUnitID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ClassUnit_ClassUnitID });
            
            DropForeignKey("dbo.ClassUnits", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClassUnits", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ClassUnits", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserClassUnits", "ClassUnit_ClassUnitID");
            CreateIndex("dbo.ApplicationUserClassUnits", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserClassUnits", "ClassUnit_ClassUnitID", "dbo.ClassUnits", "ClassUnitID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserClassUnits", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Dossiers", newName: "Files");
        }
    }
}
