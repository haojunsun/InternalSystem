namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "RelatedPeople_ArtificialID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorldHeritages", "RelatedPeople_ArtificialID");
        }
    }
}
