namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pushNotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.pushSubscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Endpoint = c.String(nullable: false),
                        Auth = c.String(nullable: false),
                        P256dh = c.String(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pushSubscriptions", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.pushSubscriptions", new[] { "ApplicationUserId" });
            DropTable("dbo.pushSubscriptions");
        }
    }
}
