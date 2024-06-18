using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class LyricLineIdSegements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments");

            migrationBuilder.RenameColumn(
                name: "LiricLineId",
                table: "LyricSegments",
                newName: "LyricLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LyricSegments_LiricLineId",
                table: "LyricSegments",
                newName: "IX_LyricSegments_LyricLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_LyricLines_LyricLineId",
                table: "LyricSegments",
                column: "LyricLineId",
                principalTable: "LyricLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_LyricLines_LyricLineId",
                table: "LyricSegments");

            migrationBuilder.RenameColumn(
                name: "LyricLineId",
                table: "LyricSegments",
                newName: "LiricLineId");

            migrationBuilder.RenameIndex(
                name: "IX_LyricSegments_LyricLineId",
                table: "LyricSegments",
                newName: "IX_LyricSegments_LiricLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_LyricLines_LiricLineId",
                table: "LyricSegments",
                column: "LiricLineId",
                principalTable: "LyricLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
