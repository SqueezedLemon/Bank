using Bank.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistance
{
    /// <summary>
    /// Class to perform ORM related tasks.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.User)
                .WithOne()
                .HasForeignKey<UserDetail>(ud => ud.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Balance>()
                .HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<Balance>(b => b.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionHistory>()
                .HasOne(th => th.User)
                .WithMany()
                .HasForeignKey(th => th.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithOne()
                .HasForeignKey<RefreshToken>(rt => rt.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
