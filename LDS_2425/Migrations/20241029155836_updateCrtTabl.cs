using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class updateCrtTabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "ShoppingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "ShoppingCartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartLoan_ListingConnections_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
