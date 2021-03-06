﻿// <auto-generated />
using System;
using BoekenApplicatie.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoekenApplicatie.Data.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20180802192819_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Prefix");

                    b.Property<string>("Residence");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ArtistId");

                    b.Property<int>("Edition");

                    b.Property<string>("Isbn");

                    b.Property<int>("Language");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("PriceBought");

                    b.Property<string>("PriceReason");

                    b.Property<Guid?>("PublisherId");

                    b.Property<int>("ReleasedYear");

                    b.Property<string>("SerialNumber");

                    b.Property<Guid?>("TranslatorId");

                    b.Property<int>("YearBought");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("TranslatorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Lending", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BookId");

                    b.Property<DateTimeOffset?>("EndLend");

                    b.Property<DateTimeOffset?>("StartLend");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Lendings");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Prefix");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BookId");

                    b.Property<double>("Rate");

                    b.Property<DateTimeOffset?>("RatingDate");

                    b.Property<string>("Remarks");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Title", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<Guid?>("BookId");

                    b.Property<int>("Category");

                    b.Property<int>("Genre");

                    b.Property<int>("OriginalLangage");

                    b.Property<int>("OriginalReleasedYear");

                    b.Property<string>("OriginalTitle");

                    b.Property<int>("Part");

                    b.Property<string>("SeriesName");

                    b.Property<string>("TitleName");

                    b.Property<Guid?>("TranslatorId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.HasIndex("TranslatorId");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Artist", b =>
                {
                    b.HasBaseType("BoekenApplicatie.Domain.Models.Person");


                    b.ToTable("Artist");

                    b.HasDiscriminator().HasValue("Artist");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Author", b =>
                {
                    b.HasBaseType("BoekenApplicatie.Domain.Models.Person");


                    b.ToTable("Author");

                    b.HasDiscriminator().HasValue("Author");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Translator", b =>
                {
                    b.HasBaseType("BoekenApplicatie.Domain.Models.Person");


                    b.ToTable("Translator");

                    b.HasDiscriminator().HasValue("Translator");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Book", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.Artist", "Artist")
                        .WithMany("Books")
                        .HasForeignKey("ArtistId");

                    b.HasOne("BoekenApplicatie.Domain.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId");

                    b.HasOne("BoekenApplicatie.Domain.Models.Translator")
                        .WithMany("Books")
                        .HasForeignKey("TranslatorId");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Lending", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.Book", "Book")
                        .WithMany("Lendings")
                        .HasForeignKey("BookId");

                    b.HasOne("BoekenApplicatie.Domain.Models.ApplicationUser", "User")
                        .WithMany("Lendings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Rating", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.Book", "Book")
                        .WithMany("Ratings")
                        .HasForeignKey("BookId");

                    b.HasOne("BoekenApplicatie.Domain.Models.ApplicationUser", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BoekenApplicatie.Domain.Models.Title", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.Author", "Author")
                        .WithMany("Titles")
                        .HasForeignKey("AuthorId");

                    b.HasOne("BoekenApplicatie.Domain.Models.Book")
                        .WithMany("Titles")
                        .HasForeignKey("BookId");

                    b.HasOne("BoekenApplicatie.Domain.Models.Translator", "Translator")
                        .WithMany()
                        .HasForeignKey("TranslatorId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BoekenApplicatie.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BoekenApplicatie.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
