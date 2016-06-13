namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorldHeritagesTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "CreatedUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "CreatedUtc");
        }
    }
}
