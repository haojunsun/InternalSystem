namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upReleaseTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "ReleaseDateTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "ReleaseDateTime");
        }
    }
}
