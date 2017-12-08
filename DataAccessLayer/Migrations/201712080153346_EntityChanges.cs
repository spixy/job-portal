namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobApplications", "Question_Id", "dbo.Questions");
            DropIndex("dbo.JobApplications", new[] { "Question_Id" });
            DropColumn("dbo.JobApplications", "Question_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobApplications", "Question_Id", c => c.Int());
            CreateIndex("dbo.JobApplications", "Question_Id");
            AddForeignKey("dbo.JobApplications", "Question_Id", "dbo.Questions", "Id");
        }
    }
}
