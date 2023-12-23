using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_AspNetUsers_UserId",
                table: "Balance");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_AspNetUsers_UserId",
                table: "TransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionHistory",
                table: "TransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balance",
                table: "Balance");

            migrationBuilder.RenameTable(
                name: "TransactionHistory",
                newName: "TransactionHistories");

            migrationBuilder.RenameTable(
                name: "Balance",
                newName: "Balances");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistory_UserId",
                table: "TransactionHistories",
                newName: "IX_TransactionHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Balance_UserId",
                table: "Balances",
                newName: "IX_Balances_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionHistories",
                table: "TransactionHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balances",
                table: "Balances",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_AspNetUsers_UserId",
                table: "Balances",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_UserId",
                table: "TransactionHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_AspNetUsers_UserId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_UserId",
                table: "TransactionHistories");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionHistories",
                table: "TransactionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balances",
                table: "Balances");

            migrationBuilder.RenameTable(
                name: "TransactionHistories",
                newName: "TransactionHistory");

            migrationBuilder.RenameTable(
                name: "Balances",
                newName: "Balance");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistories_UserId",
                table: "TransactionHistory",
                newName: "IX_TransactionHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Balances_UserId",
                table: "Balance",
                newName: "IX_Balance_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionHistory",
                table: "TransactionHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balance",
                table: "Balance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_AspNetUsers_UserId",
                table: "Balance",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_AspNetUsers_UserId",
                table: "TransactionHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
