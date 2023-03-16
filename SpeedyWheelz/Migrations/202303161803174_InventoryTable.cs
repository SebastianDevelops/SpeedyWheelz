namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product = c.String(),
                        OriginalQuant = c.Int(nullable: false),
                        QuantSold = c.Int(nullable: false),
                        RemainingQuant = c.Int(nullable: false),
                        TotalSold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryManagements");
        }
    }
}
