using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class VerseBridgeChorusClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bridge_Hymns_HymnId",
                table: "Bridge");

            migrationBuilder.DropForeignKey(
                name: "FK_Chorus_Hymns_HymnId",
                table: "Chorus");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Bridge_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Chorus_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Verse_VerseId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_Verse_Hymns_HymnId",
                table: "Verse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Verse",
                table: "Verse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chorus",
                table: "Chorus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bridge",
                table: "Bridge");

            migrationBuilder.RenameTable(
                name: "Verse",
                newName: "Verses");

            migrationBuilder.RenameTable(
                name: "Chorus",
                newName: "Choruses");

            migrationBuilder.RenameTable(
                name: "Bridge",
                newName: "Bridges");

            migrationBuilder.RenameIndex(
                name: "IX_Verse_HymnId",
                table: "Verses",
                newName: "IX_Verses_HymnId");

            migrationBuilder.RenameIndex(
                name: "IX_Chorus_HymnId",
                table: "Choruses",
                newName: "IX_Choruses_HymnId");

            migrationBuilder.RenameIndex(
                name: "IX_Bridge_HymnId",
                table: "Bridges",
                newName: "IX_Bridges_HymnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Verses",
                table: "Verses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choruses",
                table: "Choruses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bridges",
                table: "Bridges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bridges_Hymns_HymnId",
                table: "Bridges",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Choruses_Hymns_HymnId",
                table: "Choruses",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Verses_Hymns_HymnId",
                table: "Verses",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bridges_Hymns_HymnId",
                table: "Bridges");

            migrationBuilder.DropForeignKey(
                name: "FK_Choruses_Hymns_HymnId",
                table: "Choruses");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Bridges_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Choruses_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Verses_VerseId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_Verses_Hymns_HymnId",
                table: "Verses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Verses",
                table: "Verses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choruses",
                table: "Choruses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bridges",
                table: "Bridges");

            migrationBuilder.RenameTable(
                name: "Verses",
                newName: "Verse");

            migrationBuilder.RenameTable(
                name: "Choruses",
                newName: "Chorus");

            migrationBuilder.RenameTable(
                name: "Bridges",
                newName: "Bridge");

            migrationBuilder.RenameIndex(
                name: "IX_Verses_HymnId",
                table: "Verse",
                newName: "IX_Verse_HymnId");

            migrationBuilder.RenameIndex(
                name: "IX_Choruses_HymnId",
                table: "Chorus",
                newName: "IX_Chorus_HymnId");

            migrationBuilder.RenameIndex(
                name: "IX_Bridges_HymnId",
                table: "Bridge",
                newName: "IX_Bridge_HymnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Verse",
                table: "Verse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chorus",
                table: "Chorus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bridge",
                table: "Bridge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bridge_Hymns_HymnId",
                table: "Bridge",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chorus_Hymns_HymnId",
                table: "Chorus",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Bridge_BridgeId",
                table: "LyricSegments",
                column: "BridgeId",
                principalTable: "Bridge",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Chorus_ChorusId",
                table: "LyricSegments",
                column: "ChorusId",
                principalTable: "Chorus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Verse_VerseId",
                table: "LyricSegments",
                column: "VerseId",
                principalTable: "Verse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Verse_Hymns_HymnId",
                table: "Verse",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
