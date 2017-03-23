namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataFormat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "DataFormat", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "DataFormat");
        }
    }
}
