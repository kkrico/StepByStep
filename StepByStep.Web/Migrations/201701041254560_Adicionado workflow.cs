namespace StepByStep.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adicionadoworkflow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PassoFeito", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PassoFeito");
        }
    }
}
