using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectEvent.Migrations
{
    /// <inheritdoc />
    public partial class withDevide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIgnore",
                table: "BudgetItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "BudgetItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CategoryBudgetRange",
                columns: table => new
                {
                    CategoryBudgetRangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    MinBudget = table.Column<double>(type: "float", nullable: false),
                    MaxBudget = table.Column<double>(type: "float", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBudgetRange", x => x.CategoryBudgetRangeID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryBudgetRange");

            migrationBuilder.DropColumn(
                name: "IsIgnore",
                table: "BudgetItem");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "BudgetItem");
        }
    }
}
