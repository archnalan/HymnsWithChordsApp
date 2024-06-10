using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class HymnBookChartModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "ChordChartFilePath",
                table: "Chords");

            migrationBuilder.RenameColumn(
                name: "audioFilePath",
                table: "Chords",
                newName: "ChordAudioFilePath");

            migrationBuilder.AddColumn<int>(
                name: "ChordChartId",
                table: "Chords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HymnBookId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChordsCharts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChordId = table.Column<int>(type: "int", nullable: false),
                    ChartAudioFilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PositionDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChordChartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChordsCharts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChordsCharts_Chords_ChordId",
                        column: x => x.ChordId,
                        principalTable: "Chords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HymnBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HymnBooks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_HymnBookId",
                table: "Categories",
                column: "HymnBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ChordsCharts_ChordId",
                table: "ChordsCharts",
                column: "ChordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_HymnBooks_HymnBookId",
                table: "Categories",
                column: "HymnBookId",
                principalTable: "HymnBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments",
                column: "LiricLineId",
                principalTable: "LyricLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_HymnBooks_HymnBookId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments");

            migrationBuilder.DropTable(
                name: "ChordsCharts");

            migrationBuilder.DropTable(
                name: "HymnBooks");

            migrationBuilder.DropIndex(
                name: "IX_Categories_HymnBookId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ChordChartId",
                table: "Chords");

            migrationBuilder.DropColumn(
                name: "HymnBookId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ChordAudioFilePath",
                table: "Chords",
                newName: "audioFilePath");

            migrationBuilder.AddColumn<string>(
                name: "ChordChartFilePath",
                table: "Chords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments",
                column: "LiricLineId",
                principalTable: "LyricLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
