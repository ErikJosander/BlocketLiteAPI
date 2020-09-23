using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlocketLiteEFCoreDB.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNr",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Advertisements",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contact", "CreatedOn" },
                values: new object[] { "10708 001122", new DateTimeOffset(new DateTime(2020, 9, 17, 9, 13, 7, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Contact", "CreatedOn" },
                values: new object[] { "1111-33334", new DateTimeOffset(new DateTime(2020, 9, 17, 9, 13, 7, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Contact", "CreatedOn" },
                values: new object[] { "000-33334", new DateTimeOffset(new DateTime(2020, 9, 17, 9, 13, 7, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 9, 17, 11, 13, 7, 594, DateTimeKind.Local).AddTicks(5838));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 9, 17, 11, 13, 7, 596, DateTimeKind.Local).AddTicks(6847));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "ContactNr",
                table: "Advertisements",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContactNr", "CreatedOn" },
                values: new object[] { "10708 001122", new DateTimeOffset(new DateTime(2020, 9, 17, 8, 56, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ContactNr", "CreatedOn" },
                values: new object[] { "1111-33334", new DateTimeOffset(new DateTime(2020, 9, 17, 8, 56, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContactNr", "CreatedOn" },
                values: new object[] { "000-33334", new DateTimeOffset(new DateTime(2020, 9, 17, 8, 56, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 9, 17, 10, 56, 5, 827, DateTimeKind.Local).AddTicks(4986));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 9, 17, 10, 56, 5, 829, DateTimeKind.Local).AddTicks(6868));
        }
    }
}
