using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Entities;

namespace RekomBackend.Database;

public class RekomContext : DbContext
{
   public DbSet<Account> Accounts { get; set; } = null!;
   public DbSet<Otp> Otps { get; set; } = null!;
   public DbSet<Rekomer> Rekomers { get; set; } = null!;
   public DbSet<Follow> Follows { get; set; } = null!;
   public DbSet<Restaurant> Restaurants { get; set; } = null!;
   public DbSet<Food> Foods { get; set; } = null!;
   public DbSet<Rating> Ratings { get; set; } = null!;
   public DbSet<Review> Reviews { get; set; } = null!;
   public DbSet<ReviewMedia> ReviewMedias { get; set; } = null!;
   public DbSet<Reaction> Reactions { get; set; } = null!;
   public DbSet<ReviewReaction> ReviewReactions { get; set; } = null!;
   public DbSet<Comment> Comments { get; set; } = null!;
   public DbSet<FavouriteRestaurant> FavouriteRestaurants { get; set; } = null!;

   public DbSet<RatingResultView> RatingResultViews { get; set; } = null!;
   
   public RekomContext(DbContextOptions<RekomContext> options) : base(options)
   {
      
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasPostgresEnum<Role>();
      modelBuilder.HasPostgresExtension("postgis");

      modelBuilder.Entity<Restaurant>()
         .HasGeneratedTsVectorColumn(
            r => r.FullTextSearch,
            "english",
            r => new { r.Name, r.Description, r.Address })
         .HasIndex(r => r.FullTextSearch)
         .HasMethod("GIN");
         // .HasOperators("gin");
      
      modelBuilder.Entity<Food>()
         .HasGeneratedTsVectorColumn(
            fod => fod.FullTextSearch,
            "english",
            fod => new { fod.Name, fod.Description })
         .HasIndex(r => r.FullTextSearch)
         .HasMethod("GIN");
         // .HasOperators("gin");
      
         modelBuilder.Entity<Rekomer>()
         .HasGeneratedTsVectorColumn(
            fod => fod.FullTextSearch,
            "english",
            fod => new { fod.FullName, fod.Description })
         .HasIndex(r => r.FullTextSearch)
         .HasMethod("GIN");
         // .HasOperators("gin");
      
      modelBuilder.Entity<Restaurant>()
         .Property(r => r.Location)
         .HasColumnType("geography(Point, 4326)");

      modelBuilder.Entity<Follow>()
         .HasOne(f => f.Follower)
         .WithMany(u => u.Followings)
         .HasForeignKey(f => f.FollowerId)
         .OnDelete(DeleteBehavior.Cascade);
      
      modelBuilder.Entity<Follow>()
         .HasOne(f => f.Following)
         .WithMany(u => u.Followers)
         .HasForeignKey(f => f.FollowingId)
         .OnDelete(DeleteBehavior.Cascade);
      
      modelBuilder.Entity<RatingResultView>().ToView("RatingResultViews");
   }
}