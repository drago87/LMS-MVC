using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using Queries.Core.Models;
using Queries.Core.Domain;
using System;

namespace Queries.Data.SampleData
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
      
    }
}