using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataStore.SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Citiy",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryConnectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citiy", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Citiy_Country_CountryConnectId",
                        column: x => x.CountryConnectId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelStar = table.Column<int>(type: "int", nullable: false),
                    HotelPrice = table.Column<int>(type: "int", nullable: false),
                    HotelDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityConnctId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotel_Citiy_CityConnctId",
                        column: x => x.CityConnctId,
                        principalTable: "Citiy",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelConnctId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Hotel_HotelConnctId",
                        column: x => x.HotelConnctId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { 1, "O'zbekiston" },
                    { 2, "Fransiya" }
                });

            migrationBuilder.InsertData(
                table: "Citiy",
                columns: new[] { "CityId", "CityName", "CountryConnectId" },
                values: new object[,]
                {
                    { 1, "Toshkent", 1 },
                    { 2, "Paris", 2 }
                });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "HotelId", "CityConnctId", "HotelDescription", "HotelName", "HotelPrice", "HotelStar" },
                values: new object[,]
                {
                    { 1, 1, "Located in Tashkent, Hilton Tashkent City has a bar and a terrace.", "Hilton", 400, 3 },
                    { 2, 2, "Отель B&B HOTEL Paris 17 Batignolles расположен на северо-западе Парижа,", "B&B", 500, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citiy_CountryConnectId",
                table: "Citiy",
                column: "CountryConnectId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CityConnctId",
                table: "Hotel",
                column: "CityConnctId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_HotelConnctId",
                table: "Image",
                column: "HotelConnctId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Citiy");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
