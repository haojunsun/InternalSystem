namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invalid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Managers", "Invalid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Managers", "Invalid");
        }
    }
}
