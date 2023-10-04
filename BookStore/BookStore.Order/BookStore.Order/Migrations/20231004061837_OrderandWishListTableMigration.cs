using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Order.Migrations
{
    public partial class OrderandWishListTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ordres",
                columns: table => new
                {
                    OrderID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    BookID = table.Column<long>(type: "bigint", nullable: false),
                    OrderQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordres", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    WishListID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.WishListID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordres");

            migrationBuilder.DropTable(
                name: "WishLists");
        }
    }
}
