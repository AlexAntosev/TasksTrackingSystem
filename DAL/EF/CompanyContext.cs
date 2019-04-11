using System;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.EF
{
    public class CompanyContext : IdentityDbContext<AuthenticationUser>, IContext, IDisposable
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
        
        public CompanyContext() : base("TaskTrackingSystemDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CompanyContext>());
            Database.SetInitializer(new DropCreateDatabaseAlways<CompanyContext>());
        }
        
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired();
            builder.Entity<Project>()
                .Property(p => p.Tag)
                .IsRequired();
            builder.Entity<Project>()
                .HasMany(p => p.Team)
                .WithMany(u => u.Projects)
                .Map(m => m.ToTable("ProjectsAndUsers").MapLeftKey("ProjectId").MapRightKey("UserId"));
            builder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithRequired(t => t.Project);

            builder.Entity<Task>()
                .Property(t => t.Name)
                .IsRequired();
            builder.Entity<Task>()
                .Property(t => t.Priority)
                .IsRequired();
            builder.Entity<Task>()
                .Property(t => t.Created)
                .IsRequired();
            builder.Entity<Task>()
                .Property(t => t.Updated)
                .IsRequired();
            builder.Entity<Task>()
                .HasMany(t => t.Comments)
                .WithRequired(c => c.Task);
            builder.Entity<Task>()
                .HasRequired(t => t.Creator)
                .WithMany(u => u.CreatedTasks);
            builder.Entity<Task>()
                .HasOptional(t => t.Executor)
                .WithMany(u => u.TasksInProgress);

            builder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();
            builder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();
            builder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();

            builder.Entity<Comment>()
                .Property(u => u.Description)
                .IsRequired();
            builder.Entity<Comment>()
                .Property(u => u.Time)
                .IsRequired();

            builder.Entity<AuthenticationUser>().ToTable("AuthenticationUsers");
            builder.Entity<IdentityUserRole>().ToTable("UserRoles");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            base.OnModelCreating(builder);
        }

        public static CompanyContext Create()
        {
            return new CompanyContext();
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

        public async System.Threading.Tasks.Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }
    }
}
