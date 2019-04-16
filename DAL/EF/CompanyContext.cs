using System;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.EF
{
    public class CompanyContext : IdentityDbContext<AuthenticationUser>, IContext, IDisposable
    {
        public DbSet<Task> Tasks { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Invite> Invites { get; set; }

        public DbSet<UserWithRole> UsersWithRoles { get; set; }

        public CompanyContext() : base("TaskTrackingSystemDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CompanyContext>());
            Database.SetInitializer(new DropCreateDatabaseAlways<CompanyContext>());
        }
        
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new ProjectConfiguration());
            builder.Configurations.Add(new TaskConfiguration());
            builder.Configurations.Add(new UserConfiguration());
            builder.Configurations.Add(new CommentConfiguration());

            builder.Entity<AuthenticationUser>().ToTable("AuthenticationUsers");
            builder.Entity<IdentityUserRole>().ToTable("UserRoles");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            base.OnModelCreating(builder);
        }

        private class ProjectConfiguration : EntityTypeConfiguration<Project>
        {
            public ProjectConfiguration()
            {
                Property(p => p.Name).IsRequired();
                Property(p => p.Tag).IsRequired();
                HasMany(p => p.Team)
                    .WithMany(u => u.Projects)
                    .Map(m => m.ToTable("ProjectsAndUsersWithRoles").MapLeftKey("ProjectId").MapRightKey("UserWithRoleId"));
                HasMany(p => p.Tasks)
                    .WithRequired(t => t.Project);
                HasMany(p => p.Invites)
                    .WithOptional(i => i.Project);
            }
        }

        private class TaskConfiguration : EntityTypeConfiguration<Task>
        {
            public TaskConfiguration()
            {
                Property(t => t.Name)
                    .IsRequired();
                Property(t => t.Priority)
                    .IsRequired();
                Property(t => t.Created)
                    .IsRequired();
                Property(t => t.Updated)
                    .IsRequired();
                HasMany(t => t.Comments)
                    .WithRequired(c => c.Task);
                HasRequired(t => t.Creator)
                    .WithMany(u => u.CreatedTasks);
                HasOptional(t => t.Executor)
                    .WithMany(u => u.TasksInProgress);
            }
        }

        private class UserConfiguration : EntityTypeConfiguration<User>
        {
            public UserConfiguration()
            {
                Property(u => u.UserName)
                    .IsRequired();
                Property(u => u.FirstName)
                    .IsRequired();
                Property(u => u.LastName)
                    .IsRequired();
                HasMany(u => u.Invites)
                    .WithOptional(i => i.Receiver);
                HasMany(u => u.CreatedInvites)
                    .WithOptional(i => i.Author);
            }
        }

        private class CommentConfiguration : EntityTypeConfiguration<Comment>
        {
            public CommentConfiguration()
            {
                Property(u => u.Description)
                    .IsRequired();
                Property(u => u.Time)
                    .IsRequired();
            }
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
