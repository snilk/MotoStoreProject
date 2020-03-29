namespace MotoStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeighnKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MotoImages", "Motorcycle_Id", "dbo.Motorcycles");
            DropForeignKey("dbo.Orders", "Motorcycle_Id", "dbo.Motorcycles");
            DropForeignKey("dbo.Orders", "ShopInformation_Id", "dbo.ShopInformations");
            DropIndex("dbo.MotoImages", new[] { "Motorcycle_Id" });
            DropIndex("dbo.Orders", new[] { "Motorcycle_Id" });
            DropIndex("dbo.Orders", new[] { "ShopInformation_Id" });
            DropColumn("dbo.MotoImages", "MotoId");
            DropColumn("dbo.Orders", "MotoId");
            DropColumn("dbo.Orders", "ShopId");
            RenameColumn(table: "dbo.MotoImages", name: "Motorcycle_Id", newName: "MotoId");
            RenameColumn(table: "dbo.Orders", name: "Motorcycle_Id", newName: "MotoId");
            RenameColumn(table: "dbo.Orders", name: "ShopInformation_Id", newName: "ShopId");
            AlterColumn("dbo.MotoImages", "MotoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "MotoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ShopId", c => c.Int(nullable: false));
            CreateIndex("dbo.MotoImages", "MotoId");
            CreateIndex("dbo.Orders", "MotoId");
            CreateIndex("dbo.Orders", "ShopId");
            AddForeignKey("dbo.MotoImages", "MotoId", "dbo.Motorcycles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "MotoId", "dbo.Motorcycles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ShopId", "dbo.ShopInformations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ShopId", "dbo.ShopInformations");
            DropForeignKey("dbo.Orders", "MotoId", "dbo.Motorcycles");
            DropForeignKey("dbo.MotoImages", "MotoId", "dbo.Motorcycles");
            DropIndex("dbo.Orders", new[] { "ShopId" });
            DropIndex("dbo.Orders", new[] { "MotoId" });
            DropIndex("dbo.MotoImages", new[] { "MotoId" });
            AlterColumn("dbo.Orders", "ShopId", c => c.Int());
            AlterColumn("dbo.Orders", "MotoId", c => c.Int());
            AlterColumn("dbo.MotoImages", "MotoId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ShopId", newName: "ShopInformation_Id");
            RenameColumn(table: "dbo.Orders", name: "MotoId", newName: "Motorcycle_Id");
            RenameColumn(table: "dbo.MotoImages", name: "MotoId", newName: "Motorcycle_Id");
            AddColumn("dbo.Orders", "ShopId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "MotoId", c => c.Int(nullable: false));
            AddColumn("dbo.MotoImages", "MotoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ShopInformation_Id");
            CreateIndex("dbo.Orders", "Motorcycle_Id");
            CreateIndex("dbo.MotoImages", "Motorcycle_Id");
            AddForeignKey("dbo.Orders", "ShopInformation_Id", "dbo.ShopInformations", "Id");
            AddForeignKey("dbo.Orders", "Motorcycle_Id", "dbo.Motorcycles", "Id");
            AddForeignKey("dbo.MotoImages", "Motorcycle_Id", "dbo.Motorcycles", "Id");
        }
    }
}
