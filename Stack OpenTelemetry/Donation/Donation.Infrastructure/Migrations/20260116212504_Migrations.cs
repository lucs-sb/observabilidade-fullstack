using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Donation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_donation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    donor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_donation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    donation_type = table.Column<int>(type: "integer", nullable: false),
                    volume_ml = table.Column<int>(type: "integer", nullable: false),
                    bag_number = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_donation", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_donation");
        }
    }
}
