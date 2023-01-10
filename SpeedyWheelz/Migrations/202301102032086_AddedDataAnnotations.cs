namespace SpeedyWheelz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataAnnotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories");
            DropIndex("dbo.Products", new[] { "AlcoholCategoryId" });
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "AlcoholCategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.Products", "AlcoholCategoryId");
            AddForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories");
            DropIndex("dbo.Products", new[] { "AlcoholCategoryId" });
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "AlcoholCategoryId", c => c.Int());
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            CreateIndex("dbo.Products", "AlcoholCategoryId");
            AddForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories", "Id");
        }
    }
}
