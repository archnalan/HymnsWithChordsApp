using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChordChartRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "ChordsCharts");

			migrationBuilder.CreateTable(
				name: "ChordCharts",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FilePath = table.Column<string>(nullable: false),
					ChordId = table.Column<int>(nullable: false),
					FretPosition = table.Column<int>(nullable: false),
					ChartAudioFilePath = table.Column<string>(maxLength: 255, nullable: true),
					PositionDescription = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ChordCharts", x => x.Id);
					table.ForeignKey(
						name: "FK_ChordCharts_Chords_ChordId",
						column: x => x.ChordId,
						principalTable: "Chords",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_ChordCharts_ChordId",
				table: "ChordCharts",
				column: "ChordId");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "ChordCharts");

			migrationBuilder.CreateTable(
				name: "ChordsCharts",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ChordId = table.Column<int>(type: "int", nullable: false),
					ChartAudioFilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
					FretPosition = table.Column<int>(type: "int", nullable: false),
					PositionDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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

			migrationBuilder.CreateIndex(
				name: "IX_ChordsCharts_ChordId",
				table: "ChordsCharts",
				column: "ChordId");

		}
    }
}
