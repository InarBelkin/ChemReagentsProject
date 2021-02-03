namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Solution_recipe_line", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Supplies", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Supply_consumption", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Solution_line", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Solution_line", "Count", c => c.Single(nullable: false));
            AlterColumn("dbo.Supply_consumption", "Count", c => c.Single(nullable: false));
            AlterColumn("dbo.Supplies", "Count", c => c.Single(nullable: false));
            AlterColumn("dbo.Solution_recipe_line", "Count", c => c.Single(nullable: false));
        }
    }
}
