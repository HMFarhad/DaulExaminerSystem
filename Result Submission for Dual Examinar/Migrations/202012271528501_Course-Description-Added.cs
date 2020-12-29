namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseDescriptionAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Description");
        }
    }
}
