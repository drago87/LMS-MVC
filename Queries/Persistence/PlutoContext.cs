//using Microsoft.AspNet.Identity.EntityFramework;
//using Queries.Core.Domain;
//using Queries.Core.Models;
////using Queries.Persistence.EntityConfigurations;
//using System.Data.Entity;

//namespace Queries.Persistence
//{
//    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    //    public ApplicationDbContext()
//    //        : base("DefaultConnection", throwIfV1Schema: false)

//    //public class PlutoContext : DbContext
//    public class PlutoContext : IdentityDbContext<ApplicationUser>
//    {
//        public PlutoContext() : base("name=PlutoContext")
//        {
//            this.Configuration.LazyLoadingEnabled = false;
//        }

//        public virtual DbSet<Author>  Authors { get; set; }
//        public virtual DbSet<Subject> Subjects { get; set; }
//        //public virtual DbSet<ClassUnit> Classunits { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            //modelBuilder.Configurations.Add(new CourseConfiguration());
//        }
//    }
//}
