namespace SlavStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storeUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Owner_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Stores", "Owner_Id");
            AddForeignKey("dbo.Stores", "Owner_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Stores", new[] { "Owner_Id" });
            DropColumn("dbo.Stores", "Owner_Id");
        }
    }
}
