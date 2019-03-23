using DAL.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DAL.Interfaces
{
    public interface IContext : IDisposable
    {
        DbSet<Task> Tasks { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void Save();
    }
}
