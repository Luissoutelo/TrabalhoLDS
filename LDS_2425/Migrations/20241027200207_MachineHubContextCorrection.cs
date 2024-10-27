using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class MachineHubContextCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Receipt_ReceiptId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesPage_Users_UserId",
                table: "FavoritesPage");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesPageMachine_Machines_MachineId",
                table: "FavoritesPageMachine");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Loan_Listings_Loan_ListingId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Machines_MachineId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Users_OwnerId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Users_UserId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_FavoritesPage_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartMachineConnections_FavoritesPage_FavoritesPageId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receipt",
                table: "Receipt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesPageMachine",
                table: "FavoritesPageMachine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesPage",
                table: "FavoritesPage");

            migrationBuilder.RenameTable(
                name: "Receipt",
                newName: "Receipts");

            migrationBuilder.RenameTable(
                name: "FavoritesPageMachine",
                newName: "FavoritesPageMachines");

            migrationBuilder.RenameTable(
                name: "FavoritesPage",
                newName: "FavoritesPages");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_UserId",
                table: "Receipts",
                newName: "IX_Receipts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_OwnerId",
                table: "Receipts",
                newName: "IX_Receipts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_MachineId",
                table: "Receipts",
                newName: "IX_Receipts_MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_Loan_ListingId",
                table: "Receipts",
                newName: "IX_Receipts_Loan_ListingId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesPageMachine_MachineId",
                table: "FavoritesPageMachines",
                newName: "IX_FavoritesPageMachines_MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesPage_UserId",
                table: "FavoritesPages",
                newName: "IX_FavoritesPages_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receipts",
                table: "Receipts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesPageMachines",
                table: "FavoritesPageMachines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesPages",
                table: "FavoritesPages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FavoritesPageLoan_Listings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoritesPageId = table.Column<int>(type: "int", nullable: false),
                    Loan_ListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesPageLoan_Listings", x => x.Id);
                });

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
                name: "FK_ShoppingCartLoan_ListingConnections_FavoritesPages_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "FavoritesPageId",
                principalTable: "FavoritesPages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartMachineConnections_FavoritesPages_FavoritesPageId",
                table: "ShoppingCartMachineConnections",
                column: "FavoritesPageId",
                principalTable: "FavoritesPages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_ShoppingCartLoan_ListingConnections_FavoritesPages_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartMachineConnections_FavoritesPages_FavoritesPageId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropTable(
                name: "FavoritesPageLoan_Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receipts",
                table: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesPages",
                table: "FavoritesPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesPageMachines",
                table: "FavoritesPageMachines");

            migrationBuilder.RenameTable(
                name: "Receipts",
                newName: "Receipt");

            migrationBuilder.RenameTable(
                name: "FavoritesPages",
                newName: "FavoritesPage");

            migrationBuilder.RenameTable(
                name: "FavoritesPageMachines",
                newName: "FavoritesPageMachine");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_UserId",
                table: "Receipt",
                newName: "IX_Receipt_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_OwnerId",
                table: "Receipt",
                newName: "IX_Receipt_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_MachineId",
                table: "Receipt",
                newName: "IX_Receipt_MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_Loan_ListingId",
                table: "Receipt",
                newName: "IX_Receipt_Loan_ListingId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesPages_UserId",
                table: "FavoritesPage",
                newName: "IX_FavoritesPage_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesPageMachines_MachineId",
                table: "FavoritesPageMachine",
                newName: "IX_FavoritesPageMachine_MachineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receipt",
                table: "Receipt",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesPage",
                table: "FavoritesPage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesPageMachine",
                table: "FavoritesPageMachine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Receipt_ReceiptId",
                table: "Contracts",
                column: "ReceiptId",
                principalTable: "Receipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesPage_Users_UserId",
                table: "FavoritesPage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesPageMachine_Machines_MachineId",
                table: "FavoritesPageMachine",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Loan_Listings_Loan_ListingId",
                table: "Receipt",
                column: "Loan_ListingId",
                principalTable: "Loan_Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Machines_MachineId",
                table: "Receipt",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Users_OwnerId",
                table: "Receipt",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Users_UserId",
                table: "Receipt",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_FavoritesPage_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "FavoritesPageId",
                principalTable: "FavoritesPage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartMachineConnections_FavoritesPage_FavoritesPageId",
                table: "ShoppingCartMachineConnections",
                column: "FavoritesPageId",
                principalTable: "FavoritesPage",
                principalColumn: "Id");
        }
    }
}
