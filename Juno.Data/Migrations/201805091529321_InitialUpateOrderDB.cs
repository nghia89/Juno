namespace Juno.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpateOrderDB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "CustomerMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CustomerMessage", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
