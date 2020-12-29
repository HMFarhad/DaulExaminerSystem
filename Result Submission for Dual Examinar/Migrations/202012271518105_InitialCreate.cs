namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        MidTeacherId = c.Long(nullable: false),
                        FinalTeacherId = c.Long(nullable: false),
                        Student_StudentId = c.Long(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MarkId = c.Long(nullable: false, identity: true),
                        CourseId = c.Long(nullable: false),
                        MidMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MarkId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Courses", new[] { "Student_StudentId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Marks");
            DropTable("dbo.Courses");
        }
    }
}
