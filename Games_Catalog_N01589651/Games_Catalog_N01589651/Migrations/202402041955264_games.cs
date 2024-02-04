namespace Games_Catalog_N01589651.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false, identity: true),
                        DeveloperStudioName = c.String(),
                    })
                .PrimaryKey(t => t.DeveloperId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Price = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Developers");
        }
    }
}
