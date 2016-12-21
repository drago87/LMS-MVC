using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Collections.Generic;

namespace LMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            //Orginal
            //var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            //Modified
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Classunit", this.Classunit.ToString()));

            return userIdentity;
        }

        //Extended Properties
        public List<ClassUnit> Classunit { get; set; }
    }

    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<ClassUnit> MyClassUnit { get; set; }
        public DbSet<Files> MyFiles { get; set; }
        public DbSet<Subject> MySubjects { get; set; }
        public DbSet<Lesson> MyLessons { get; set; }
        public DbSet<Folder> MyFolders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClassUnit>().
                HasRequired(f => f.Shared)
                .WithRequiredPrincipal()
                    .WillCascadeOnDelete(false);


            modelBuilder.Entity<ClassUnit>().
                HasRequired(f => f.Submission)
                .WithRequiredPrincipal()
                    .WillCascadeOnDelete(false);

        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}