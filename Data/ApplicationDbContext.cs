using Donation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Donation.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DonationRequest>()
                .HasOne(dr => dr.Beneficiary)
                .WithMany(u => u.DonationRequests)
                .HasForeignKey(dr => dr.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Donaation>()
               .HasOne(dr => dr.Donor)
               .WithMany(u => u.Donations)
               .HasForeignKey(dr => dr.DonorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StatusHistory>()
               .HasOne(dr => dr.ChangedByUser)
               .WithMany(u => u.StatusHistories)
               .HasForeignKey(dr => dr.ChangedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution>()
              .HasOne(dr => dr.Coordinator)
              .WithMany(u => u.Distributions)
              .HasForeignKey(dr => dr.CoordinatorId)
              .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<ApplicationUser> User {  get; set; }
        public DbSet<Donaation> Donations { get; set; }
        public DbSet<DonationRequest> DonationsRequests { get; set; }
        public DbSet<StatusHistory> StatusHistories { get; set; }
        public DbSet<Distribution> Distributions { get; set; }

    }
}
