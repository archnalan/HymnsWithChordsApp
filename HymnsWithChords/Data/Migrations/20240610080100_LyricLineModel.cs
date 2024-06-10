using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class LyricLineModel : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "ChorusId",
                table: "LyricSegments");

            migrationBuilder.RenameColumn(
                name: "VerseId",
                table: "LyricSegments",
                newName: "LiricLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LyricSegments_VerseId",
                table: "LyricSegments",
                newName: "IX_LyricSegments_LiricLineId");

            migrationBuilder.AddColumn<string>(
                name: "DisplayTitle",
                table: "Choruses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "audioFilePath",
                table: "Chords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Bridges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LyricLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LyricLineOrder = table.Column<int>(type: "int", nullable: false),
                    VerseId = table.Column<int>(type: "int", nullable: true),
                    ChorusId = table.Column<int>(type: "int", nullable: true),
                    BridgeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LyricLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LyricLines_Bridges_BridgeId",
                        column: x => x.BridgeId,
                        principalTable: "Bridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LyricLines_Choruses_ChorusId",
                        column: x => x.ChorusId,
                        principalTable: "Choruses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LyricLines_Verses_VerseId",
                        column: x => x.VerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LyricLines_BridgeId",
                table: "LyricLines",
                column: "BridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricLines_ChorusId",
                table: "LyricLines",
                column: "ChorusId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricLines_VerseId",
                table: "LyricLines",
                column: "VerseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments",
                column: "LiricLineId",
                principalTable: "LyricLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments");

            migrationBuilder.DropTable(
                name: "LyricLines");

            migrationBuilder.DropColumn(
                name: "DisplayTitle",
                table: "Choruses");

            migrationBuilder.DropColumn(
                name: "audioFilePath",
                table: "Chords");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Bridges");

            migrationBuilder.RenameColumn(
                name: "LiricLineId",
                table: "LyricSegments",
                newName: "VerseId");

            migrationBuilder.RenameIndex(
                name: "IX_LyricSegments_LiricLineId",
                table: "LyricSegments",
                newName: "IX_LyricSegments_VerseId");

            migrationBuilder.AddColumn<int>(
                name: "BridgeId",
                table: "LyricSegments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChorusId",
                table: "LyricSegments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_BridgeId",
                table: "LyricSegments",
                column: "BridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_ChorusId",
                table: "LyricSegments",
                column: "ChorusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Bridges_BridgeId",
                table: "LyricSegments",
                column: "BridgeId",
                principalTable: "Bridges",
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
    }
}
