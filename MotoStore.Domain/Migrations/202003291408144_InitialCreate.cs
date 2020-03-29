namespace MotoStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MotoImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MotoId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Motorcycle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Motorcycles", t => t.Motorcycle_Id)
                .Index(t => t.Motorcycle_Id);
            
            CreateTable(
                "dbo.Motorcycles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Type = c.String(),
                        Year = c.Int(nullable: false),
                        EngineCapacity = c.Double(nullable: false),
                        Cylinders = c.Int(nullable: false),
                        Abs = c.Boolean(nullable: false),
                        ElectricStarter = c.Boolean(nullable: false),
                        CruizeControl = c.Boolean(nullable: false),
                        Description = c.String(),
                        ModelsCount = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        MainImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MotoId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ShopId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Address = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        Motorcycle_Id = c.Int(),
                        ShopInformation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Motorcycles", t => t.Motorcycle_Id)
                .ForeignKey("dbo.ShopInformations", t => t.ShopInformation_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Motorcycle_Id)
                .Index(t => t.ShopInformation_Id);
            
            CreateTable(
                "dbo.ShopInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "ShopInformation_Id", "dbo.ShopInformations");
            DropForeignKey("dbo.Orders", "Motorcycle_Id", "dbo.Motorcycles");
            DropForeignKey("dbo.MotoImages", "Motorcycle_Id", "dbo.Motorcycles");
            DropIndex("dbo.Orders", new[] { "ShopInformation_Id" });
            DropIndex("dbo.Orders", new[] { "Motorcycle_Id" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.MotoImages", new[] { "Motorcycle_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.ShopInformations");
            DropTable("dbo.Orders");
            DropTable("dbo.Motorcycles");
            DropTable("dbo.MotoImages");
        }
    }
}
