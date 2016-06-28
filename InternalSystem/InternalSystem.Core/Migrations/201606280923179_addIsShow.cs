namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsShow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "IsShow", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "IsShow");
        }
    }
}
