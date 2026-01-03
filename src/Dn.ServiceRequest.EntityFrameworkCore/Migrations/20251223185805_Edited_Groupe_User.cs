using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dn.ServiceRequest.Migrations
{
    /// <inheritdoc />
    public partial class Edited_Groupe_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_Receiver",
                table: "GroupeUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_Receiver",
                table: "GroupeUsers");
        }
    }
}
