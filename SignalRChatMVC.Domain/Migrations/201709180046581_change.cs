namespace SignalRChatMVC.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationReplies",
                c => new
                    {
                        ConversationReplyId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        Sent = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 50),
                        ConversationId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ConversationReplyId)
                .ForeignKey("dbo.Conversations", t => t.ConversationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 50),
                        ToUserFK = c.String(maxLength: 50),
                        FromUserFK = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ConversationId)
                .ForeignKey("dbo.Users", t => t.FromUserFK)
                .ForeignKey("dbo.Users", t => t.ToUserFK)
                .Index(t => t.ToUserFK)
                .Index(t => t.FromUserFK);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.FriendId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.FriendId)
                .Index(t => t.FriendId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "FriendId", "dbo.Users");
            DropForeignKey("dbo.Friends", "UserId", "dbo.Users");
            DropForeignKey("dbo.ConversationReplies", "UserId", "dbo.Users");
            DropForeignKey("dbo.ConversationReplies", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "ToUserFK", "dbo.Users");
            DropForeignKey("dbo.Conversations", "FromUserFK", "dbo.Users");
            DropIndex("dbo.Friends", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "FriendId" });
            DropIndex("dbo.Conversations", new[] { "FromUserFK" });
            DropIndex("dbo.Conversations", new[] { "ToUserFK" });
            DropIndex("dbo.ConversationReplies", new[] { "ConversationId" });
            DropIndex("dbo.ConversationReplies", new[] { "UserId" });
            DropTable("dbo.Friends");
            DropTable("dbo.Users");
            DropTable("dbo.Conversations");
            DropTable("dbo.ConversationReplies");
        }
    }
}
