using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class editinrel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productsDetials_products_ProductId",
                table: "productsDetials");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "productsDetials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_productsDetials_products_ProductId",
                table: "productsDetials",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productsDetials_products_ProductId",
                table: "productsDetials");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "productsDetials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productsDetials_products_ProductId",
                table: "productsDetials",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
