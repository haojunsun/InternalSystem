namespace InternalSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CatchLine = c.String(maxLength: 30),
                        TitleImageUrl = c.String(),
                        Body = c.String(),
                        CreatedUtc = c.DateTime(),
                        ChannelTags = c.String(),
                        ColumnTags = c.String(),
                        IsDraft = c.Int(nullable: false),
                        IsRelease = c.Int(nullable: false),
                        OtherImageUrl = c.String(),
                        ImageUrl = c.String(),
                        ManagerName = c.String(),
                        MediaUrl = c.String(),
                    })
                .PrimaryKey(t => t.ArticleId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LoginId = c.String(),
                        Pass = c.String(),
                        CreatedUtc = c.DateTime(),
                        Authority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Managers");
            DropTable("dbo.Articles");
        }
    }
}
