namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWorldHeritageManager : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "Manager_ManagerId", c => c.Int());
            CreateIndex("dbo.WorldHeritages", "Manager_ManagerId");
            AddForeignKey("dbo.WorldHeritages", "Manager_ManagerId", "dbo.Managers", "ManagerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorldHeritages", "Manager_ManagerId", "dbo.Managers");
            DropIndex("dbo.WorldHeritages", new[] { "Manager_ManagerId" });
            DropColumn("dbo.WorldHeritages", "Manager_ManagerId");
        }
    }
}
