using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectEvent.Migrations
{
    /// <inheritdoc />
    public partial class afterservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_Events_AllEventEventID",
                table: "BudgetItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_Vendors_VendorID",
                table: "BudgetItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Events_AllEventEventID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Vendors_VendorID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorAttributes_Vendors_VendorId",
                table: "VendorAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AllEventEventID",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorAttributes",
                table: "VendorAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetItems",
                table: "BudgetItems");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_AllEventEventID",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "AllEventEventID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "WorkingArea",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AllEventEventID",
                table: "BudgetItems");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "Vendor");

            migrationBuilder.RenameTable(
                name: "VendorAttributes",
                newName: "VendorAttribute");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "BudgetItems",
                newName: "BudgetItem");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "VendorAttribute",
                newName: "VendorID");

            migrationBuilder.RenameIndex(
                name: "IX_VendorAttributes_VendorId",
                table: "VendorAttribute",
                newName: "IX_VendorAttribute_VendorID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_UserID",
                table: "Event",
                newName: "IX_Event_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetItems_VendorID",
                table: "BudgetItem",
                newName: "IX_BudgetItem_VendorID");

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AreaID",
                table: "Vendor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Vendor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VendorID",
                table: "BudgetItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                table: "BudgetItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "VendorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorAttribute",
                table: "VendorAttribute",
                column: "VendorAttributeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetItem",
                table: "BudgetItem",
                column: "BudgetItemID");

            migrationBuilder.CreateTable(
                name: "EventVendors",
                columns: table => new
                {
                    EventsEventID = table.Column<int>(type: "int", nullable: false),
                    VendorsVendorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventVendors", x => new { x.EventsEventID, x.VendorsVendorID });
                    table.ForeignKey(
                        name: "FK_EventVendors_Event_EventsEventID",
                        column: x => x.EventsEventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventVendors_Vendor_VendorsVendorID",
                        column: x => x.VendorsVendorID,
                        principalTable: "Vendor",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EventID",
                table: "Tasks",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_AreaID",
                table: "Vendor",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CategoryID",
                table: "Vendor",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventTypeID",
                table: "Event",
                column: "EventTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItem_CategoryID",
                table: "BudgetItem",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItem_EventID",
                table: "BudgetItem",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventVendors_VendorsVendorID",
                table: "EventVendors",
                column: "VendorsVendorID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItem_Category_CategoryID",
                table: "BudgetItem",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItem_Event_EventID",
                table: "BudgetItem",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItem_Vendor_VendorID",
                table: "BudgetItem",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "VendorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventType_EventTypeID",
                table: "Event",
                column: "EventTypeID",
                principalTable: "EventType",
                principalColumn: "EventTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_User_UserID",
                table: "Event",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Event_EventID",
                table: "Tasks",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Vendor_VendorID",
                table: "Tasks",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Area_AreaID",
                table: "Vendor",
                column: "AreaID",
                principalTable: "Area",
                principalColumn: "AreaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Category_CategoryID",
                table: "Vendor",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAttribute_Vendor_VendorID",
                table: "VendorAttribute",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItem_Category_CategoryID",
                table: "BudgetItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItem_Event_EventID",
                table: "BudgetItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItem_Vendor_VendorID",
                table: "BudgetItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventType_EventTypeID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_User_UserID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Event_EventID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Vendor_VendorID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Area_AreaID",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Category_CategoryID",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorAttribute_Vendor_VendorID",
                table: "VendorAttribute");

            migrationBuilder.DropTable(
                name: "EventVendors");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EventID",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorAttribute",
                table: "VendorAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_AreaID",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CategoryID",
                table: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventTypeID",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetItem",
                table: "BudgetItem");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItem_CategoryID",
                table: "BudgetItem");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItem_EventID",
                table: "BudgetItem");

            migrationBuilder.DropColumn(
                name: "AreaID",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.RenameTable(
                name: "VendorAttribute",
                newName: "VendorAttributes");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "Vendors");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameTable(
                name: "BudgetItem",
                newName: "BudgetItems");

            migrationBuilder.RenameColumn(
                name: "VendorID",
                table: "VendorAttributes",
                newName: "VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_VendorAttribute_VendorID",
                table: "VendorAttributes",
                newName: "IX_VendorAttributes_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_UserID",
                table: "Events",
                newName: "IX_Events_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetItem_VendorID",
                table: "BudgetItems",
                newName: "IX_BudgetItems_VendorID");

            migrationBuilder.AlterColumn<string>(
                name: "EventID",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AllEventEventID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkingArea",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "VendorID",
                table: "BudgetItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventID",
                table: "BudgetItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AllEventEventID",
                table: "BudgetItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorAttributes",
                table: "VendorAttributes",
                column: "VendorAttributeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "VendorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetItems",
                table: "BudgetItems",
                column: "BudgetItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AllEventEventID",
                table: "Tasks",
                column: "AllEventEventID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_AllEventEventID",
                table: "BudgetItems",
                column: "AllEventEventID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_Events_AllEventEventID",
                table: "BudgetItems",
                column: "AllEventEventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_Vendors_VendorID",
                table: "BudgetItems",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Events_AllEventEventID",
                table: "Tasks",
                column: "AllEventEventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Vendors_VendorID",
                table: "Tasks",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorAttributes_Vendors_VendorId",
                table: "VendorAttributes",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
