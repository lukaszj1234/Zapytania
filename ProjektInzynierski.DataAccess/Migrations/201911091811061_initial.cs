namespace ProjektInzynierski.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddedFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        InquiryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .Index(t => t.InquiryId);
            
            CreateTable(
                "dbo.Inquiries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Industry = c.String(),
                        CollumnNumber = c.Int(),
                        RowNumber = c.Int(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        InquiryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .Index(t => t.InquiryId);
            
            CreateTable(
                "dbo.ReferenceOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        InquiryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .Index(t => t.InquiryId);
            
            CreateTable(
                "dbo.SendedInquiries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        InquiryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .Index(t => t.InquiryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SendedInquiries", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.ReferenceOffers", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.Offers", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.AddedFiles", "InquiryId", "dbo.Inquiries");
            DropIndex("dbo.SendedInquiries", new[] { "InquiryId" });
            DropIndex("dbo.ReferenceOffers", new[] { "InquiryId" });
            DropIndex("dbo.Offers", new[] { "InquiryId" });
            DropIndex("dbo.AddedFiles", new[] { "InquiryId" });
            DropTable("dbo.SendedInquiries");
            DropTable("dbo.ReferenceOffers");
            DropTable("dbo.Offers");
            DropTable("dbo.Inquiries");
            DropTable("dbo.AddedFiles");
        }
    }
}
