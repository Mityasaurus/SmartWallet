using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWallet.Migrations
{
    public partial class TransactionsRateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Transactions");
        }
    }
}
