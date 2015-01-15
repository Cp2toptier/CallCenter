namespace CallCenter.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31102014 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Incidences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(nullable: false),
                        InternalNote = c.String(),
                        InternalNotePrueba = c.String(),
                        Equipment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.Equipment_Id)
                .Index(t => t.Equipment_Id);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        EquipmentType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentType_Id)
                .Index(t => t.EquipmentType_Id);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Incidence_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidences", t => t.Incidence_Id)
                .Index(t => t.Incidence_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "Incidence_Id" });
            DropIndex("dbo.Equipments", new[] { "EquipmentType_Id" });
            DropIndex("dbo.Incidences", new[] { "Equipment_Id" });
            DropForeignKey("dbo.Messages", "Incidence_Id", "dbo.Incidences");
            DropForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Incidences", "Equipment_Id", "dbo.Equipments");
            DropTable("dbo.Messages");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Incidences");
        }
    }
}
