﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RekomBackend.Database;

#nullable disable

namespace RekomBackend.Migrations
{
    [DbContext(typeof(RekomContext))]
    [Migration("20230221071718_CreateRestaurantAndFood")]
    partial class CreateRestaurantAndFood
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Account", b =>
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

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Follow", b =>
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

                    b.HasIndex("FollowerId");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Food", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

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

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Otp", b =>
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

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Rekomer", b =>
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

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Restaurant", b =>
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

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Follow", b =>
                {
                    b.HasOne("RekomBackend.App.Models.Entities.Rekomer", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekomBackend.App.Models.Entities.Rekomer", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Food", b =>
                {
                    b.HasOne("RekomBackend.App.Models.Entities.Restaurant", "Restaurant")
                        .WithMany("Menu")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Otp", b =>
                {
                    b.HasOne("RekomBackend.App.Models.Entities.Account", "Account")
                        .WithOne("Otp")
                        .HasForeignKey("RekomBackend.App.Models.Entities.Otp", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Rekomer", b =>
                {
                    b.HasOne("RekomBackend.App.Models.Entities.Account", "Account")
                        .WithOne("Rekomer")
                        .HasForeignKey("RekomBackend.App.Models.Entities.Rekomer", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Restaurant", b =>
                {
                    b.HasOne("RekomBackend.App.Models.Entities.Account", "Account")
                        .WithOne("Restaurant")
                        .HasForeignKey("RekomBackend.App.Models.Entities.Restaurant", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Account", b =>
                {
                    b.Navigation("Otp");

                    b.Navigation("Rekomer");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Rekomer", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Followings");
                });

            modelBuilder.Entity("RekomBackend.App.Models.Entities.Restaurant", b =>
                {
                    b.Navigation("Menu");
                });
#pragma warning restore 612, 618
        }
    }
}