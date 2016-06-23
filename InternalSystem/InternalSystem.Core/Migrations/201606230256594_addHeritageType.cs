namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHeritageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "HeritageType", c => c.Int(nullable: false));
            AddColumn("dbo.WorldHeritages", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "Content");
            DropColumn("dbo.WorldHeritages", "HeritageType");
        }
    }
}
