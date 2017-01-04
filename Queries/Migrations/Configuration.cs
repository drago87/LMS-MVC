namespace Queries.Migrations
{
    using Queries.Core.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Queries.Core.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Queries.Core.Models.ApplicationDbContext context)
        {
            //Queries.Data.SampleData.Seed(context);

            #region Subjects
            var matte    = new Subject { SubjectName = "Matte" };
            var engelska = new Subject { SubjectName = "Engelska" };
            var historia = new Subject { SubjectName = "Historia" };
            var biologi  = new Subject { SubjectName = "Biologi" };
            context.Subjects.AddOrUpdate(
                s => s.SubjectName,
                historia, biologi, matte, engelska);
            #endregion
        }
    }
}
