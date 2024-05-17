using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class LyricSegmentAdditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Words",
                table: "LyricSegments",
                newName: "Lyric");

            migrationBuilder.AddColumn<int>(
                name: "LyricOrder",
                table: "LyricSegments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LyricOrder",
                table: "LyricSegments");

            migrationBuilder.RenameColumn(
                name: "Lyric",
                table: "LyricSegments",
                newName: "Words");
        }
    }
}
