namespace Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classunit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassUnits",
                c => new
                    {
                        ClassUnitID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        Shared_FolderID = c.Int(),
                        Submission_FolderID = c.Int(),
                    })
                .PrimaryKey(t => t.ClassUnitID)
                .ForeignKey("dbo.Folders", t => t.Shared_FolderID)
                .ForeignKey("dbo.Folders", t => t.Submission_FolderID)
                .Index(t => t.Shared_FolderID)
                .Index(t => t.Submission_FolderID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        StopTime = c.DateTime(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        ClassUnitID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.ClassUnits", t => t.ClassUnitID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.ClassUnitID);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        FolderID = c.Int(nullable: false, identity: true),
                        FolderName = c.String(),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FolderID);
            
            CreateTable(
                "dbo.Dossiers",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        Folder_FolderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.Folders", t => t.Folder_FolderID, cascadeDelete: true)
                .Index(t => t.Folder_FolderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassUnits", "Submission_FolderID", "dbo.Folders");
            DropForeignKey("dbo.ClassUnits", "Shared_FolderID", "dbo.Folders");
            DropForeignKey("dbo.Dossiers", "Folder_FolderID", "dbo.Folders");
            DropForeignKey("dbo.Lessons", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Lessons", "ClassUnitID", "dbo.ClassUnits");
            DropIndex("dbo.Dossiers", new[] { "Folder_FolderID" });
            DropIndex("dbo.Lessons", new[] { "ClassUnitID" });
            DropIndex("dbo.Lessons", new[] { "SubjectID" });
            DropIndex("dbo.ClassUnits", new[] { "Submission_FolderID" });
            DropIndex("dbo.ClassUnits", new[] { "Shared_FolderID" });
            DropTable("dbo.Dossiers");
            DropTable("dbo.Folders");
            DropTable("dbo.Lessons");
            DropTable("dbo.ClassUnits");
        }
    }
}
