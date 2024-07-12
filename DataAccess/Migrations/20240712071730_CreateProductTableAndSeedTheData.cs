using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.WebClient.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductTableAndSeedTheData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auther = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Auther", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1L, "Sudha Murty", "Here, There and Everywhere is a celebration of Sudha Murty's literary journey and is her 200th title across genres and languages. Bringing together her best-loved stories from various collections alongside some new ones and a thoughtful introduction, here is a book that is, in every sense, as multifaceted as its author.", "1234567890123", 200.0, 174.0, 170.0, 160.0, "HereThere and Everywhere" },
                    { 2L, "The Dalai Lama", "In this unique and important book, one of the world's great spiritual leaders offers his practical wisdom and advice on how we can overcome everyday human problems and achieve lasting happiness.", "1234567890123", 350.0, 320.0, 300.0, 310.0, "THE ART OF HAPPINESS" },
                    { 3L, "Peter Casey", "The first and only authorized biography on Tata Group including the Tata-Mistry legal battle, exclusive interviews with Ratan Tata, and never-before-seen photographs of the Tata family.", "1234567890123", 500.0, 450.0, 430.0, 440.0, "The Story of Tata: 1868 to 2021" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
