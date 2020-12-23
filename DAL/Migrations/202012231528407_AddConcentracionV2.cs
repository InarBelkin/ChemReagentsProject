namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConcentracionV2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Сoncentration", newName: "Concentrations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Concentrations", newName: "Сoncentration");
        }
    }
}
