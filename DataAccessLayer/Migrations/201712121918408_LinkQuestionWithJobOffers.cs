namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkQuestionWithJobOffers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "JobOffer_Id", "dbo.JobOffers");
            DropIndex("dbo.Questions", new[] { "JobOffer_Id" });
            CreateTable(
                "dbo.QuestionJobOffers",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        JobOffer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.JobOffer_Id })
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .ForeignKey("dbo.JobOffers", t => t.JobOffer_Id, cascadeDelete: true)
                .Index(t => t.Question_Id)
                .Index(t => t.JobOffer_Id);
            
            DropColumn("dbo.Questions", "JobOffer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "JobOffer_Id", c => c.Int());
            DropForeignKey("dbo.QuestionJobOffers", "JobOffer_Id", "dbo.JobOffers");
            DropForeignKey("dbo.QuestionJobOffers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.QuestionJobOffers", new[] { "JobOffer_Id" });
            DropIndex("dbo.QuestionJobOffers", new[] { "Question_Id" });
            DropTable("dbo.QuestionJobOffers");
            CreateIndex("dbo.Questions", "JobOffer_Id");
            AddForeignKey("dbo.Questions", "JobOffer_Id", "dbo.JobOffers", "Id");
        }
    }
}
