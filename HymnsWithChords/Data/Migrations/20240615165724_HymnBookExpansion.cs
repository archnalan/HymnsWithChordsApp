using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HymnsWithChords.Data.Migrations
{
    /// <inheritdoc />
    public partial class HymnBookExpansion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "HymnBooks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedTime",
                table: "HymnBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "HymnBooks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "HymnBooks",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "HymnBooks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "HymnBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "HymnBooks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "HymnBooks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "AddedTime",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "HymnBooks");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "HymnBooks");
        }
    }
}
