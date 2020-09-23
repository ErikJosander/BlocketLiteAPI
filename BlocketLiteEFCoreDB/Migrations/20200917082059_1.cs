using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlocketLiteEFCoreDB.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAdress = table.Column<string>(maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    ConstructionYear = table.Column<int>(maxLength: 4, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SellingPrice = table.Column<int>(maxLength: 50, nullable: true),
                    RentingPrice = table.Column<int>(maxLength: 50, nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    PropertyTypeId = table.Column<int>(nullable: false),
                    CanBeSold = table.Column<bool>(nullable: false),
                    CanBeRented = table.Column<bool>(nullable: false),
                    AdressId = table.Column<int>(nullable: false),
                    ContactNr = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisements_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    RatedUserId = table.Column<int>(nullable: true),
                    RatingUserId1 = table.Column<int>(nullable: true),
                    RatingUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_RatingUserId",
                        column: x => x.RatingUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_RatingUserId1",
                        column: x => x.RatingUserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(maxLength: 250, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AdvertisementId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "apartment" },
                    { 2, "house" },
                    { 3, "office" },
                    { 4, "warehouse" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "Erik@gmail.com", "123", "Calle" },
                    { 2, "Johan@gmail.com", "123", "Johan" },
                    { 3, "Alex@gmail.com", "123", "Alex" }
                });

            migrationBuilder.InsertData(
                table: "Advertisements",
                columns: new[] { "Id", "AdressId", "CanBeRented", "CanBeSold", "ConstructionYear", "ContactNr", "CreationDate", "Description", "PropertyTypeId", "RentingPrice", "SellingPrice", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, true, false, 1978, "10708 001122", new DateTime(2020, 9, 17, 10, 20, 59, 308, DateTimeKind.Local).AddTicks(127), "Big apartment with 5 rooms for rent", 1, 350, null, "Very very cool apartment for rent", 1 },
                    { 2, 2, false, true, 1921, "1111-33334", new DateTime(2020, 9, 17, 10, 20, 59, 310, DateTimeKind.Local).AddTicks(3798), "Cozy house, close to the sea, 2 bedrooms and a big garden", 2, null, 1200000, "Small house in fishing village", 2 },
                    { 3, 3, true, false, 2012, "000-33334", new DateTime(2020, 9, 17, 10, 20, 59, 310, DateTimeKind.Local).AddTicks(3880), "Small office space 10sq in the heart of London", 3, 500, null, "Office Space in London", 3 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "RatedUserId", "RatingUserId", "RatingUserId1", "Value" },
                values: new object[] { 1, 1, 2, null, 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AdvertisementId", "Content", "CreationDate", "UserId" },
                values: new object[] { 1, 1, "huml, humla humla. Text", new DateTime(2020, 9, 17, 10, 20, 59, 310, DateTimeKind.Local).AddTicks(5580), 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AdvertisementId", "Content", "CreationDate", "UserId" },
                values: new object[] { 2, 2, "huml, humla humla. Comment 2", new DateTime(2020, 9, 17, 10, 20, 59, 310, DateTimeKind.Local).AddTicks(6375), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_AdressId",
                table: "Advertisements",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_PropertyTypeId",
                table: "Advertisements",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_UserId",
                table: "Advertisements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AdvertisementId",
                table: "Comments",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RatingUserId",
                table: "Ratings",
                column: "RatingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RatingUserId1",
                table: "Ratings",
                column: "RatingUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
