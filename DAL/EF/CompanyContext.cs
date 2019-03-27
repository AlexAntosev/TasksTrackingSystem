﻿using System;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.EF
{
    /// <summary>
    /// Database context
    /// </summary>
    public class CompanyContext : IdentityDbContext<ApplicationUser>, IContext, IDisposable
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
        }

        DbEntityEntry IContext.Entry(object entity)
        {
            return Entry(entity);
        }

        DbEntityEntry<TEntity> IContext.Entry<TEntity>(TEntity entity)
        {
            return Entry<TEntity>(entity);
        }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            builder.Entity<IdentityUserRole>().ToTable("UserRoles");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim>().ToTable("UserClaims");
        }

        public static CompanyContext Create()
        {
            return new CompanyContext();
        }
    }
}
