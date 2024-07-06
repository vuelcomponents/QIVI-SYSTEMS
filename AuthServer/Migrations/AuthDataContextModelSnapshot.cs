﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using authServer.Data;

#nullable disable

namespace authServer.Migrations
{
    [DbContext(typeof(AuthDataContext))]
    partial class AuthDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("LicenceUser", b =>
                {
                    b.Property<long>("LicencesId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("LicencesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LicenceUser");
                });

            modelBuilder.Entity("ClassLibrary.Models.BlockedToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BlockedTokens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BlockedToken");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ClassLibrary.Models.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Blocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IpBlocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("SignedInDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("TokenId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Verified")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("TokenId");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ClassLibrary.Models.Gender", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "Man"
                        },
                        new
                        {
                            Id = 2L,
                            Code = "Woman"
                        },
                        new
                        {
                            Id = 3L,
                            Code = "Other"
                        });
                });

            modelBuilder.Entity("ClassLibrary.Models.Licence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndTimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<short>("MaxUsersQty")
                        .HasColumnType("smallint");

                    b.Property<int>("Software")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTimeStamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Licences");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "LI:Super::HRTechnique",
                            EndTimeStamp = new DateTime(2024, 8, 6, 13, 59, 19, 71, DateTimeKind.Local).AddTicks(2394),
                            Host = "http://192.168.1.81:1830",
                            Icon = "mdi mdi-language-r",
                            MaxUsersQty = (short)5,
                            Software = 0,
                            StartTimeStamp = new DateTime(2024, 7, 6, 13, 59, 19, 71, DateTimeKind.Local).AddTicks(2338)
                        });
                });

            modelBuilder.Entity("ClassLibrary.Models.Notification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("Seen")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ClassLibrary.Models.SessionToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SessionTokens");
                });

            modelBuilder.Entity("ClassLibrary.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Blocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ChangeEmailDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ClaimDeviceVerification")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ClaimTokenExpiryMinutes")
                        .HasColumnType("int");

                    b.Property<string>("CompanyAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyNip")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyPhone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("GenderId")
                        .HasColumnType("bigint");

                    b.Property<bool>("HrtLicence")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockedChoiceClaimDeviceVerification")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockedClaimTokenExpiryMinutes")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LombanditLicence")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MaxUsersCount")
                        .HasColumnType("int");

                    b.Property<int>("MonthlyActivities")
                        .HasColumnType("int");

                    b.Property<int>("MonthlyVisits")
                        .HasColumnType("int");

                    b.Property<string>("NewEmail")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("PersonalId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PersonalPhone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("RefreshToken")
                        .HasColumnType("longblob");

                    b.Property<DateTime?>("ResetPasswordDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<bool>("SuppressSelfSecurityChanges")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("SuppressTokenRefresh")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Verified")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("VerifyDeviceDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("VerifyEmailDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClassLibrary.Models.BlockedChangeEmailToken", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.BlockedToken");

                    b.HasDiscriminator().HasValue("BlockedChangeEmailToken");
                });

            modelBuilder.Entity("ClassLibrary.Models.BlockedDeviceVerifyToken", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.BlockedToken");

                    b.HasDiscriminator().HasValue("BlockedDeviceVerifyToken");
                });

            modelBuilder.Entity("ClassLibrary.Models.BlockedRefreshToken", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.BlockedToken");

                    b.HasDiscriminator().HasValue("BlockedRefreshToken");
                });

            modelBuilder.Entity("ClassLibrary.Models.BlockedResetPasswordToken", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.BlockedToken");

                    b.HasDiscriminator().HasValue("BlockedResetPasswordToken");
                });

            modelBuilder.Entity("ClassLibrary.Models.BlockedVerifyToken", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.BlockedToken");

                    b.HasDiscriminator().HasValue("BlockedVerifyToken");
                });

            modelBuilder.Entity("LicenceUser", b =>
                {
                    b.HasOne("ClassLibrary.Models.Licence", null)
                        .WithMany()
                        .HasForeignKey("LicencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassLibrary.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibrary.Models.Device", b =>
                {
                    b.HasOne("ClassLibrary.Models.SessionToken", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.HasOne("ClassLibrary.Models.User", null)
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Token");
                });

            modelBuilder.Entity("ClassLibrary.Models.Notification", b =>
                {
                    b.HasOne("ClassLibrary.Models.User", null)
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ClassLibrary.Models.User", b =>
                {
                    b.HasOne("ClassLibrary.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("ClassLibrary.Models.User", null)
                        .WithMany("Users")
                        .HasForeignKey("UserId");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("ClassLibrary.Models.User", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Notifications");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
