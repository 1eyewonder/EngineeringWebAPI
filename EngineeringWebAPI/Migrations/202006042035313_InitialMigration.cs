namespace EngineeringWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ASMEMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Material = c.String(nullable: false),
                        Temperature = c.Single(nullable: false),
                        Stress = c.Single(nullable: false),
                        ASMEYear = c.String(nullable: false),
                        FlangeMaterialClass = c.Int(nullable: false),
                        MaterialClassification = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PipeSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NPS = c.String(nullable: false),
                        OD = c.Single(nullable: false),
                        Schedule = c.String(nullable: false, maxLength: 3),
                        WallThickness = c.Single(nullable: false),
                        API661Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PipeSchedules");
            DropTable("dbo.ASMEMaterials");
        }
    }
}
