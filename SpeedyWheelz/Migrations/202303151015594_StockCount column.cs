namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockCountcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "stockCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "stockCount");
        }
    }
}
