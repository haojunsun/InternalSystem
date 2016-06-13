namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorldHeritages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorldHeritages",
                c => new
                    {
                        WorldHeritageId = c.Int(nullable: false, identity: true),
                        InventoryId = c.String(),
                        ArtificialId = c.String(),
                        TitleProper = c.String(),
                        Title = c.String(),
                        FirstLevel = c.String(),
                        SecondLevel = c.String(),
                        ProjectCode = c.String(),
                        SubItemNumber = c.String(),
                        Batch = c.String(),
                        Notes = c.String(),
                        ApplicationTime = c.String(),
                        Nation = c.String(),
                        NationalBranch = c.String(),
                        Gender = c.String(),
                        Birth = c.String(),
                        Education = c.String(),
                        Occupation = c.String(),
                        Successor = c.String(),
                        Genre = c.String(),
                        Municipalities = c.String(),
                        Region = c.String(),
                        CountyLevelCity = c.String(),
                        Township = c.String(),
                        Village = c.String(),
                        DeclarationArea = c.String(),
                        ProtectionUnit = c.String(),
                        EcologicalZoneProject = c.String(),
                        ProductiveProtectionBase = c.String(),
                        ProvinceCode = c.String(),
                        PostTime = c.String(),
                        Death = c.String(),
                        Adopt = c.String(),
                        Language = c.String(),
                        CreatorName = c.String(),
                        ResponsibilityCreator = c.String(),
                        DataFormat = c.String(),
                        DataProvider = c.String(),
                        CollectionUnit = c.String(),
                        DigitalFormat = c.String(),
                        Size = c.String(),
                        Duration = c.String(),
                        ResolvingPower = c.String(),
                        KBps = c.String(),
                        SamplingFrequency = c.String(),
                        Channels = c.String(),
                        StoragePlace = c.String(),
                        DisplayLevel = c.String(),
                        Description = c.String(),
                        DescriptionTime = c.String(),
                        Audit = c.String(),
                        AuditTime = c.String(),
                        Organization = c.String(),
                        Remarks = c.String(),
                        url = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.WorldHeritageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorldHeritages");
        }
    }
}
