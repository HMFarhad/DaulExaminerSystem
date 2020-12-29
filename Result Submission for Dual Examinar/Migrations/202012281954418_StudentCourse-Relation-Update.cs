namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourseRelationUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses");
            DropIndex("dbo.Courses", new[] { "StudentCourse_StudentCourseId" });
            AddColumn("dbo.StudentCourses", "CourseId", c => c.Long(nullable: false));
            CreateIndex("dbo.StudentCourses", "StudentId");
            CreateIndex("dbo.StudentCourses", "CourseId");
            AddForeignKey("dbo.StudentCourses", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            AddForeignKey("dbo.StudentCourses", "StudentId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Courses", "StudentCourse_StudentCourseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "StudentCourse_StudentCourseId", c => c.Long());
            DropForeignKey("dbo.StudentCourses", "StudentId", "dbo.Users");
            DropForeignKey("dbo.StudentCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentCourses", new[] { "StudentId" });
            DropColumn("dbo.StudentCourses", "CourseId");
            CreateIndex("dbo.Courses", "StudentCourse_StudentCourseId");
            AddForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses", "StudentCourseId");
        }
    }
}
