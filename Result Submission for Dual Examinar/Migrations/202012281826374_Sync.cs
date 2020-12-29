namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sync : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "User_Id", c => c.Long());
            AddColumn("dbo.Marks", "StudentId", c => c.Long(nullable: false));
            CreateIndex("dbo.Courses", "User_Id");
            CreateIndex("dbo.Marks", "CourseId");
            AddForeignKey("dbo.Marks", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Marks", "CourseId", "dbo.Courses");
            DropIndex("dbo.Marks", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "User_Id" });
            DropColumn("dbo.Marks", "StudentId");
            DropColumn("dbo.Courses", "User_Id");
        }
    }
}
