using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dn.ServiceRequest.Migrations
{
    /// <inheritdoc />
    public partial class Edited_Ticket_Entity_Add_ClosureDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosureDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosureDate",
                table: "Tickets");
        }
    }
}
