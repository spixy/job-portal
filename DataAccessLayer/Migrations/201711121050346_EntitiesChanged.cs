namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBases", "JobApplication_Id", "dbo.JobApplications");
            DropIndex("dbo.UserBases", new[] { "JobApplication_Id" });
            DropColumn("dbo.UserBases", "JobApplication_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBases", "JobApplication_Id", c => c.Int());
            CreateIndex("dbo.UserBases", "JobApplication_Id");
            AddForeignKey("dbo.UserBases", "JobApplication_Id", "dbo.JobApplications", "Id");
        }
    }
}
