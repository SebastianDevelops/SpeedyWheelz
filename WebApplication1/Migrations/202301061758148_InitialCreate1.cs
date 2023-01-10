namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlcoholCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Image = c.String(),
                        ImageUrl = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AlcoholCategoryId = c.Int(nullable: false),
                        TobaccoCategoryId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                        Description = c.String(),
                        isAlcohol = c.Boolean(nullable: false),
                        isTobacco = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.AlcoholCategories", t => t.AlcoholCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.TobaccoCategories", t => t.TobaccoCategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.AlcoholCategoryId)
                .Index(t => t.TobaccoCategoryId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TobaccoCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "TobaccoCategoryId", "dbo.TobaccoCategories");
            DropForeignKey("dbo.Products", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "AlcoholCategoryId", "dbo.AlcoholCategories");
            DropIndex("dbo.Products", new[] { "TagId" });
            DropIndex("dbo.Products", new[] { "TobaccoCategoryId" });
            DropIndex("dbo.Products", new[] { "AlcoholCategoryId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.TobaccoCategories");
            DropTable("dbo.Tags");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.AlcoholCategories");
        }
    }
}
