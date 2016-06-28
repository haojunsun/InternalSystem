namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRelease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "ReleaseTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorldHeritages", "Release_ManagerId", c => c.Int());
            CreateIndex("dbo.WorldHeritages", "Release_ManagerId");
            AddForeignKey("dbo.WorldHeritages", "Release_ManagerId", "dbo.Managers", "ManagerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorldHeritages", "Release_ManagerId", "dbo.Managers");
            DropIndex("dbo.WorldHeritages", new[] { "Release_ManagerId" });
            DropColumn("dbo.WorldHeritages", "Release_ManagerId");
            DropColumn("dbo.WorldHeritages", "ReleaseTime");
        }
    }
}
