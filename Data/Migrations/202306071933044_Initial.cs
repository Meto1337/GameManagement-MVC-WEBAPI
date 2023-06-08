namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Corporations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorporationName = c.String(nullable: false, maxLength: 20),
                        CorporationOwnerName = c.String(maxLength: 20),
                        Address = c.String(nullable: false),
                        AddressNumber = c.Int(nullable: false),
                        City = c.String(maxLength: 20),
                        ContactNumber = c.String(maxLength: 10),
                        Email = c.String(maxLength: 60),
                        EstablishedYear = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        Age = c.Int(nullable: false),
                        Specialization = c.String(maxLength: 20),
                        PhoneNumber = c.String(maxLength: 10),
                        YearsOfExperiance = c.Int(),
                        MonthSalary = c.Double(),
                        Corporation_Id = c.Int(nullable: false),
                        Corporation_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Corporations", t => t.Corporation_Id1)
                .Index(t => t.Corporation_Id1);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 28),
                        Description = c.String(maxLength: 300),
                        PublishDate = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        Creator_Id = c.Int(nullable: false),
                        Corporation_Id = c.Int(nullable: false),
                        Corporation_Id1 = c.Int(),
                        Creator_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Corporations", t => t.Corporation_Id1)
                .ForeignKey("dbo.Developers", t => t.Creator_Id1)
                .Index(t => t.Corporation_Id1)
                .Index(t => t.Creator_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Creator_Id1", "dbo.Developers");
            DropForeignKey("dbo.Games", "Corporation_Id1", "dbo.Corporations");
            DropForeignKey("dbo.Developers", "Corporation_Id1", "dbo.Corporations");
            DropIndex("dbo.Games", new[] { "Creator_Id1" });
            DropIndex("dbo.Games", new[] { "Corporation_Id1" });
            DropIndex("dbo.Developers", new[] { "Corporation_Id1" });
            DropTable("dbo.Games");
            DropTable("dbo.Developers");
            DropTable("dbo.Corporations");
        }
    }
}
