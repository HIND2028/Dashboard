using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class editinrel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "damegedproducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_damegedproducts_ProductId",
                table: "damegedproducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_damegedproducts_products_ProductId",
                table: "damegedproducts",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_damegedproducts_products_ProductId",
                table: "damegedproducts");

            migrationBuilder.DropIndex(
                name: "IX_damegedproducts_ProductId",
                table: "damegedproducts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "damegedproducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
