namespace DataAccessLayer.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class Login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBases", "Username", c => c.String());
            AddColumn("dbo.UserBases", "Password", c => c.String(maxLength: 100));
            AddColumn("dbo.UserBases", "Username1", c => c.String());
            AddColumn("dbo.UserBases", "Password1", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserBases", "Password1");
            DropColumn("dbo.UserBases", "Username1");
            DropColumn("dbo.UserBases", "Password");
            DropColumn("dbo.UserBases", "Username");
        }
    }
}
