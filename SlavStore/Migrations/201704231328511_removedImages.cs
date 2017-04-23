namespace SlavStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Item_Id", "dbo.Items");
            DropIndex("dbo.Images", new[] { "Item_Id" });
            AddColumn("dbo.Items", "Image", c => c.String());
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(nullable: false),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Items", "Image");
            CreateIndex("dbo.Images", "Item_Id");
            AddForeignKey("dbo.Images", "Item_Id", "dbo.Items", "Id");
        }
    }
}
