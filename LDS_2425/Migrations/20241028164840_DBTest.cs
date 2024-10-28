using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class DBTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Loan_Listings_ListingId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Receipts_ReceiptId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesPageMachines_Machines_MachineId",
                table: "FavoritesPageMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesPages_Users_UserId",
                table: "FavoritesPages");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Listings_Categories_CategoryId",
                table: "Loan_Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Listings_Users_OwnerId",
                table: "Loan_Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Listings_Users_UserId",
                table: "Loan_Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Categories_CategoryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Loan_Listings_Loan_ListingId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Machines_MachineId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Users_OwnerId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Users_UserId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_Loan_Listings_Loan_ListingId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartMachineConnections_Machines_MachineId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartMachineConnections_MachineId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_Loan_ListingId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_Loan_ListingId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_MachineId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_OwnerId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Machines_CategoryId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Loan_Listings_CategoryId",
                table: "Loan_Listings");

            migrationBuilder.DropIndex(
                name: "IX_Loan_Listings_OwnerId",
                table: "Loan_Listings");

            migrationBuilder.DropIndex(
                name: "IX_Loan_Listings_UserId",
                table: "Loan_Listings");

            migrationBuilder.DropIndex(
                name: "IX_FavoritesPages_UserId",
                table: "FavoritesPages");

            migrationBuilder.DropIndex(
                name: "IX_FavoritesPageMachines_MachineId",
                table: "FavoritesPageMachines");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ListingId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ReceiptId",
                table: "Contracts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartMachineConnections_MachineId",
                table: "ShoppingCartMachineConnections",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_Loan_ListingId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "Loan_ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_Loan_ListingId",
                table: "Receipts",
                column: "Loan_ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_MachineId",
                table: "Receipts",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_OwnerId",
                table: "Receipts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_CategoryId",
                table: "Machines",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_Listings_CategoryId",
                table: "Loan_Listings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_Listings_OwnerId",
                table: "Loan_Listings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_Listings_UserId",
                table: "Loan_Listings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesPages_UserId",
                table: "FavoritesPages",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesPageMachines_MachineId",
                table: "FavoritesPageMachines",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ListingId",
                table: "Contracts",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ReceiptId",
                table: "Contracts",
                column: "ReceiptId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Loan_Listings_ListingId",
                table: "Contracts",
                column: "ListingId",
                principalTable: "Loan_Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Receipts_ReceiptId",
                table: "Contracts",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesPageMachines_Machines_MachineId",
                table: "FavoritesPageMachines",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesPages_Users_UserId",
                table: "FavoritesPages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Listings_Categories_CategoryId",
                table: "Loan_Listings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Listings_Users_OwnerId",
                table: "Loan_Listings",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Listings_Users_UserId",
                table: "Loan_Listings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Categories_CategoryId",
                table: "Machines",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Loan_Listings_Loan_ListingId",
                table: "Receipts",
                column: "Loan_ListingId",
                principalTable: "Loan_Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Machines_MachineId",
                table: "Receipts",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Users_OwnerId",
                table: "Receipts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Users_UserId",
                table: "Receipts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_Loan_Listings_Loan_ListingId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "Loan_ListingId",
                principalTable: "Loan_Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartMachineConnections_Machines_MachineId",
                table: "ShoppingCartMachineConnections",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
