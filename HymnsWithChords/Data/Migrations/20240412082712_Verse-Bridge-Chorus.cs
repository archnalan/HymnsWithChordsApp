using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class VerseBridgeChorus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Hymns_HymnId",
                table: "LyricSegments");

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_HymnId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "HymnId",
                table: "LyricSegments");

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

            migrationBuilder.AddColumn<int>(
                name: "VerseId",
                table: "LyricSegments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "History",
                table: "Hymns",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedBy",
                table: "Hymns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextFilePath",
                table: "Hymns",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bridge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HymnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bridge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bridge_Hymns_HymnId",
                        column: x => x.HymnId,
                        principalTable: "Hymns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chorus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HymnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chorus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chorus_Hymns_HymnId",
                        column: x => x.HymnId,
                        principalTable: "Hymns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    HymnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verse_Hymns_HymnId",
                        column: x => x.HymnId,
                        principalTable: "Hymns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_BridgeId",
                table: "LyricSegments",
                column: "BridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_ChorusId",
                table: "LyricSegments",
                column: "ChorusId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_VerseId",
                table: "LyricSegments",
                column: "VerseId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bridge_HymnId",
                table: "Bridge",
                column: "HymnId");

            migrationBuilder.CreateIndex(
                name: "IX_Chorus_HymnId",
                table: "Chorus",
                column: "HymnId");

            migrationBuilder.CreateIndex(
                name: "IX_Verse_HymnId",
                table: "Verse",
                column: "HymnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Bridge_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Chorus_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropForeignKey(
                name: "FK_LyricSegments_Verse_VerseId",
                table: "LyricSegments");

            migrationBuilder.DropTable(
                name: "Bridge");

            migrationBuilder.DropTable(
                name: "Chorus");

            migrationBuilder.DropTable(
                name: "Verse");

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropIndex(
                name: "IX_LyricSegments_VerseId",
                table: "LyricSegments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BridgeId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "ChorusId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "VerseId",
                table: "LyricSegments");

            migrationBuilder.DropColumn(
                name: "TextFilePath",
                table: "Hymns");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "HymnId",
                table: "LyricSegments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "History",
                table: "Hymns",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedBy",
                table: "Hymns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_LyricSegments_HymnId",
                table: "LyricSegments",
                column: "HymnId");

            migrationBuilder.AddForeignKey(
                name: "FK_LyricSegments_Hymns_HymnId",
                table: "LyricSegments",
                column: "HymnId",
                principalTable: "Hymns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
