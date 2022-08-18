namespace Restaurant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Sub_Categories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ParentCategoryId", c => c.Int());
            CreateIndex("dbo.Categories", "ParentCategoryId");
            AddForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            DropColumn("dbo.Categories", "ParentCategoryId");
        }
    }
}
