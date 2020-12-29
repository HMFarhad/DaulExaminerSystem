namespace Result_Submission_for_Dual_Examinar.Migrations
{
    using Result_Submission_for_Dual_Examinar.Controllers;
    using Result_Submission_for_Dual_Examinar.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Result_Submission_for_Dual_Examinar.Models.DualExaminerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Result_Submission_for_Dual_Examinar.Models.DualExaminerDBContext";
            try
            {
                using (Result_Submission_for_Dual_Examinar.Models.DualExaminerDBContext context = new DualExaminerDBContext())
                {
                    if (context?.Users?.Count() == 0)
                    {
                        User user = new User()
                        {
                            FullName = "Admin User",
                            UserName = "Admin",
                            Password = HomeController.ComputeSHA512Hash("admin"),
                            Type = "admin"
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                        base.Seed(context);
                    }

                }
            }
            catch (Exception)
            {

                //throw;
            }
      

        }

        protected override void Seed(Result_Submission_for_Dual_Examinar.Models.DualExaminerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
