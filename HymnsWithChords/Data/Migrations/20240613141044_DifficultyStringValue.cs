using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class DifficultyStringValue : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			

			// Change the column type to nvarchar with a maximum length of 50
			migrationBuilder.AlterColumn<string>(
				name: "Difficulty",
				table: "Chords",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int");

			// Update existing data from int to string values
			migrationBuilder.Sql(
				@"
				UPDATE Chords
				SET Difficulty = CASE
				WHEN Difficulty = '1' THEN 'Easy'
				WHEN Difficulty = '2' THEN 'Medium'
				WHEN Difficulty = '3' THEN 'Advanced'
				END;
				");

		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			

			// Revert the column type back to int
			migrationBuilder.AlterColumn<int>(
				name: "Difficulty",
				table: "Chords",
				type: "int",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			// From string back to int
			migrationBuilder.Sql(
				@"
				UPDATE Chords
				SET Difficulty = CASE
				WHEN Difficulty = 'Easy' THEN 1
				WHEN Difficulty = 'Medium' THEN 2
				WHEN Difficulty = 'Advanced' THEN 3
				END;
				"
				);

		}

	}
}
