using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multitenant.API.Migrations
{
    /// <inheritdoc />
    public partial class Estrategia03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                schema: "dbo",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "People",
                schema: "dbo",
                newName: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "People",
                newSchema: "dbo");
        }
    }
}
