namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsEffect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "IsEffect", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "IsEffect");
        }
    }
}
