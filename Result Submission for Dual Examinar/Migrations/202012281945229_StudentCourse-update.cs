namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourseupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "StudentCourse_StudentCourseId", c => c.Long());
            CreateIndex("dbo.Courses", "StudentCourse_StudentCourseId");
            AddForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses", "StudentCourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses");
            DropIndex("dbo.Courses", new[] { "StudentCourse_StudentCourseId" });
            DropColumn("dbo.Courses", "StudentCourse_StudentCourseId");
        }
    }
}
