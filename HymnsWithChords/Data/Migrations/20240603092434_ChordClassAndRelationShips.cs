using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChordClassAndRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Bridges_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Choruses_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Verses_VerseId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "Chord",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "ChordChartFilePath",
                table: "LyricSegments");

            migrationBuilder.AddColumn<int>(
                name: "ChordId",
                table: "LyricSegments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChordName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    ChordChartFilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_ChordId",
                table: "LyricSegments",
                column: "ChordId");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Bridges_BridgeId",
                table: "LyricSegments",
                column: "BridgeId",
                principalTable: "Bridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Chords_ChordId",
                table: "LyricSegments",
                column: "ChordId",
                principalTable: "Chords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Choruses_ChorusId",
                table: "LyricSegments",
                column: "ChorusId",
                principalTable: "Choruses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Verses_VerseId",
                table: "LyricSegments",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Bridges_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Chords_ChordId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Choruses_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Verses_VerseId",
                table: "LyricSegments");

            migrationBuilder.DropTable(
                name: "Chords");

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_ChordId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "ChordId",
                table: "LyricSegments");

            migrationBuilder.AddColumn<string>(
                name: "Chord",
                table: "LyricSegments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChordChartFilePath",
                table: "LyricSegments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Bridges_BridgeId",
                table: "LyricSegments",
                column: "BridgeId",
                principalTable: "Bridges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Choruses_ChorusId",
                table: "LyricSegments",
                column: "ChorusId",
                principalTable: "Choruses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Verses_VerseId",
                table: "LyricSegments",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id");
        }
    }
}
