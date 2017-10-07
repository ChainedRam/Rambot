namespace KLD.TeamSpeak.Repository
{
    using System.Data.Entity.Migrations;

    public partial class Whatdidichange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clip",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Source = c.String(maxLength: 100, unicode: false),
                        Length = c.Double(),
                        AddedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.ClipUse",
                c => new
                    {
                        UniqueId = c.String(nullable: false, maxLength: 40, unicode: false),
                        ClipName = c.String(nullable: false, maxLength: 20, unicode: false),
                        playedCount = c.Int(),
                    })
                .PrimaryKey(t => new { t.UniqueId, t.ClipName })
                .ForeignKey("dbo.TeamSpeakUser", t => t.UniqueId)
                .ForeignKey("dbo.Clip", t => t.ClipName)
                .Index(t => t.UniqueId)
                .Index(t => t.ClipName);
            
            CreateTable(
                "dbo.TeamSpeakUser",
                c => new
                    {
                        UniqueId = c.String(nullable: false, maxLength: 40, unicode: false),
                        Nickname = c.String(maxLength: 100, fixedLength: true),
                        ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.UniqueId);
            
            CreateTable(
                "dbo.IntroClip",
                c => new
                    {
                        ClipName = c.String(nullable: false, maxLength: 20, unicode: false),
                        UniqueId = c.String(nullable: false, maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => new { t.ClipName, t.UniqueId })
                .ForeignKey("dbo.Clip", t => t.ClipName, cascadeDelete: true)
                .ForeignKey("dbo.TeamSpeakUser", t => t.UniqueId, cascadeDelete: true)
                .Index(t => t.ClipName)
                .Index(t => t.UniqueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntroClip", "UniqueId", "dbo.TeamSpeakUser");
            DropForeignKey("dbo.IntroClip", "ClipName", "dbo.Clip");
            DropForeignKey("dbo.ClipUse", "ClipName", "dbo.Clip");
            DropForeignKey("dbo.ClipUse", "UniqueId", "dbo.TeamSpeakUser");
            DropIndex("dbo.IntroClip", new[] { "UniqueId" });
            DropIndex("dbo.IntroClip", new[] { "ClipName" });
            DropIndex("dbo.ClipUse", new[] { "ClipName" });
            DropIndex("dbo.ClipUse", new[] { "UniqueId" });
            DropTable("dbo.IntroClip");
            DropTable("dbo.TeamSpeakUser");
            DropTable("dbo.ClipUse");
            DropTable("dbo.Clip");
        }
    }
}
