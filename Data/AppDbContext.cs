using Microsoft.EntityFrameworkCore;
using InternetTenderService.Models;

namespace InternetTenderService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Tender> Tenders { get; set; }
    public DbSet<Bid> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tender>()
            .HasOne(t => t.Owner)
            .WithMany(u => u.Tenders)
            .HasForeignKey(t => t.OwnerId);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Tender)
            .WithMany(t => t.Bids)
            .HasForeignKey(b => b.TenderId);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Bidder)
            .WithMany(u => u.Bids)
            .HasForeignKey(b => b.BidderId);
    }
}
