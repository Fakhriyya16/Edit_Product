using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello_MVC.Migrations
{
    public partial class CreatedSocialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9775));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9777));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9748));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9751));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9754));

            migrationBuilder.InsertData(
                table: "Socials",
                columns: new[] { "Id", "CreatedDate", "Link", "Name", "SoftDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9624), "https://x.com/home?lang=ru", "Twitter", false },
                    { 2, new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9626), "https://x.com/home?lang=ru", "Instagram", false },
                    { 3, new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9629), "https://x.com/home?lang=ru", "Tumblr", false },
                    { 4, new DateTime(2024, 5, 21, 23, 15, 21, 580, DateTimeKind.Local).AddTicks(9631), "https://x.com/home?lang=ru", "Pinterest", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7371));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7372));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7374));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7271));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7274));
        }
    }
}
