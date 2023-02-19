using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.Database;

public class RekomContext : DbContext
{
   private readonly IConfiguration _configuration;

   public DbSet<Account> Accounts { get; set; } = null!;
   
   public RekomContext(DbContextOptions<RekomContext> options, IConfiguration configuration) : base(options)
   {
      _configuration = configuration;
   }
   
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseMySQL(_configuration.GetValue<string>("MySQLConnectionString")!);
   }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<EntityBase>()
         .Property(e => e.CreatedAt)
         .HasDefaultValueSql("GETUTCDATE()");

      modelBuilder.Entity<EntityBase>()
         .Property(e => e.UpdatedAt)
         .HasDefaultValueSql("GETUTCDATE()")
         .ValueGeneratedOnUpdate();
      
      modelBuilder.Entity<Account>()
         .Property(a => a.Role)
         .HasConversion(
            v => v.ToString(),
            v => (Role)Enum.Parse(typeof(Role), v)
         );
   }
}