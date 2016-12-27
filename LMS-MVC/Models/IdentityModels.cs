﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace LMS_MVC.Models
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
        public List<ClassUnit> ClassUnits { get; set; }

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

    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<ClassUnit> MyClassUnit { get; set; }
        public DbSet<Dossier> MyFiles { get; set; }
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

       
        //If this line Appers delete it
        //public System.Data.Entity.DbSet<LMS_MVC.Models.ApplicationUser> ApplicationUsers { get; set; }

    }
}