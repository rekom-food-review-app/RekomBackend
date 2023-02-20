using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.Database;

public class RekomContext : DbContext
{
   private readonly IConfiguration _configuration;

   public DbSet<Account> Accounts { get; set; } = null!;
   public DbSet<Otp> Otps { get; set; } = null!;
   
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
      // modelBuilder.Entity<EntityBase>()
      //    .Property(e => e.CreatedAt)
      //    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
      //
      // modelBuilder.Entity<EntityBase>()
      //    .Property(e => e.UpdatedAt)
      //    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
      //    .ValueGeneratedOnUpdate();
      
      modelBuilder.Entity<Account>()
         .Property(a => a.Role)
         .HasConversion(
            v => v.ToString(),
            v => (Role)Enum.Parse(typeof(Role), v)
         );
   }
   
   public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
   {
      AddTimestamps();
      return base.SaveChangesAsync(cancellationToken);
   }

   private void AddTimestamps()
   {
      var entities = ChangeTracker.Entries()
         .Where(x => x is { Entity: EntityBase, State: EntityState.Added or EntityState.Modified });

      foreach (var entity in entities)
      {
         var now = DateTime.UtcNow;

         if (entity.State == EntityState.Added)
         {
            ((EntityBase)entity.Entity).CreatedAt = now;
         }
         ((EntityBase)entity.Entity).UpdatedAt = now;
      }
   }
}