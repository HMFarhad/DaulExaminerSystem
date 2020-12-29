namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarkMidandFinal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marks", "MidCTMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Marks", "MidExamMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Marks", "FinalCTMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Marks", "FinalExamMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Marks", "MidMark");
            DropColumn("dbo.Marks", "FinalMark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marks", "FinalMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Marks", "MidMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Marks", "FinalExamMark");
            DropColumn("dbo.Marks", "FinalCTMark");
            DropColumn("dbo.Marks", "MidExamMark");
            DropColumn("dbo.Marks", "MidCTMark");
        }
    }
}
