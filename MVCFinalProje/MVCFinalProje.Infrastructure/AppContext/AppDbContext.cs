using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MVCFinalProje.Core.BaseEntities;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Enums;
using MVCFinalProje.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.AppContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option): base(option) { }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }  

        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurations).Assembly);
            // Yukarıdaki kod blogu IEntityConfiguration interface inin bulundugu namespace deki tüm sınıflara condiguration uygulamasını sağlar.
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void SetBaseProperties()
        {
            var enrties = ChangeTracker.Entries<BaseEntity>(); // ChangeTracker veri tabanı işlemlerini kontrol eder.
            var userId = "UserBulunamadı";
            foreach (var ent in enrties) 
            {
                SetIfAdded(ent, userId);
                SetIfModified(ent, userId);
                SetIfDeleted(ent, userId);
            }
        }

        private void SetIfDeleted(EntityEntry<BaseEntity> ent, string userId)
        {
            if (ent.State != EntityState.Deleted)
            {
                return;
            }
            if (ent.Entity is not AuditableEntity entity)
            {
                return;
            }
            ent.State = EntityState.Modified;
            ent.Entity.Status = Status.Deleted;
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = userId;
        }

        private void SetIfModified(EntityEntry<BaseEntity> ent, string userId)
        {
            if (ent.State == EntityState.Modified)
            {
                ent.Entity.Status = Status.Added;
                ent.Entity.UpdatedBy = userId;
                ent.Entity.UpdatedDate = DateTime.Now;
            }
        }

        private void SetIfAdded(EntityEntry<BaseEntity> ent, string userId)
        {
            if (ent.State == EntityState.Added)
            {
                ent.Entity.Status = Status.Added;
                ent.Entity.CreatedBy = userId;
                ent.Entity.CreatedDate = DateTime.Now;
            }
        }
    }
}
