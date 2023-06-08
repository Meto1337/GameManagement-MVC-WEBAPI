namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tgird : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Category", c => c.String(maxLength: 28));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Category");
        }
    }
}
