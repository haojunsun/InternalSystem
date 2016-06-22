namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WorldHeritages", name: "Manager_ManagerId", newName: "User_ManagerId");
            RenameIndex(table: "dbo.WorldHeritages", name: "IX_Manager_ManagerId", newName: "IX_User_ManagerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.WorldHeritages", name: "IX_User_ManagerId", newName: "IX_Manager_ManagerId");
            RenameColumn(table: "dbo.WorldHeritages", name: "User_ManagerId", newName: "Manager_ManagerId");
        }
    }
}
