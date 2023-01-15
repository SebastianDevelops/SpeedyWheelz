namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createddrivertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DriverId", c => c.String());
            AddColumn("dbo.Orders", "isAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "isAssigned");
            DropColumn("dbo.Orders", "DriverId");
        }
    }
}
