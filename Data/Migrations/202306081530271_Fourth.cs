namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Corporations", "CorporationName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Corporations", "CorporationName", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
