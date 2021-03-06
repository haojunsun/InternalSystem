namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newOrigin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorldHeritages", "OtherTitle", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevelOfArtClassification", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevelOfArtClassification", c => c.String());
            AddColumn("dbo.WorldHeritages", "ThirdLevelOfArtClassification", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevelOfEthnicGroup", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevelOfEthnicGroup", c => c.String());
            AddColumn("dbo.WorldHeritages", "Subgroup", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevelOfSocialAttributes", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevelOfSocialAttributes", c => c.String());
            AddColumn("dbo.WorldHeritages", "TimesProperty", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevelOfElements", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevelOfElements", c => c.String());
            AddColumn("dbo.WorldHeritages", "ThirdLevelOfElements", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevelOfCharacter", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevelOfCharacter", c => c.String());
            AddColumn("dbo.WorldHeritages", "ArtSchool", c => c.String());
            AddColumn("dbo.WorldHeritages", "PerformingForm", c => c.String());
            AddColumn("dbo.WorldHeritages", "Structure", c => c.String());
            AddColumn("dbo.WorldHeritages", "SongPattern", c => c.String());
            AddColumn("dbo.WorldHeritages", "BeatsPattern", c => c.String());
            AddColumn("dbo.WorldHeritages", "MusicModeName", c => c.String());
            AddColumn("dbo.WorldHeritages", "GongCheModeName", c => c.String());
            AddColumn("dbo.WorldHeritages", "NumberedMusicalNotationModeName", c => c.String());
            AddColumn("dbo.WorldHeritages", "JiuGongDaChengModeName", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevelOfSubjectClassification", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevelOfResourcesClassification", c => c.String());
            AddColumn("dbo.WorldHeritages", "ThirdLevelOfResourcesClassification", c => c.String());
            AddColumn("dbo.WorldHeritages", "Keywords", c => c.String());
            AddColumn("dbo.WorldHeritages", "Abstract", c => c.String());
            AddColumn("dbo.WorldHeritages", "NameOfAwards", c => c.String());
            AddColumn("dbo.WorldHeritages", "YearOrTimeOfAwards", c => c.String());
            AddColumn("dbo.WorldHeritages", "Awarder", c => c.String());
            AddColumn("dbo.WorldHeritages", "Type", c => c.String());
            AddColumn("dbo.WorldHeritages", "Name", c => c.String());
            AddColumn("dbo.WorldHeritages", "Role", c => c.String());
            AddColumn("dbo.WorldHeritages", "NationalityOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "BirthplaceOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "GenderOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "AgeOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "EducationLevelOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "VocationOfRelatedPeople", c => c.String());
            AddColumn("dbo.WorldHeritages", "TemporalType", c => c.String());
            AddColumn("dbo.WorldHeritages", "FromHistoricalCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "ToHistoricalCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "FromADCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "ToADCalendar", c => c.String());
            AddColumn("dbo.WorldHeritages", "SpatialType", c => c.String());
            AddColumn("dbo.WorldHeritages", "HistoricalPlaceName", c => c.String());
            AddColumn("dbo.WorldHeritages", "PresentPlaceName", c => c.String());
            AddColumn("dbo.WorldHeritages", "OtherPlaceName", c => c.String());
            AddColumn("dbo.WorldHeritages", "Province", c => c.String());
            AddColumn("dbo.WorldHeritages", "City", c => c.String());
            AddColumn("dbo.WorldHeritages", "County", c => c.String());
            AddColumn("dbo.WorldHeritages", "Town", c => c.String());
            AddColumn("dbo.WorldHeritages", "Venues", c => c.String());
            AddColumn("dbo.WorldHeritages", "LongitudeLatitude", c => c.String());
            AddColumn("dbo.WorldHeritages", "HasPart", c => c.String());
            AddColumn("dbo.WorldHeritages", "IsPartOf", c => c.String());
            AddColumn("dbo.WorldHeritages", "Refer", c => c.String());
            AddColumn("dbo.WorldHeritages", "HasFormat", c => c.String());
            AddColumn("dbo.WorldHeritages", "NameReferences", c => c.String());
            AddColumn("dbo.WorldHeritages", "NameofReferencesCreator", c => c.String());
            AddColumn("dbo.WorldHeritages", "RoleofReferencesCreator", c => c.String());
            AddColumn("dbo.WorldHeritages", "ISBNISRC", c => c.String());
            AddColumn("dbo.WorldHeritages", "ReferencesFormat", c => c.String());
            AddColumn("dbo.WorldHeritages", "AcquisitionMethod", c => c.String());
            AddColumn("dbo.WorldHeritages", "ProviderOfReferences", c => c.String());
            AddColumn("dbo.WorldHeritages", "RepositoryNamePublisher", c => c.String());
            AddColumn("dbo.WorldHeritages", "PublicationDate", c => c.String());
            AddColumn("dbo.WorldHeritages", "PlaceOfPublication", c => c.String());
            AddColumn("dbo.WorldHeritages", "DigitalObjectFormat", c => c.String());
            AddColumn("dbo.WorldHeritages", "DigitalSpecification", c => c.String());
            AddColumn("dbo.WorldHeritages", "AudioVideoBitRate", c => c.String());
            AddColumn("dbo.WorldHeritages", "AudioSamplingFrequency", c => c.String());
            AddColumn("dbo.WorldHeritages", "NumberOfChannels", c => c.String());
            AddColumn("dbo.WorldHeritages", "FilePath", c => c.String());
            AddColumn("dbo.WorldHeritages", "DisplayType", c => c.String());
            AddColumn("dbo.WorldHeritages", "CDNumber", c => c.String());
            AddColumn("dbo.WorldHeritages", "Holder", c => c.String());
            AddColumn("dbo.WorldHeritages", "RightsDate", c => c.String());
            AddColumn("dbo.WorldHeritages", "Licens", c => c.String());
            AddColumn("dbo.WorldHeritages", "Note", c => c.String());
            AddColumn("dbo.WorldHeritages", "Recorder", c => c.String());
            AddColumn("dbo.WorldHeritages", "RecordingTime", c => c.String());
            AddColumn("dbo.WorldHeritages", "Modifier", c => c.String());
            AddColumn("dbo.WorldHeritages", "ModifiedDate", c => c.String());
            AddColumn("dbo.WorldHeritages", "MetadataAuditor", c => c.String());
            AddColumn("dbo.WorldHeritages", "MetadataAuditoTime", c => c.String());
            AddColumn("dbo.WorldHeritages", "MetadataManagement", c => c.String());
            DropColumn("dbo.WorldHeritages", "Title");
            DropColumn("dbo.WorldHeritages", "FirstLevel");
            DropColumn("dbo.WorldHeritages", "SecondLevel");
            DropColumn("dbo.WorldHeritages", "ProjectCode");
            DropColumn("dbo.WorldHeritages", "SubItemNumber");
            DropColumn("dbo.WorldHeritages", "Batch");
            DropColumn("dbo.WorldHeritages", "Notes");
            DropColumn("dbo.WorldHeritages", "ApplicationTime");
            DropColumn("dbo.WorldHeritages", "Nation");
            DropColumn("dbo.WorldHeritages", "NationalBranch");
            DropColumn("dbo.WorldHeritages", "Gender");
            DropColumn("dbo.WorldHeritages", "Birth");
            DropColumn("dbo.WorldHeritages", "Education");
            DropColumn("dbo.WorldHeritages", "Occupation");
            DropColumn("dbo.WorldHeritages", "Successor");
            DropColumn("dbo.WorldHeritages", "Genre");
            DropColumn("dbo.WorldHeritages", "Municipalities");
            DropColumn("dbo.WorldHeritages", "Region");
            DropColumn("dbo.WorldHeritages", "CountyLevelCity");
            DropColumn("dbo.WorldHeritages", "Township");
            DropColumn("dbo.WorldHeritages", "DeclarationArea");
            DropColumn("dbo.WorldHeritages", "ProtectionUnit");
            DropColumn("dbo.WorldHeritages", "EcologicalZoneProject");
            DropColumn("dbo.WorldHeritages", "ProductiveProtectionBase");
            DropColumn("dbo.WorldHeritages", "ProvinceCode");
            DropColumn("dbo.WorldHeritages", "PostTime");
            DropColumn("dbo.WorldHeritages", "Death");
            DropColumn("dbo.WorldHeritages", "Adopt");
            DropColumn("dbo.WorldHeritages", "CreatorName");
            DropColumn("dbo.WorldHeritages", "ResponsibilityCreator");
            DropColumn("dbo.WorldHeritages", "DataFormat");
            DropColumn("dbo.WorldHeritages", "DataProvider");
            DropColumn("dbo.WorldHeritages", "CollectionUnit");
            DropColumn("dbo.WorldHeritages", "DigitalFormat");
            DropColumn("dbo.WorldHeritages", "ResolvingPower");
            DropColumn("dbo.WorldHeritages", "KBps");
            DropColumn("dbo.WorldHeritages", "SamplingFrequency");
            DropColumn("dbo.WorldHeritages", "Channels");
            DropColumn("dbo.WorldHeritages", "StoragePlace");
            DropColumn("dbo.WorldHeritages", "DisplayLevel");
            DropColumn("dbo.WorldHeritages", "Description");
            DropColumn("dbo.WorldHeritages", "DescriptionTime");
            DropColumn("dbo.WorldHeritages", "Audit");
            DropColumn("dbo.WorldHeritages", "AuditTime");
            DropColumn("dbo.WorldHeritages", "Organization");
            DropColumn("dbo.WorldHeritages", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorldHeritages", "Remarks", c => c.String());
            AddColumn("dbo.WorldHeritages", "Organization", c => c.String());
            AddColumn("dbo.WorldHeritages", "AuditTime", c => c.String());
            AddColumn("dbo.WorldHeritages", "Audit", c => c.String());
            AddColumn("dbo.WorldHeritages", "DescriptionTime", c => c.String());
            AddColumn("dbo.WorldHeritages", "Description", c => c.String());
            AddColumn("dbo.WorldHeritages", "DisplayLevel", c => c.String());
            AddColumn("dbo.WorldHeritages", "StoragePlace", c => c.String());
            AddColumn("dbo.WorldHeritages", "Channels", c => c.String());
            AddColumn("dbo.WorldHeritages", "SamplingFrequency", c => c.String());
            AddColumn("dbo.WorldHeritages", "KBps", c => c.String());
            AddColumn("dbo.WorldHeritages", "ResolvingPower", c => c.String());
            AddColumn("dbo.WorldHeritages", "DigitalFormat", c => c.String());
            AddColumn("dbo.WorldHeritages", "CollectionUnit", c => c.String());
            AddColumn("dbo.WorldHeritages", "DataProvider", c => c.String());
            AddColumn("dbo.WorldHeritages", "DataFormat", c => c.String());
            AddColumn("dbo.WorldHeritages", "ResponsibilityCreator", c => c.String());
            AddColumn("dbo.WorldHeritages", "CreatorName", c => c.String());
            AddColumn("dbo.WorldHeritages", "Adopt", c => c.String());
            AddColumn("dbo.WorldHeritages", "Death", c => c.String());
            AddColumn("dbo.WorldHeritages", "PostTime", c => c.String());
            AddColumn("dbo.WorldHeritages", "ProvinceCode", c => c.String());
            AddColumn("dbo.WorldHeritages", "ProductiveProtectionBase", c => c.String());
            AddColumn("dbo.WorldHeritages", "EcologicalZoneProject", c => c.String());
            AddColumn("dbo.WorldHeritages", "ProtectionUnit", c => c.String());
            AddColumn("dbo.WorldHeritages", "DeclarationArea", c => c.String());
            AddColumn("dbo.WorldHeritages", "Township", c => c.String());
            AddColumn("dbo.WorldHeritages", "CountyLevelCity", c => c.String());
            AddColumn("dbo.WorldHeritages", "Region", c => c.String());
            AddColumn("dbo.WorldHeritages", "Municipalities", c => c.String());
            AddColumn("dbo.WorldHeritages", "Genre", c => c.String());
            AddColumn("dbo.WorldHeritages", "Successor", c => c.String());
            AddColumn("dbo.WorldHeritages", "Occupation", c => c.String());
            AddColumn("dbo.WorldHeritages", "Education", c => c.String());
            AddColumn("dbo.WorldHeritages", "Birth", c => c.String());
            AddColumn("dbo.WorldHeritages", "Gender", c => c.String());
            AddColumn("dbo.WorldHeritages", "NationalBranch", c => c.String());
            AddColumn("dbo.WorldHeritages", "Nation", c => c.String());
            AddColumn("dbo.WorldHeritages", "ApplicationTime", c => c.String());
            AddColumn("dbo.WorldHeritages", "Notes", c => c.String());
            AddColumn("dbo.WorldHeritages", "Batch", c => c.String());
            AddColumn("dbo.WorldHeritages", "SubItemNumber", c => c.String());
            AddColumn("dbo.WorldHeritages", "ProjectCode", c => c.String());
            AddColumn("dbo.WorldHeritages", "SecondLevel", c => c.String());
            AddColumn("dbo.WorldHeritages", "FirstLevel", c => c.String());
            AddColumn("dbo.WorldHeritages", "Title", c => c.String());
            DropColumn("dbo.WorldHeritages", "MetadataManagement");
            DropColumn("dbo.WorldHeritages", "MetadataAuditoTime");
            DropColumn("dbo.WorldHeritages", "MetadataAuditor");
            DropColumn("dbo.WorldHeritages", "ModifiedDate");
            DropColumn("dbo.WorldHeritages", "Modifier");
            DropColumn("dbo.WorldHeritages", "RecordingTime");
            DropColumn("dbo.WorldHeritages", "Recorder");
            DropColumn("dbo.WorldHeritages", "Note");
            DropColumn("dbo.WorldHeritages", "Licens");
            DropColumn("dbo.WorldHeritages", "RightsDate");
            DropColumn("dbo.WorldHeritages", "Holder");
            DropColumn("dbo.WorldHeritages", "CDNumber");
            DropColumn("dbo.WorldHeritages", "DisplayType");
            DropColumn("dbo.WorldHeritages", "FilePath");
            DropColumn("dbo.WorldHeritages", "NumberOfChannels");
            DropColumn("dbo.WorldHeritages", "AudioSamplingFrequency");
            DropColumn("dbo.WorldHeritages", "AudioVideoBitRate");
            DropColumn("dbo.WorldHeritages", "DigitalSpecification");
            DropColumn("dbo.WorldHeritages", "DigitalObjectFormat");
            DropColumn("dbo.WorldHeritages", "PlaceOfPublication");
            DropColumn("dbo.WorldHeritages", "PublicationDate");
            DropColumn("dbo.WorldHeritages", "RepositoryNamePublisher");
            DropColumn("dbo.WorldHeritages", "ProviderOfReferences");
            DropColumn("dbo.WorldHeritages", "AcquisitionMethod");
            DropColumn("dbo.WorldHeritages", "ReferencesFormat");
            DropColumn("dbo.WorldHeritages", "ISBNISRC");
            DropColumn("dbo.WorldHeritages", "RoleofReferencesCreator");
            DropColumn("dbo.WorldHeritages", "NameofReferencesCreator");
            DropColumn("dbo.WorldHeritages", "NameReferences");
            DropColumn("dbo.WorldHeritages", "HasFormat");
            DropColumn("dbo.WorldHeritages", "Refer");
            DropColumn("dbo.WorldHeritages", "IsPartOf");
            DropColumn("dbo.WorldHeritages", "HasPart");
            DropColumn("dbo.WorldHeritages", "LongitudeLatitude");
            DropColumn("dbo.WorldHeritages", "Venues");
            DropColumn("dbo.WorldHeritages", "Town");
            DropColumn("dbo.WorldHeritages", "County");
            DropColumn("dbo.WorldHeritages", "City");
            DropColumn("dbo.WorldHeritages", "Province");
            DropColumn("dbo.WorldHeritages", "OtherPlaceName");
            DropColumn("dbo.WorldHeritages", "PresentPlaceName");
            DropColumn("dbo.WorldHeritages", "HistoricalPlaceName");
            DropColumn("dbo.WorldHeritages", "SpatialType");
            DropColumn("dbo.WorldHeritages", "ToADCalendar");
            DropColumn("dbo.WorldHeritages", "FromADCalendar");
            DropColumn("dbo.WorldHeritages", "ToHistoricalCalendar");
            DropColumn("dbo.WorldHeritages", "FromHistoricalCalendar");
            DropColumn("dbo.WorldHeritages", "TemporalType");
            DropColumn("dbo.WorldHeritages", "VocationOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "EducationLevelOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "AgeOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "GenderOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "BirthplaceOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "NationalityOfRelatedPeople");
            DropColumn("dbo.WorldHeritages", "Role");
            DropColumn("dbo.WorldHeritages", "Name");
            DropColumn("dbo.WorldHeritages", "Type");
            DropColumn("dbo.WorldHeritages", "Awarder");
            DropColumn("dbo.WorldHeritages", "YearOrTimeOfAwards");
            DropColumn("dbo.WorldHeritages", "NameOfAwards");
            DropColumn("dbo.WorldHeritages", "Abstract");
            DropColumn("dbo.WorldHeritages", "Keywords");
            DropColumn("dbo.WorldHeritages", "ThirdLevelOfResourcesClassification");
            DropColumn("dbo.WorldHeritages", "SecondLevelOfResourcesClassification");
            DropColumn("dbo.WorldHeritages", "FirstLevelOfSubjectClassification");
            DropColumn("dbo.WorldHeritages", "JiuGongDaChengModeName");
            DropColumn("dbo.WorldHeritages", "NumberedMusicalNotationModeName");
            DropColumn("dbo.WorldHeritages", "GongCheModeName");
            DropColumn("dbo.WorldHeritages", "MusicModeName");
            DropColumn("dbo.WorldHeritages", "BeatsPattern");
            DropColumn("dbo.WorldHeritages", "SongPattern");
            DropColumn("dbo.WorldHeritages", "Structure");
            DropColumn("dbo.WorldHeritages", "PerformingForm");
            DropColumn("dbo.WorldHeritages", "ArtSchool");
            DropColumn("dbo.WorldHeritages", "SecondLevelOfCharacter");
            DropColumn("dbo.WorldHeritages", "FirstLevelOfCharacter");
            DropColumn("dbo.WorldHeritages", "ThirdLevelOfElements");
            DropColumn("dbo.WorldHeritages", "SecondLevelOfElements");
            DropColumn("dbo.WorldHeritages", "FirstLevelOfElements");
            DropColumn("dbo.WorldHeritages", "TimesProperty");
            DropColumn("dbo.WorldHeritages", "SecondLevelOfSocialAttributes");
            DropColumn("dbo.WorldHeritages", "FirstLevelOfSocialAttributes");
            DropColumn("dbo.WorldHeritages", "Subgroup");
            DropColumn("dbo.WorldHeritages", "SecondLevelOfEthnicGroup");
            DropColumn("dbo.WorldHeritages", "FirstLevelOfEthnicGroup");
            DropColumn("dbo.WorldHeritages", "ThirdLevelOfArtClassification");
            DropColumn("dbo.WorldHeritages", "SecondLevelOfArtClassification");
            DropColumn("dbo.WorldHeritages", "FirstLevelOfArtClassification");
            DropColumn("dbo.WorldHeritages", "OtherTitle");
        }
    }
}
