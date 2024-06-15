using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class HymnBookSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "HymnBooks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "HymnBooks");
        }
    }
}
