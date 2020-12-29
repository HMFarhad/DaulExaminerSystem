namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarkRelation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Marks", "StudentId");
            AddForeignKey("dbo.Marks", "StudentId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "StudentId", "dbo.Users");
            DropIndex("dbo.Marks", new[] { "StudentId" });
        }
    }
}
