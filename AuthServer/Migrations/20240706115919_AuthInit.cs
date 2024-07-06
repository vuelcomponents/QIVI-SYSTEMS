using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authServer.Migrations
{
    /// <inheritdoc />
    public partial class AuthInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase().Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "BlockedTokens",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        Token = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                        Discriminator = table
                            .Column<string>(type: "varchar(34)", maxLength: 34, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_BlockedTokens", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "Genders",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        Code = table
                            .Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Genders", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "Licences",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        Code = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Icon = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Software = table.Column<int>(type: "int", nullable: false),
                        Host = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        StartTimeStamp = table.Column<DateTime>(
                            type: "datetime(6)",
                            nullable: false
                        ),
                        EndTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                        MaxUsersQty = table.Column<short>(type: "smallint", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Licences", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "SessionTokens",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        Token = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_SessionTokens", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "Users",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        Verified = table.Column<bool>(type: "tinyint(1)", nullable: true),
                        Email = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Password = table.Column<byte[]>(type: "longblob", nullable: false),
                        Salt = table.Column<byte[]>(type: "longblob", nullable: false),
                        HrtLicence = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        LombanditLicence = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        Role = table.Column<int>(type: "int", nullable: false),
                        FirstName = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        LastName = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                        PersonalId = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        GenderId = table.Column<long>(type: "bigint", nullable: true),
                        PersonalPhone = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Address = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        PostCode = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        City = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        CompanyNip = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        CompanyName = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        CompanyAddress = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        CompanyPhone = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Website = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        UserId = table.Column<long>(type: "bigint", nullable: true),
                        ResetPasswordDateTime = table.Column<DateTime>(
                            type: "datetime(6)",
                            nullable: true
                        ),
                        VerifyEmailDateTime = table.Column<DateTime>(
                            type: "datetime(6)",
                            nullable: true
                        ),
                        VerifyDeviceDateTime = table.Column<DateTime>(
                            type: "datetime(6)",
                            nullable: true
                        ),
                        ChangeEmailDateTime = table.Column<DateTime>(
                            type: "datetime(6)",
                            nullable: true
                        ),
                        NewEmail = table
                            .Column<string>(type: "longtext", nullable: true)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        RefreshToken = table.Column<byte[]>(type: "longblob", nullable: true),
                        Blocked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        SuppressSelfSecurityChanges = table.Column<bool>(
                            type: "tinyint(1)",
                            nullable: false
                        ),
                        ClaimDeviceVerification = table.Column<bool>(
                            type: "tinyint(1)",
                            nullable: false
                        ),
                        LockedChoiceClaimDeviceVerification = table.Column<bool>(
                            type: "tinyint(1)",
                            nullable: false
                        ),
                        ClaimTokenExpiryMinutes = table.Column<int>(type: "int", nullable: false),
                        LockedClaimTokenExpiryMinutes = table.Column<bool>(
                            type: "tinyint(1)",
                            nullable: false
                        ),
                        SuppressTokenRefresh = table.Column<bool>(
                            type: "tinyint(1)",
                            nullable: false
                        ),
                        MaxUsersCount = table.Column<int>(type: "int", nullable: false),
                        MonthlyVisits = table.Column<int>(type: "int", nullable: false),
                        MonthlyActivities = table.Column<int>(type: "int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Users", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Users_Genders_GenderId",
                            column: x => x.GenderId,
                            principalTable: "Genders",
                            principalColumn: "Id"
                        );
                        table.ForeignKey(
                            name: "FK_Users_Users_UserId",
                            column: x => x.UserId,
                            principalTable: "Users",
                            principalColumn: "Id"
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "Devices",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        Ip = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        UserAgent = table
                            .Column<string>(type: "longtext", nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        TokenId = table.Column<long>(type: "bigint", nullable: true),
                        Blocked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        IpBlocked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        SignedInDateTime = table.Column<DateTime>(
                            type: "datetime(6)",
                            nullable: true
                        )
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Devices", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Devices_SessionTokens_TokenId",
                            column: x => x.TokenId,
                            principalTable: "SessionTokens",
                            principalColumn: "Id"
                        );
                        table.ForeignKey(
                            name: "FK_Devices_Users_UserId",
                            column: x => x.UserId,
                            principalTable: "Users",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "LicenceUser",
                    columns: table => new
                    {
                        LicencesId = table.Column<long>(type: "bigint", nullable: false),
                        UsersId = table.Column<long>(type: "bigint", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_LicenceUser", x => new { x.LicencesId, x.UsersId });
                        table.ForeignKey(
                            name: "FK_LicenceUser_Licences_LicencesId",
                            column: x => x.LicencesId,
                            principalTable: "Licences",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade
                        );
                        table.ForeignKey(
                            name: "FK_LicenceUser_Users_UsersId",
                            column: x => x.UsersId,
                            principalTable: "Users",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "Notifications",
                    columns: table => new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "MySql:ValueGenerationStrategy",
                                MySqlValueGenerationStrategy.IdentityColumn
                            ),
                        TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                        Seen = table.Column<bool>(type: "tinyint(1)", nullable: false),
                        UserId = table.Column<long>(type: "bigint", nullable: true),
                        Code = table
                            .Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4"),
                        Description = table
                            .Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                            .Annotation("MySql:CharSet", "utf8mb4")
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Notifications", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Notifications_Users_UserId",
                            column: x => x.UserId,
                            principalTable: "Users",
                            principalColumn: "Id"
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1L, "Man" },
                    { 2L, "Woman" },
                    { 3L, "Other" }
                }
            );

            migrationBuilder.InsertData(
                table: "Licences",
                columns: new[]
                {
                    "Id",
                    "Code",
                    "EndTimeStamp",
                    "Host",
                    "Icon",
                    "MaxUsersQty",
                    "Software",
                    "StartTimeStamp"
                },
                values: new object[]
                {
                    1L,
                    "LI:Super::HRTechnique",
                    new DateTime(2024, 8, 6, 13, 59, 19, 71, DateTimeKind.Local).AddTicks(2394),
                    "http://192.168.1.81:1830",
                    "mdi mdi-language-r",
                    (short)5,
                    0,
                    new DateTime(2024, 7, 6, 13, 59, 19, 71, DateTimeKind.Local).AddTicks(2338)
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TokenId",
                table: "Devices",
                column: "TokenId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_LicenceUser_UsersId",
                table: "LicenceUser",
                column: "UsersId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId"
            );

            migrationBuilder.CreateIndex(name: "IX_Users_UserId", table: "Users", column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BlockedTokens");

            migrationBuilder.DropTable(name: "Devices");

            migrationBuilder.DropTable(name: "LicenceUser");

            migrationBuilder.DropTable(name: "Notifications");

            migrationBuilder.DropTable(name: "SessionTokens");

            migrationBuilder.DropTable(name: "Licences");

            migrationBuilder.DropTable(name: "Users");

            migrationBuilder.DropTable(name: "Genders");
        }
    }
}
