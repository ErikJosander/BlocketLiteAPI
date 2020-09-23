using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlocketLiteEFCoreDB.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Adresses_AdressId",
                table: "Advertisements");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_AdressId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "AdressId",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Advertisements",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Advertisements");

            migrationBuilder.AddColumn<int>(
                name: "AdressId",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "Id", "City", "PostalCode", "StreetAdress" },
                values: new object[,]
                {
                    { 1, "New York", "34599", "Popstreet 4" },
                    { 2, "San Fransisco", "22299", "ParadiseSt 24" },
                    { 3, "London", "12389", "Trafageler Square 5" }
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 17, 18, 54, 29, 731, DateTimeKind.Local).AddTicks(9187));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 17, 18, 54, 29, 736, DateTimeKind.Local).AddTicks(2134));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 9, 17, 18, 54, 29, 736, DateTimeKind.Local).AddTicks(2241));

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AdressId", "CreatedOn" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 9, 17, 16, 54, 29, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AdressId", "CreatedOn" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2020, 9, 17, 16, 54, 29, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Advertisements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AdressId", "CreatedOn" },
                values: new object[] { 3, new DateTimeOffset(new DateTime(2020, 9, 17, 16, 54, 29, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_AdressId",
                table: "Advertisements",
                column: "AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Adresses_AdressId",
                table: "Advertisements",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
