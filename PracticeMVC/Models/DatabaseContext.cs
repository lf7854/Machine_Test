using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticeMVC.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("defaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, PracticeMVC.Migrations.Configuration>());
        }

        public DbSet<CategoryMaster> CategoryMasters { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }
    }
}