using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWallet.Migrations
{
    public partial class TransactionsNamesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_RecipientCardId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_RecipientId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "Transactions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RecipientCardId",
                table: "Transactions",
                newName: "UserCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_RecipientId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_RecipientCardId",
                table: "Transactions",
                newName: "IX_Transactions_UserCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_UserCardId",
                table: "Transactions",
                column: "UserCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_UserCardId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "RecipientId");

            migrationBuilder.RenameColumn(
                name: "UserCardId",
                table: "Transactions",
                newName: "RecipientCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                newName: "IX_Transactions_RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserCardId",
                table: "Transactions",
                newName: "IX_Transactions_RecipientCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_RecipientCardId",
                table: "Transactions",
                column: "RecipientCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_RecipientId",
                table: "Transactions",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
