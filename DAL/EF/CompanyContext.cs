using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DAL.EF
{
    /// <summary>
    /// Database context
    /// </summary>
    public class CompanyContext : DbContext
    {
        /// <summary>
        /// Get and set tasks entities
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Get and set project entities
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// Get and set user entities
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Get and set comment entities
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Get and set user profile entities
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }

        public static CompanyContext Context()
        {
            return new CompanyContext();
        }

        public CompanyContext() : base("CompanyDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CompanyContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CompanyContext>());
        }
    }
}
