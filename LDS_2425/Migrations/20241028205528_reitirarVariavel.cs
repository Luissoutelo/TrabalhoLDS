using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDS_2425.Migrations
{
    /// <inheritdoc />
    public partial class reitirarVariavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "ShoppingCartId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartLoan_ListingConnections_ShoppingCartId",
                table: "ShoppingCartLoan_ListingConnections",
                column: "ShoppingCartId");
        }
    }
}
