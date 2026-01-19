using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext 
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestHistory> RequestHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User - Request (CreatedBy) ilişkisi
            modelBuilder.Entity<Request>()
                .HasRequired(r => r.CreatedByUser)
                .WithMany(u => u.CreatedRequests)
                .HasForeignKey(r => r.CreatedByUserId)
                .WillCascadeOnDelete(false);

            // User - Request (ProcessedBy) ilişkisi
            modelBuilder.Entity<Request>()
                .HasOptional(r => r.ProcessedByUser)
                .WithMany(u => u.ProcessedRequests)
                .HasForeignKey(r => r.ProcessedByUserId)
                .WillCascadeOnDelete(false);

            // Request - RequestHistory ilişkisi
            modelBuilder.Entity<RequestHistory>()
                .HasRequired(h => h.Request)
                .WithMany(r => r.RequestHistories)
                .HasForeignKey(h => h.RequestId)
                .WillCascadeOnDelete(false);

            // User - RequestHistory ilişkisi
            modelBuilder.Entity<RequestHistory>()
                .HasRequired(h => h.User)
                .WithMany(u => u.RequestHistories)
                .HasForeignKey(h => h.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
