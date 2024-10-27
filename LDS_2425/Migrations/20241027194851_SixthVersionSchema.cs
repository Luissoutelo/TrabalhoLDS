using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class SixthVersionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Loan_Listings_Loan_ListingId1",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Machines_MachineId1",
                table: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_Loan_ListingId1",
                table: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_MachineId1",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "Loan_ListingId1",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "MachineId1",
                table: "Receipt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Loan_ListingId1",
                table: "Receipt",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineId1",
                table: "Receipt",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_Loan_ListingId1",
                table: "Receipt",
                column: "Loan_ListingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_MachineId1",
                table: "Receipt",
                column: "MachineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Loan_Listings_Loan_ListingId1",
                table: "Receipt",
                column: "Loan_ListingId1",
                principalTable: "Loan_Listings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Machines_MachineId1",
                table: "Receipt",
                column: "MachineId1",
                principalTable: "Machines",
                principalColumn: "Id");
        }
    }
}
