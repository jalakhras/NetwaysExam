using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Netways.EntityFramworkCore.Migrations
{
    public partial class InitalDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.EnsureSchema(
                name: "Netways");

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Netways",
                columns: table => new
                {
                    LoginName = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Salary = table.Column<int>(nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LoginName);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Lookup",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Afghanistan" },
                    { 33, "British Virgin Islands" },
                    { 32, "British Indian Ocean Territory" },
                    { 31, "Brazil" },
                    { 30, "Bouvet Island" },
                    { 29, "Botswana" },
                    { 28, "Bosnia and Herzegovina" },
                    { 27, "Bonaire" },
                    { 26, "Bolivia" },
                    { 25, "Bhutan" },
                    { 24, "Bermuda" },
                    { 23, "Benin" },
                    { 22, "Belize" },
                    { 21, "Belgium" },
                    { 20, "Belarus" },
                    { 19, "Barbados" },
                    { 18, "Bangladesh" },
                    { 17, "Bahrain" },
                    { 2, "Åland" },
                    { 3, "Albania" },
                    { 4, "Algeria" },
                    { 5, "American Samoa" },
                    { 6, "Andorra" },
                    { 7, "Anguilla" },
                    { 34, "Brunei" },
                    { 8, "Antarctica" },
                    { 10, "Argentina" },
                    { 11, "Armenia" },
                    { 12, "Aruba" },
                    { 13, "Australia" },
                    { 15, "Azerbaijan" },
                    { 16, "Bahamas" },
                    { 9, "Antigua and Barbuda" },
                    { 35, "Bulgaria" }
                });

            migrationBuilder.InsertData(
                schema: "Netways",
                table: "Users",
                columns: new[] { "LoginName", "Address", "CountryId", "DateofBirth", "DisplayName", "IsActive", "PasswordHash", "PasswordSalt", "ProfilePicture", "Salary" },
                values: new object[] { "Netways@Admin", "Lebanon / Beirut", 10, new DateTime(1994, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netways", true, null, null, null, 1000 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                schema: "Netways",
                table: "Users",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "Netways");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "Lookup");
        }
    }
}
