namespace Games_Catalog_N01589651.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_fk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "DeveloperId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "GenreId");
            CreateIndex("dbo.Games", "DeveloperId");
            AddForeignKey("dbo.Games", "DeveloperId", "dbo.Developers", "DeveloperId", cascadeDelete: true);
            AddForeignKey("dbo.Games", "GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Games", "DeveloperId", "dbo.Developers");
            DropIndex("dbo.Games", new[] { "DeveloperId" });
            DropIndex("dbo.Games", new[] { "GenreId" });
            DropColumn("dbo.Games", "DeveloperId");
            DropColumn("dbo.Games", "GenreId");
        }
    }
}
