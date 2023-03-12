using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Entities;
using RekomBackend.App.Helpers;

namespace RekomBackend.Database;

public class RekomContext : DbContext
{
   private readonly IConfiguration _configuration;
   
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
   
   public RekomContext(DbContextOptions<RekomContext> options, IConfiguration configuration) : base(options)
   {
      _configuration = configuration;
   }

   public RekomContext(IConfiguration configuration)
   {
      _configuration = configuration;
   }

   // public override int SaveChanges()
   // {
   //    foreach (var entry in ChangeTracker.Entries())
   //    {
   //       if (entry.Entity is Rekomer rekomer)
   //       {
   //          if (entry.State is EntityState.Added or EntityState.Modified)
   //          {
   //             // rekomer.FullTextSearch = NpgsqlTsVector.Parse($"{StringHelper.ToEnglish(rekomer.FullName)} {rekomer.FullName} {StringHelper.ToEnglish(rekomer.Description)} {rekomer.Description}");
   //             rekomer.FullTextSearch = Rekomers.Select(rek => EF.Functions.ToTsVector(($"{StringHelper.ToEnglish(rekomer.FullName)} {rekomer.FullName} {StringHelper.ToEnglish(rekomer.Description)} {rekomer.Description}"))).Single();
   //          }
   //       }
   //    }
   //    return base.SaveChanges();
   // }
   //
   // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
   // {
   //    foreach (var entry in ChangeTracker.Entries())
   //    {
   //       if (entry.Entity is Rekomer rekomer)
   //       {
   //          if (entry.State is EntityState.Added or EntityState.Modified)
   //          {
   //             rekomer.FullTextSearch = Rekomers.Select(rek => EF.Functions.ToTsVector(($"{StringHelper.ToEnglish(rekomer.FullName)} {rekomer.FullName} {StringHelper.ToEnglish(rekomer.Description)} {rekomer.Description}"))).Single();
   //          }
   //       }
   //    }
   //    return base.SaveChangesAsync(cancellationToken);
   // }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseNpgsql(
         _configuration.GetValue<string>("PostgresConnectionString")!,
         o => o.UseNetTopologySuite());
   }

   private string ToEnglish(string input)
   {
      // return new string(input.Normalize(NormalizationForm.FormD)
      //    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
      //    .ToArray());
      
      // input = input.Normalize(NormalizationForm.FormKD);
      //
      // // Replace non-letter characters with empty string
      // return Regex.Replace(input, @"[^a-zA-Z]+", "", RegexOptions.Compiled);

      return input;
   }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      // var regex = new Regex(@"[\p{IsLatin}]+", RegexOptions.Compiled);
      
      modelBuilder.HasPostgresEnum<Role>();
      modelBuilder.HasPostgresExtension("postgis");

      modelBuilder.Entity<Restaurant>()
         .HasGeneratedTsVectorColumn(
            r => r.FullTextSearch,
            "english",
            r => new
            {
               r.Name, 
               r.Description, 
               r.Address
               // EName = ToEnglish(r.Name),
               // EDescription = ToEnglish(r.Description),
               // EAddress = ToEnglish(r.Address)
            })
         .HasIndex(r => r.FullTextSearch)
         .HasMethod("GIN");
         // .HasOperators("gin");
      
      modelBuilder.Entity<Food>()
         .HasGeneratedTsVectorColumn(
            fod => fod.FullTextSearch,
            "english",
            fod => new
            {
               fod.Name, 
               fod.Description,
               // EName = ToEnglish(fod.Name),
               // EDescription = ToEnglish(fod.Description ?? string.Empty),
            })
         .HasIndex(r => r.FullTextSearch)
         .HasMethod("GIN");
         // .HasOperators("gin");

         // modelBuilder.Entity<Food>(entity =>
         // {
         //    entity.Property(e => e.FullTextSearch)
         //       .HasComputedColumnSql(
         //          "to_tsvector('english', " +
         //          "coalesce(PgSqlUnaccent(Name), '') || ' ' || " +
         //          "coalesce(PgSqlUnaccent(Description), ''))")
         //       .IsRequired();
         // });

         modelBuilder.Entity<Rekomer>()
         .HasGeneratedTsVectorColumn(
            rek => rek.FullTextSearch,
            "english",
            rek => new
            {
               rek.FullName, 
               rek.Description,
               // EFullName = ToEnglish(rek.FullName ?? string.Empty),
               // EDescription = ToEnglish(rek.Description ?? string.Empty),
            })
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