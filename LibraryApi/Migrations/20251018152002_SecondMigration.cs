using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GenreBooks_Name",
                table: "GenreBooks",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GenreBooks_Name",
                table: "GenreBooks");
        }
    }
}
