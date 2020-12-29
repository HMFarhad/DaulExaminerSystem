namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Courses", new[] { "Student_StudentId" });
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Courses", "Student_StudentId");
            DropTable("dbo.Students");
            DropTable("dbo.Teachers");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            AddColumn("dbo.Courses", "Student_StudentId", c => c.Long());
            DropTable("dbo.Users");
            CreateIndex("dbo.Courses", "Student_StudentId");
            AddForeignKey("dbo.Courses", "Student_StudentId", "dbo.Students", "StudentId");
        }
    }
}
