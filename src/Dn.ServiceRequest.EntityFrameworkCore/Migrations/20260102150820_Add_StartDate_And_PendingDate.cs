using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dn.ServiceRequest.Migrations
{
    /// <inheritdoc />
    public partial class Add_StartDate_And_PendingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ticket_id",
                table: "Comments",
                newName: "Ticket_Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "PendingDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PendingDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Ticket_Id",
                table: "Comments",
                newName: "Ticket_id");
        }
    }
}
