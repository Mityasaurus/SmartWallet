using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWallet.Migrations
{
    public partial class TransactionTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_UserCardId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "UserCardId",
                table: "Transactions",
                newName: "SenderCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                newName: "IX_Transactions_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserCardId",
                table: "Transactions",
                newName: "IX_Transactions_SenderCardId");

            migrationBuilder.AddColumn<int>(
                name: "RecipientCardId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecipientCardId",
                table: "Transactions",
                column: "RecipientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecipientId",
                table: "Transactions",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_RecipientCardId",
                table: "Transactions",
                column: "RecipientCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_SenderCardId",
                table: "Transactions",
                column: "SenderCardId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_SenderId",
                table: "Transactions",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_RecipientCardId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_SenderCardId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_RecipientId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_SenderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecipientCardId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecipientId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecipientCardId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Transactions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SenderCardId",
                table: "Transactions",
                newName: "UserCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SenderId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SenderCardId",
                table: "Transactions",
                newName: "IX_Transactions_UserCardId");

            migrationBuilder.AddColumn<string>(
                name: "OperationType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
