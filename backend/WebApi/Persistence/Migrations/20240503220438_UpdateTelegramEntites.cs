using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTelegramEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaySumupNotificationTime",
                schema: "providers",
                table: "TelegramBotConfigurations");

            migrationBuilder.DropColumn(
                name: "ReceiveDaySumupNotification",
                schema: "providers",
                table: "TelegramBotConfigurations");

            migrationBuilder.RenameColumn(
                name: "ReceiveMemberPurchaseNotification",
                schema: "providers",
                table: "TelegramBotConfigurations",
                newName: "ReceiveMemberNotesNotification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveMemberNotesNotification",
                schema: "providers",
                table: "TelegramBotConfigurations",
                newName: "ReceiveMemberPurchaseNotification");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "DaySumupNotificationTime",
                schema: "providers",
                table: "TelegramBotConfigurations",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveDaySumupNotification",
                schema: "providers",
                table: "TelegramBotConfigurations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
