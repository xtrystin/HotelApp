using Microsoft.EntityFrameworkCore.Migrations;

namespace HAApi.Library.Migrations
{
    public partial class ExtractPaymentFromCheckInsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Rooms_RoomId",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_RoomId",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "CheckIns");

            migrationBuilder.AlterColumn<string>(
                name: "CashierId",
                table: "CheckOuts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CashierId",
                table: "CheckIns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "CheckIns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    Tax = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_PaymentId",
                table: "CheckIns",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Payment_PaymentId",
                table: "CheckIns",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Payment_PaymentId",
                table: "CheckIns");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_PaymentId",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "CheckIns");

            migrationBuilder.AlterColumn<int>(
                name: "CashierId",
                table: "CheckOuts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "CheckIns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CashierId",
                table: "CheckIns",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "CheckIns",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "CheckIns",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "CheckIns",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_RoomId",
                table: "CheckIns",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Rooms_RoomId",
                table: "CheckIns",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
