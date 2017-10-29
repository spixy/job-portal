namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        JobApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.JobApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.JobApplicationId);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobOfferId = c.Int(nullable: false),
                        JobCandidateId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        RegisteredUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserBases", t => t.RegisteredUser_Id)
                .ForeignKey("dbo.UserBases", t => t.JobCandidateId, cascadeDelete: true)
                .ForeignKey("dbo.JobOffers", t => t.JobOfferId, cascadeDelete: true)
                .Index(t => t.JobOfferId)
                .Index(t => t.JobCandidateId)
                .Index(t => t.RegisteredUser_Id);
            
            CreateTable(
                "dbo.UserBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        Education = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        JobApplication_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.JobApplication_Id)
                .Index(t => t.JobApplication_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EmployerId = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                // foreign key constraint exception => disable cascade delete
                .ForeignKey("dbo.UserBases", t => t.EmployerId, cascadeDelete: false)
                .ForeignKey("dbo.Offices", t => t.OfficeId, cascadeDelete: true)
                .Index(t => t.EmployerId)
                .Index(t => t.OfficeId);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.Int(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        JobOffer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobOffers", t => t.JobOffer_Id)
                .Index(t => t.JobOffer_Id);
            
            CreateTable(
                "dbo.SkillJobCandidates",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        JobCandidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.JobCandidate_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserBases", t => t.JobCandidate_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.JobCandidate_Id);
            
            CreateTable(
                "dbo.JobOfferSkills",
                c => new
                    {
                        JobOffer_Id = c.Int(nullable: false),
                        Skill_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobOffer_Id, t.Skill_Id })
                .ForeignKey("dbo.JobOffers", t => t.JobOffer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .Index(t => t.JobOffer_Id)
                .Index(t => t.Skill_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "JobApplicationId", "dbo.JobApplications");
            DropForeignKey("dbo.JobApplications", "JobOfferId", "dbo.JobOffers");
            DropForeignKey("dbo.JobApplications", "JobCandidateId", "dbo.UserBases");
            DropForeignKey("dbo.UserBases", "JobApplication_Id", "dbo.JobApplications");
            DropForeignKey("dbo.JobApplications", "RegisteredUser_Id", "dbo.UserBases");
            DropForeignKey("dbo.JobOfferSkills", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.JobOfferSkills", "JobOffer_Id", "dbo.JobOffers");
            DropForeignKey("dbo.Questions", "JobOffer_Id", "dbo.JobOffers");
            DropForeignKey("dbo.JobOffers", "OfficeId", "dbo.Offices");
            DropForeignKey("dbo.JobOffers", "EmployerId", "dbo.UserBases");
            DropForeignKey("dbo.SkillJobCandidates", "JobCandidate_Id", "dbo.UserBases");
            DropForeignKey("dbo.SkillJobCandidates", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.JobOfferSkills", new[] { "Skill_Id" });
            DropIndex("dbo.JobOfferSkills", new[] { "JobOffer_Id" });
            DropIndex("dbo.SkillJobCandidates", new[] { "JobCandidate_Id" });
            DropIndex("dbo.SkillJobCandidates", new[] { "Skill_Id" });
            DropIndex("dbo.Questions", new[] { "JobOffer_Id" });
            DropIndex("dbo.JobOffers", new[] { "OfficeId" });
            DropIndex("dbo.JobOffers", new[] { "EmployerId" });
            DropIndex("dbo.UserBases", new[] { "JobApplication_Id" });
            DropIndex("dbo.JobApplications", new[] { "RegisteredUser_Id" });
            DropIndex("dbo.JobApplications", new[] { "JobCandidateId" });
            DropIndex("dbo.JobApplications", new[] { "JobOfferId" });
            DropIndex("dbo.Answers", new[] { "JobApplicationId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.JobOfferSkills");
            DropTable("dbo.SkillJobCandidates");
            DropTable("dbo.Questions");
            DropTable("dbo.Offices");
            DropTable("dbo.JobOffers");
            DropTable("dbo.Skills");
            DropTable("dbo.UserBases");
            DropTable("dbo.JobApplications");
            DropTable("dbo.Answers");
        }
    }
}
