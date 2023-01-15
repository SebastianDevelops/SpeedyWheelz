namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIDFK : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Orders", "UserId");
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "UserId", c => c.String());
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Orders", "UserId", c => c.String());
            CreateIndex("dbo.Orders", "ApplicationUser_Id");
        }
    }
}
