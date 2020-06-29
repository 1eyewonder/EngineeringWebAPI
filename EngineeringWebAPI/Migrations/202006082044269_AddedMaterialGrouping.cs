namespace EngineeringWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaterialGrouping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ASMEMaterials", "MaterialGrouping", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ASMEMaterials", "MaterialGrouping");
        }
    }
}
