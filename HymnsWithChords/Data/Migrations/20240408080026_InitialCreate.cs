using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hymns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WrittenDateRange = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WrittenBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    History = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hymns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hymns_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LyricSegments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Words = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Chord = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HymnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LyricSegments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LyricSegments_Hymns_HymnId",
                        column: x => x.HymnId,
                        principalTable: "Hymns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hymns_CategoryId",
                table: "Hymns",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_HymnId",
                table: "LyricSegments",
                column: "HymnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LyricSegments");

            migrationBuilder.DropTable(
                name: "Hymns");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
