namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestHistories",
                c => new
                    {
                        HistoryId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Action = c.String(nullable: false, maxLength: 100),
                        FieldName = c.String(maxLength: 100),
                        OldValue = c.String(maxLength: 100),
                        NewValue = c.String(maxLength: 100),
                        Notes = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryId)
                .ForeignKey("dbo.Requests", t => t.RequestId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.RequestId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestNo = c.String(maxLength: 20),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false),
                        RequestTypeId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        ProcessedByUserId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        ApprovalNotes = c.String(),
                        ProcessedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ProcessedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ProcessedByUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false, maxLength: 255),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        RoleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.RequestHistories", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.Requests", "ProcessedByUserId", "dbo.Users");
            DropForeignKey("dbo.Requests", "CreatedByUserId", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "ProcessedByUserId" });
            DropIndex("dbo.Requests", new[] { "CreatedByUserId" });
            DropIndex("dbo.RequestHistories", new[] { "UserId" });
            DropIndex("dbo.RequestHistories", new[] { "RequestId" });
            DropTable("dbo.Users");
            DropTable("dbo.Requests");
            DropTable("dbo.RequestHistories");
        }
    }
}
