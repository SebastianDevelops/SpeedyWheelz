namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedImageTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            DropTable("dbo.ProductImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            DropColumn("dbo.Products", "ImageUrl");
            CreateIndex("dbo.ProductImages", "ProductId");
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
