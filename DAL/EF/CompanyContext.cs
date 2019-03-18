using System;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DAL.EF
{
    /// <summary>
    /// Database context
    /// </summary>
    public class CompanyContext : DbContext, IContext, IDisposable
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

        public CompanyContext() : base("CompanyDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CompanyContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CompanyContext>());
            //Configuration.LazyLoadingEnabled = false;
        }

        public void Save()
        {
            SaveChanges();
        }

        DbEntityEntry IContext.Entry(object entity)
        {
            return Entry(entity);
        }

        DbEntityEntry<TEntity> IContext.Entry<TEntity>(TEntity entity)
        {
            return Entry<TEntity>(entity);
        }
    }
}
