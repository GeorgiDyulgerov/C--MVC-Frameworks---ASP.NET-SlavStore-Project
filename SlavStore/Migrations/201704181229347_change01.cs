namespace SlavStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change01 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemApplicationUsers", newName: "ApplicationUserItems");
            DropPrimaryKey("dbo.ApplicationUserItems");
            AddPrimaryKey("dbo.ApplicationUserItems", new[] { "ApplicationUser_Id", "Item_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserItems");
            AddPrimaryKey("dbo.ApplicationUserItems", new[] { "Item_Id", "ApplicationUser_Id" });
            RenameTable(name: "dbo.ApplicationUserItems", newName: "ItemApplicationUsers");
        }
    }
}
