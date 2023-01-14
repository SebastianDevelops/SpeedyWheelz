namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTableCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        Country = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        AddressId = c.Int(nullable: false),
                        OrderStatus = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CartItemsJsonItems = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.AddressId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "ApplicationUserId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Addresses");
        }
    }
}
