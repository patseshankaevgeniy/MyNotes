using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTelegramEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "providers");

            migrationBuilder.CreateTable(
                name: "TelegramAuthCodes",
                schema: "providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortCode = table.Column<int>(type: "int", nullable: false),
                    ShortExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAuthCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageLogs",
                schema: "providers",
                columns: table => new
                {
                    TelegramMessageId = table.Column<int>(type: "int", nullable: false),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramUserId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBot = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageLogs", x => new { x.TelegramUserId, x.TelegramMessageId, x.TelegramChatId });
                });

            migrationBuilder.CreateTable(
                name: "TelegramSessions",
                schema: "providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUsers",
                schema: "providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalTelegramUserId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramUserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramUserLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramBotToken = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramSessionMessages",
                schema: "providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSessionMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramSessionMessages_TelegramSessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "providers",
                        principalTable: "TelegramSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotConfigurations",
                schema: "providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiveMemberPurchaseNotification = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveDaySumupNotification = table.Column<bool>(type: "bit", nullable: false),
                    DaySumupNotificationTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramBotConfigurations_TelegramUsers_TelegramUserId",
                        column: x => x.TelegramUserId,
                        principalSchema: "providers",
                        principalTable: "TelegramUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAuthCodes_ShortCode",
                schema: "providers",
                table: "TelegramAuthCodes",
                column: "ShortCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBotConfigurations_TelegramUserId",
                schema: "providers",
                table: "TelegramBotConfigurations",
                column: "TelegramUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSessionMessages_SessionId",
                schema: "providers",
                table: "TelegramSessionMessages",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUsers_UserId_OriginalTelegramUserId_TelegramBotToken",
                schema: "providers",
                table: "TelegramUsers",
                columns: new[] { "UserId", "OriginalTelegramUserId", "TelegramBotToken" },
                unique: true,
                filter: "[TelegramBotToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramAuthCodes",
                schema: "providers");

            migrationBuilder.DropTable(
                name: "TelegramBotConfigurations",
                schema: "providers");

            migrationBuilder.DropTable(
                name: "TelegramMessageLogs",
                schema: "providers");

            migrationBuilder.DropTable(
                name: "TelegramSessionMessages",
                schema: "providers");

            migrationBuilder.DropTable(
                name: "TelegramUsers",
                schema: "providers");

            migrationBuilder.DropTable(
                name: "TelegramSessions",
                schema: "providers");
        }
    }
}
