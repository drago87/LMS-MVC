using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Awesome.Model;
using Awesome.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace Awesome.Data.SampleData
{
    public class AwesomeDatabaseInitializer : DropCreateDatabaseIfModelChanges<AwesomeDbContext>
    {
        protected override void Seed(AwesomeDbContext context)
        {
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

            context.Classunits.AddOrUpdate(
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
