namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberInOder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Street", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Street", c => c.String());
            AlterColumn("dbo.Addresses", "LastName", c => c.String());
            AlterColumn("dbo.Addresses", "FirstName", c => c.String());
            DropColumn("dbo.Addresses", "PhoneNumber");
        }
    }
}
