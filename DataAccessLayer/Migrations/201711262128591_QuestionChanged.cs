namespace DataAccessLayer.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class QuestionChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplications", "Question_Id", c => c.Int());
            CreateIndex("dbo.JobApplications", "Question_Id");
            AddForeignKey("dbo.JobApplications", "Question_Id", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplications", "Question_Id", "dbo.Questions");
            DropIndex("dbo.JobApplications", new[] { "Question_Id" });
            DropColumn("dbo.JobApplications", "Question_Id");
        }
    }
}
