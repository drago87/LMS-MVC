namespace Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class folderfileidFK : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Folders", "FileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Folders", "FileID", c => c.Int(nullable: false));
        }
    }
}
