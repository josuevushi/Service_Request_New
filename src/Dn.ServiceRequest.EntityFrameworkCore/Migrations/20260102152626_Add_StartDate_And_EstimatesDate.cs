using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dn.ServiceRequest.Migrations
{
    /// <inheritdoc />
    public partial class Add_StartDate_And_EstimatesDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EstimateDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimateDate",
                table: "Tickets");
        }
    }
}
