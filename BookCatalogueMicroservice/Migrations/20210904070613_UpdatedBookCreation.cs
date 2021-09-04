using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookCatalogueMicroservice.Migrations
{
    public partial class UpdatedBookCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicationDate",
                table: "Books",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
