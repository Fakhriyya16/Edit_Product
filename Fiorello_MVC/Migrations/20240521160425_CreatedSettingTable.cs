using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello_MVC.Migrations
{
    public partial class CreatedSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CreatedDate", "Key", "SoftDeleted", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7271), "HeaderLogo", false, "logo.png" },
                    { 2, new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7273), "Phone", false, "23145" },
                    { 3, new DateTime(2024, 5, 21, 20, 4, 25, 490, DateTimeKind.Local).AddTicks(7274), "Address", false, "28 May" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 15, 19, 16, 3, 67, DateTimeKind.Local).AddTicks(1488));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 15, 19, 16, 3, 67, DateTimeKind.Local).AddTicks(1492));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 15, 19, 16, 3, 67, DateTimeKind.Local).AddTicks(1495));
        }
    }
}
