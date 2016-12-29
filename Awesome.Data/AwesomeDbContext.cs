using Awesome.Data.SampleData;
using Awesome.Model;
using System;
using System.Security.Claims;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Awesome.Data
{

     // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ClassUnits = new List<ClassUnit>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //Orginal
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("Classunit", this.Classunit.ToString()));
            return userIdentity;
        }
        //Extended Properties
        public virtual List<ClassUnit> ClassUnits { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //Orginal
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("Classunit", this.Classunit.ToString()));

            return userIdentity;
        }
        //Extended Properties
        //public List<ClassUnit> Classunit { get; set; } //anlu remmat
    }


    //public class AwesomeDbContext : DbContext
    public class AwesomeDbContext : IdentityDbContext<ApplicationUser>
    {
        public AwesomeDbContext() : base(nameOrConnectionString: "DefaultConnection") { }
        public DbSet<ClassUnit> Classunits { get; set; }
        public DbSet<Dossier>   Files      { get; set; }
        public DbSet<Subject>   Subjects   { get; set; }
        public DbSet<Lesson>    Lessons    { get; set; }
        public DbSet<Folder>    Folders    { get; set; }

        //invoke seed (comment in production)
        static AwesomeDbContext()
        {
            Database.SetInitializer(new AwesomeDatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ClassUnit>().
                HasRequired(f => f.Shared)
                .WithRequiredPrincipal()
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassUnit>().
                HasRequired(f => f.Submission)
                .WithRequiredPrincipal()
                    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
