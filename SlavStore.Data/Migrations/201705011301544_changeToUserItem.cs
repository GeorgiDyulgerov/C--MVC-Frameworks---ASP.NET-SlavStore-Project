using System.Data.Entity.Migrations;

namespace SlavStore.Data.Migrations
{
    public partial class changeToUserItem : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.ApplicationUserItems", "Id", c => c.Int());
            DropPrimaryKey("dbo.ApplicationUserItems");
            //DropIndex("dbo.ApplicationUserItems","Item_Id");
            //AddPrimaryKey("dbo.ApplicationUserItems","Id");
            //CreateIndex("dbo.ApplicationUserItems","Id");
            //AddForeignKey("dbo.ApplicationUserItems", "Item_Id", "dbo.Items", "Id");
            //AddForeignKey("dbo.ApplicationUserItems", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
