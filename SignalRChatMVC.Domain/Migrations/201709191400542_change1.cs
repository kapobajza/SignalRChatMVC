namespace SignalRChatMVC.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "Waiting", c => c.Boolean());
            AddColumn("dbo.Friends", "Accepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "Accepted");
            DropColumn("dbo.Friends", "Waiting");
        }
    }
}
