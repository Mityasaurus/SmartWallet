using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SmartWallet.DAL.Entity;

namespace SmartWallet.DAL;

public class SmartWalletContext: DbContext
{
    private string _connectionString = "Server=tcp:yurabank.database.windows.net,1433;Initial Catalog=bank;Persist Security Info=False;User ID=bank;Password=sokolyuraPassword!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.UserCard)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.UserCardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}