using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectEvent.Migrations
{
    /// <inheritdoc />
    public partial class afterRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorAttribute_Vendors_VendorId",
                table: "VendorAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorAttribute",
                table: "VendorAttribute");

            migrationBuilder.RenameTable(
                name: "VendorAttribute",
                newName: "VendorAttributes");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "VendorAttributes",
                newName: "VendorAttributeName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VendorAttributes",
                newName: "VendorAttributeID");

            migrationBuilder.RenameIndex(
                name: "IX_VendorAttribute_VendorId",
                table: "VendorAttributes",
                newName: "IX_VendorAttributes_VendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorAttributes",
                table: "VendorAttributes",
                column: "VendorAttributeID");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    EventTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.EventTypeID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAttributes_Vendors_VendorId",
                table: "VendorAttributes",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorAttributes_Vendors_VendorId",
                table: "VendorAttributes");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorAttributes",
                table: "VendorAttributes");

            migrationBuilder.RenameTable(
                name: "VendorAttributes",
                newName: "VendorAttribute");

            migrationBuilder.RenameColumn(
                name: "VendorAttributeName",
                table: "VendorAttribute",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "VendorAttributeID",
                table: "VendorAttribute",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_VendorAttributes_VendorId",
                table: "VendorAttribute",
                newName: "IX_VendorAttribute_VendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorAttribute",
                table: "VendorAttribute",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAttribute_Vendors_VendorId",
                table: "VendorAttribute",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
