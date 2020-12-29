namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourseAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        StudentCourseId = c.Long(nullable: false, identity: true),
                        StudentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.StudentCourseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentCourses");
        }
    }
}
