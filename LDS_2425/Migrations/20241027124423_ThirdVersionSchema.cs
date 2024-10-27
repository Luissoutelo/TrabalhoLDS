using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class ThirdVersionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoritesPageId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavoritesPageId",
                table: "ShoppingCartMachineConnections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoritesPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritesPage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "FavoritesPageMachine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoritesPageId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesPageMachine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritesPageMachine_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartMachineConnections_FavoritesPageId",
                table: "ShoppingCartMachineConnections",
                column: "FavoritesPageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "FavoritesPageId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesPage_UserId",
                table: "FavoritesPage",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesPageLoan_Listing_Loan_ListingId",
                table: "FavoritesPageLoan_Listing",
                column: "Loan_ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesPageMachine_MachineId",
                table: "FavoritesPageMachine",
                column: "MachineId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_FavoritesPage_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartMachineConnections_FavoritesPage_FavoritesPageId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropTable(
                name: "FavoritesPage");

            migrationBuilder.DropTable(
                name: "FavoritesPageLoan_Listing");

            migrationBuilder.DropTable(
                name: "FavoritesPageMachine");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartMachineConnections_FavoritesPageId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropColumn(
                name: "FavoritesPageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoritesPageId",
                table: "ShoppingCartMachineConnections");

            migrationBuilder.DropColumn(
                name: "FavoritesPageId",
                table: "ShoppingCartLoan_ListingConnections");
        }
    }
}
