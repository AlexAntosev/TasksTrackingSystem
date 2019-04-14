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
        public DbSet<Task> Tasks { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Invite> Invites { get; set; }

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
            builder.Entity<Project>()
                .HasMany(p => p.Invites)
                .WithOptional(i => i.Project);

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
            builder.Entity<User>()
                .HasMany(u => u.Invites)
                .WithOptional(i => i.Receiver);
            builder.Entity<User>()
                .HasMany(u => u.CreatedInvites)
                .WithOptional(i => i.Author);

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
