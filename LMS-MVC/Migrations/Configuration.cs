namespace LMS_MVC.Migrations
{
    using LMS_MVC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_MVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LMS_MVC.Models.ApplicationDbContext context)
        {
            var a = context.Roles;

            if (!context.Roles.Any(r => r.Name == "Teacher"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var role = new IdentityRole { Name = "Teacher" };

                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Student"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var role = new IdentityRole { Name = "Student" };

                roleManager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);

            var userManager = new UserManager<ApplicationUser>(userStore);

            var user1 = new ApplicationUser { UserName = "testTeacher@test.com", Email = "testTeacher@test.com" };
            var user2 = new ApplicationUser { UserName = "testStudent@test.com", Email = "testStudent@test.com" };

            userManager.Create(user1, "Test123!");
            userManager.Create(user2, "Test123!");

            // ClassUnit Seed

            context.MyClassUnit.AddOrUpdate(
            c => c.ClassUnitID,
            new ClassUnit { ClassName = "Grund1A" },
            new ClassUnit { ClassName = "Grund3A" },
            new ClassUnit { ClassName = "Grund5b" }
            );

            ApplicationUser user1a = context.Users.FirstOrDefault(u => u.Email == "testTeacher@test.com");

            if (user1a != null)
            {
                userManager.AddToRole(user1.Id, "Teacher");
            }
            ApplicationUser user2a = context.Users.FirstOrDefault(u => u.Email == "testStudent@test.com");
            if (user2a != null)
            {
                userManager.AddToRole(user2.Id, "Student");
            }
            context.SaveChanges();
        }
    }
}
