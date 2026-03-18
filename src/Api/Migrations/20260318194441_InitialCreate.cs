using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServiceName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessageApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SentBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessageApi_ChatApi_ChatId",
                        column: x => x.ChatId,
                        principalTable: "ChatApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OnSale = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArtworkName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtworkApi_ServiceApi_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtworkApi_UserApi_UserId",
                        column: x => x.UserId,
                        principalTable: "UserApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatInviteApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SentBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatInviteApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatInviteApi_ChatApi_ChatId",
                        column: x => x.ChatId,
                        principalTable: "ChatApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatInviteApi_UserApi_UserId",
                        column: x => x.UserId,
                        principalTable: "UserApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatUserApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUserApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUserApi_ChatApi_ChatId",
                        column: x => x.ChatId,
                        principalTable: "ChatApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatUserApi_UserApi_UserId",
                        column: x => x.UserId,
                        principalTable: "UserApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvidedServiceApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidedServiceApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidedServiceApi_ServiceApi_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvidedServiceApi_UserApi_UserId",
                        column: x => x.UserId,
                        principalTable: "UserApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkCommentApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkCommentApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtworkCommentApi_ArtworkApi_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "ArtworkApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtworkCommentApi_UserApi_UserId",
                        column: x => x.UserId,
                        principalTable: "UserApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkLikeApi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkLikeApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtworkLikeApi_ArtworkApi_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "ArtworkApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtworkLikeApi_UserApi_UserId",
                        column: x => x.UserId,
                        principalTable: "UserApi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkApi_Id",
                table: "ArtworkApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkApi_ServiceId",
                table: "ArtworkApi",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkApi_UserId",
                table: "ArtworkApi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkCommentApi_ArtworkId",
                table: "ArtworkCommentApi",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkCommentApi_Id",
                table: "ArtworkCommentApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkCommentApi_UserId",
                table: "ArtworkCommentApi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkLikeApi_ArtworkId",
                table: "ArtworkLikeApi",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkLikeApi_Id",
                table: "ArtworkLikeApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkLikeApi_UserId",
                table: "ArtworkLikeApi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatApi_Id",
                table: "ChatApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatInviteApi_ChatId",
                table: "ChatInviteApi",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatInviteApi_Id",
                table: "ChatInviteApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatInviteApi_UserId",
                table: "ChatInviteApi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageApi_ChatId",
                table: "ChatMessageApi",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageApi_Id",
                table: "ChatMessageApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserApi_ChatId",
                table: "ChatUserApi",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserApi_Id",
                table: "ChatUserApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserApi_UserId",
                table: "ChatUserApi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedServiceApi_Id",
                table: "ProvidedServiceApi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedServiceApi_ServiceId",
                table: "ProvidedServiceApi",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedServiceApi_UserId",
                table: "ProvidedServiceApi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceApi_Id",
                table: "ServiceApi",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtworkCommentApi");

            migrationBuilder.DropTable(
                name: "ArtworkLikeApi");

            migrationBuilder.DropTable(
                name: "ChatInviteApi");

            migrationBuilder.DropTable(
                name: "ChatMessageApi");

            migrationBuilder.DropTable(
                name: "ChatUserApi");

            migrationBuilder.DropTable(
                name: "ProvidedServiceApi");

            migrationBuilder.DropTable(
                name: "ArtworkApi");

            migrationBuilder.DropTable(
                name: "ChatApi");

            migrationBuilder.DropTable(
                name: "ServiceApi");

            migrationBuilder.DropTable(
                name: "UserApi");
        }
    }
}
