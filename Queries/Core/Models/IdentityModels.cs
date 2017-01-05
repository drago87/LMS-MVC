﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core.Domain;
using Queries.Data.SampleData;

//namespace bat_mvc.Models
namespace Queries.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //invoke seed (comment in production)
        static ApplicationDbContext()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        // DBSets
        public virtual DbSet<Subject>   Subjects   { get; set; }
        public virtual DbSet<Lesson>    Lessons    { get; set; }
        public virtual DbSet<ClassUnit> Classunits { get; set; }
        public virtual DbSet<Folder>    Folders    { get; set; }
        public virtual DbSet<Dossier>   Dossiers   { get; set; }

        public static ApplicationDbContext Create()
        {
            Database.SetInitializer(new DatabaseInitializer());
            return new ApplicationDbContext();
        }
    }
}