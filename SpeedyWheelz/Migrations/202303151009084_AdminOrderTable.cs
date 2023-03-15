namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AddressId = c.Int(nullable: false),
                        OrderStatus = c.String(),
                        DriverId = c.String(),
                        isAssigned = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CartItemsJsonItems = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminOrders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AdminOrders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.AdminOrders", new[] { "AddressId" });
            DropIndex("dbo.AdminOrders", new[] { "UserId" });
            DropTable("dbo.AdminOrders");
        }
    }
}
