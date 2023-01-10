namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductForiegnKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories");
            DropIndex("dbo.Products", new[] { "AlcoholCategoryId" });
            AlterColumn("dbo.Products", "AlcoholCategoryId", c => c.Int());
            CreateIndex("dbo.Products", "AlcoholCategoryId");
            AddForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories");
            DropIndex("dbo.Products", new[] { "AlcoholCategoryId" });
            AlterColumn("dbo.Products", "AlcoholCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "AlcoholCategoryId");
            AddForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories", "Id", cascadeDelete: true);
        }
    }
}
