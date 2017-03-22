namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "RepositoryName", c => c.String());
            AddColumn("dbo.WorldHeritages", "Publisher", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_Name", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_Role", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_NationalityOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_BirthplaceOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_GenderOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_AgeOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_EducationLevelOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "RelatedPeople_VocationOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageTemporal_ArtificialID", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageTemporal_TemporalType", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageTemporal_FromHistoricalCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageTemporal_ToHistoricalCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageTemporal_FromADCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageTemporal_ToADCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_ArtificialID", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_SpatialType", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_HistoricalPlaceName", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_PresentPlaceName", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_OtherPlaceName", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_province", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_City", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_County", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_Town", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_Village", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_Venues", c => c.String());
            AddColumn("dbo.WorldHeritages", "CoverageSpatial_LongitudeLatitude", c => c.String());
            AddColumn("dbo.WorldHeritages", "CreatorofReferences_ArtificialID", c => c.String());
            AddColumn("dbo.WorldHeritages", "CreatorofReferences_NameofCreator", c => c.String());
            AddColumn("dbo.WorldHeritages", "CreatorofReferences_RoleofReferencesCreator", c => c.String());
            DropColumn("dbo.WorldHeritages", "RepositoryNamePublisher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorldHeritages", "RepositoryNamePublisher", c => c.String());
            DropColumn("dbo.WorldHeritages", "CreatorofReferences_RoleofReferencesCreator");
            DropColumn("dbo.WorldHeritages", "CreatorofReferences_NameofCreator");
            DropColumn("dbo.WorldHeritages", "CreatorofReferences_ArtificialID");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_LongitudeLatitude");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_Venues");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_Village");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_Town");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_County");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_City");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_province");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_OtherPlaceName");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_PresentPlaceName");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_HistoricalPlaceName");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_SpatialType");
            DropColumn("dbo.WorldHeritages", "CoverageSpatial_ArtificialID");
            DropColumn("dbo.WorldHeritages", "CoverageTemporal_ToADCalendar");
            DropColumn("dbo.WorldHeritages", "CoverageTemporal_FromADCalendar");
            DropColumn("dbo.WorldHeritages", "CoverageTemporal_ToHistoricalCalendar");
            DropColumn("dbo.WorldHeritages", "CoverageTemporal_FromHistoricalCalendar");
            DropColumn("dbo.WorldHeritages", "CoverageTemporal_TemporalType");
            DropColumn("dbo.WorldHeritages", "CoverageTemporal_ArtificialID");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_VocationOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_EducationLevelOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_AgeOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_GenderOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_BirthplaceOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_NationalityOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_Role");
            DropColumn("dbo.WorldHeritages", "RelatedPeople_Name");
            DropColumn("dbo.WorldHeritages", "Publisher");
            DropColumn("dbo.WorldHeritages", "RepositoryName");
        }
    }
}
