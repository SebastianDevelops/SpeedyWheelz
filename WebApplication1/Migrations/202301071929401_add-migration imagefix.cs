namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationimagefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductImages", "ImagePath", c => c.String());
            DropColumn("dbo.ProductImages", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductImages", "ImageUrl", c => c.String());
            DropColumn("dbo.ProductImages", "ImagePath");
        }
    }
}
