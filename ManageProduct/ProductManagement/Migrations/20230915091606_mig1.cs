using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblProduct",
                columns: table => new
                {
                    pId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pPrice = table.Column<int>(type: "int", nullable: false),
                    pRating = table.Column<int>(type: "int", nullable: false),
                    pStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProduct", x => x.pId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProduct");
        }
    }
}
