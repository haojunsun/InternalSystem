namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Audituser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "Audit", c => c.String());
            AddColumn("dbo.WorldHeritages", "AuditTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "AuditTime");
            DropColumn("dbo.WorldHeritages", "Audit");
        }
    }
}
