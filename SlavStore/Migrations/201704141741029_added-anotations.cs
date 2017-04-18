namespace SlavStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedanotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Items", "Seller_Id", "dbo.Stores");
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropIndex("dbo.Items", new[] { "Seller_Id" });
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Items", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "Seller_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Comments", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Images", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Items", "Category_Id");
            CreateIndex("dbo.Items", "Seller_Id");
            AddForeignKey("dbo.Items", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Items", "Seller_Id", "dbo.Stores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Seller_Id", "dbo.Stores");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "Seller_Id" });
            DropIndex("dbo.Items", new[] { "Category_Id" });
            AlterColumn("dbo.Stores", "Name", c => c.String());
            AlterColumn("dbo.Images", "Url", c => c.String());
            AlterColumn("dbo.Comments", "Title", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Items", "Seller_Id", c => c.Int());
            AlterColumn("dbo.Items", "Category_Id", c => c.Int());
            AlterColumn("dbo.Items", "Name", c => c.String());
            CreateIndex("dbo.Items", "Seller_Id");
            CreateIndex("dbo.Items", "Category_Id");
            AddForeignKey("dbo.Items", "Seller_Id", "dbo.Stores", "Id");
            AddForeignKey("dbo.Items", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
