using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Awesome.Model;
using Awesome.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System;

namespace Awesome.Data.SampleData
{
    public class AwesomeDatabaseInitializer : DropCreateDatabaseIfModelChanges<AwesomeDbContext>
    {
        protected override void Seed(AwesomeDbContext context)
        {
            # region Roles
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

            var userStore   = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user1 = new ApplicationUser { UserName = "testTeacher@test.com", Email = "testTeacher@test.com" };
            var user2 = new ApplicationUser { UserName = "testStudent@test.com", Email = "testStudent@test.com" };

            userManager.Create(user1, "Test123!");
            userManager.Create(user2, "Test123!");

            #endregion

            #region Klasser
            var grund1a = new ClassUnit { ClassName = "Grund1A" };
            var grund3a = new ClassUnit { ClassName = "Grund3A" };
            var grund5b = new ClassUnit { ClassName = "Grund5b" };

            context.Classunits.AddOrUpdate(
                c => c.ClassUnitID,
                grund1a, grund3a, grund5b
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
            #endregion

            #region Subjects
            var matte    = new Subject { SubjectName = "Matte" };
            var engelska = new Subject { SubjectName = "Engelska" };
            var historia = new Subject { SubjectName = "Historia" };
            var biologi  = new Subject { SubjectName = "Biologi" };
            context.Subjects.AddOrUpdate(
                s => s.SubjectName,
                historia, biologi, matte, engelska );
            #endregion

            #region Lessons
            var nu = DateTime.Now;
            context.Lessons.AddOrUpdate(
                l => l.StartTime,
                new Lesson { Subject = matte, ClassUnit = grund1a, StartTime = nu.AddHours(1), StopTime = nu.AddHours(2) },
                new Lesson { Subject = engelska, ClassUnit = grund1a, StartTime = nu.AddHours(2), StopTime = nu.AddHours(3) },
                new Lesson { Subject = biologi, ClassUnit = grund1a, StartTime = nu.AddHours(3), StopTime = nu.AddHours(4) },
                new Lesson { Subject = historia, ClassUnit = grund1a, StartTime = nu.AddHours(4), StopTime = nu.AddHours(5) },
                new Lesson { Subject = matte, ClassUnit = grund3a, StartTime = nu.AddHours(1), StopTime = nu.AddHours(2) },
                new Lesson { Subject = matte, ClassUnit = grund5b, StartTime = nu.AddHours(1), StopTime = nu.AddHours(2) }
            );
            context.SaveChanges();
            #endregion
        }
    }
}
