﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RekomBackend.Database;

#nullable disable

namespace RekomBackend.Migrations
{
    [DbContext(typeof(RekomContext))]
    partial class RekomContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RekomBackend.App.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("enum('Rekomer', 'Restaurant', 'Admin')");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RekomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ReviewId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("RekomerId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Follow", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FollowerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FollowingId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FollowingId");

                    b.HasIndex("FollowerId", "FollowingId")
                        .IsUnique();

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Food", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("tinytext");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<string>("RestaurantId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.FoodImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FoodId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("FoodImage");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Otp", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Otps");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Rating", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<uint>("Point")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Tag");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.RatingResultView", b =>
                {
                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<float>("Average")
                        .HasColumnType("float");

                    b.Property<float>("PercentFive")
                        .HasColumnType("float");

                    b.Property<float>("PercentFour")
                        .HasColumnType("float");

                    b.Property<float>("PercentOne")
                        .HasColumnType("float");

                    b.Property<float>("PercentThree")
                        .HasColumnType("float");

                    b.Property<float>("PercentTwo")
                        .HasColumnType("float");

                    b.Property<string>("RestaurantId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ToTable((string)null);

                    b.ToView("RatingResultViews", (string)null);
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Reaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<uint>("Point")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Rekomer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Rekomers");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Restaurant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Coordinates")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CoverImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Review", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RatingId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RekomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RestaurantId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.HasIndex("RekomerId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.ReviewMedia", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReviewId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("enum('image', 'video')");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReviewMedias");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.ReviewReaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReactionId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RekomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ReviewId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ReactionId");

                    b.HasIndex("RekomerId");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReviewReactions");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Comment", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Rekomer", "Rekomer")
                        .WithMany("Comments")
                        .HasForeignKey("RekomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Entities.Review", "Review")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rekomer");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Follow", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Rekomer", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Entities.Rekomer", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Food", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Restaurant", "Restaurant")
                        .WithMany("Menu")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.FoodImage", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Food", "Food")
                        .WithMany("Images")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Otp", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Account", "Account")
                        .WithOne("Otp")
                        .HasForeignKey("RekomBackend.App.Entities.Otp", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Rekomer", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Account", "Account")
                        .WithOne("Rekomer")
                        .HasForeignKey("RekomBackend.App.Entities.Rekomer", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Restaurant", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Account", "Account")
                        .WithOne("Restaurant")
                        .HasForeignKey("RekomBackend.App.Entities.Restaurant", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Review", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Rating", "Rating")
                        .WithMany("Reviews")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Entities.Rekomer", "Rekomer")
                        .WithMany("Reviews")
                        .HasForeignKey("RekomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Entities.Restaurant", "Restaurant")
                        .WithMany("Reviews")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rating");

                    b.Navigation("Rekomer");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.ReviewMedia", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Review", "Review")
                        .WithMany("Medias")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.ReviewReaction", b =>
                {
                    b.HasOne("RekomBackend.App.Entities.Reaction", "Reaction")
                        .WithMany("ReviewReactions")
                        .HasForeignKey("ReactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Entities.Rekomer", "Rekomer")
                        .WithMany("ReviewReactions")
                        .HasForeignKey("RekomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Entities.Review", "Review")
                        .WithMany("ReviewReactions")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reaction");

                    b.Navigation("Rekomer");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Account", b =>
                {
                    b.Navigation("Otp");

                    b.Navigation("Rekomer");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Food", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Rating", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Reaction", b =>
                {
                    b.Navigation("ReviewReactions");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Rekomer", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("ReviewReactions");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Restaurant", b =>
                {
                    b.Navigation("Menu");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("RekomBackend.App.Entities.Review", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Medias");

                    b.Navigation("ReviewReactions");
                });
#pragma warning restore 612, 618
        }
    }
}
