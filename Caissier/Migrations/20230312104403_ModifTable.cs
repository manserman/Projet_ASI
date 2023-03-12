using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetASI.Migrations
{
    public partial class ModifTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "servie",
                table: "Commandes",
                newName: "isServed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isServed",
                table: "Commandes",
                newName: "servie");
        }
    }
}
