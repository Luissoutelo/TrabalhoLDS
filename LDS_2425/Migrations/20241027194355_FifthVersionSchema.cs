using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class FifthVersionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritesPageLoan_Listing");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "FavoritesPageLoan_Listing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoritesPageId = table.Column<int>(type: "int", nullable: false),
                    Loan_ListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesPageLoan_Listing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritesPageLoan_Listing_Loan_Listings_Loan_ListingId",
                        column: x => x.Loan_ListingId,
                        principalTable: "Loan_Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesPageLoan_Listing_Loan_ListingId",
                table: "FavoritesPageLoan_Listing",
                column: "Loan_ListingId");
        }
    }
}
