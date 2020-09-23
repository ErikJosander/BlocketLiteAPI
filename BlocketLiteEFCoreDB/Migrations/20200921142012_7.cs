using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlocketLiteEFCoreDB.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "CreatedOn" },
                values: new object[] { "Varberg Adress", new DateTimeOffset(new DateTime(2020, 9, 21, 14, 20, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "CreatedOn" },
                values: new object[] { "Håstensgatan 4", new DateTimeOffset(new DateTime(2020, 9, 21, 14, 20, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "CreatedOn" },
                values: new object[] { "Hello Adress", new DateTimeOffset(new DateTime(2020, 9, 21, 14, 20, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 21, 16, 20, 12, 116, DateTimeKind.Local).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 21, 16, 20, 12, 126, DateTimeKind.Local).AddTicks(6013));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 21, 16, 20, 12, 126, DateTimeKind.Local).AddTicks(6066));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Adress", "CreatedOn" },
                values: new object[] { "Varberg Adress", new DateTimeOffset(new DateTime(2020, 9, 21, 14, 12, 53, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Adress", "CreatedOn" },
                values: new object[] { "Håstensgatan 4", new DateTimeOffset(new DateTime(2020, 9, 21, 14, 12, 53, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Adress", "CreatedOn" },
                values: new object[] { "Hello Adress", new DateTimeOffset(new DateTime(2020, 9, 21, 14, 12, 53, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 21, 16, 12, 53, 654, DateTimeKind.Local).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 21, 16, 12, 53, 656, DateTimeKind.Local).AddTicks(9282));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 21, 16, 12, 53, 656, DateTimeKind.Local).AddTicks(9329));
        }
    }
}
