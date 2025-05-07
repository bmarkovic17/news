using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NewsApp.Core.Domain.ArticleEntity;

#nullable disable

namespace NewsApp.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class CreateInitialModels : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "news");

        migrationBuilder.EnsureSchema(
            name: "client");

        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:news.article_status", "draft,published,unpublished");

        migrationBuilder.CreateSequence(
            name: "article_id_sequence",
            schema: "news",
            incrementBy: 10);

        migrationBuilder.CreateSequence(
            name: "user_id_sequence",
            schema: "client",
            incrementBy: 10);

        migrationBuilder.CreateTable(
            name: "user",
            schema: "client",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false),
                normalized_email = table.Column<string>(type: "text", nullable: true, computedColumnSql: "upper(email)", stored: true),
                email = table.Column<string>(type: "text", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                surname = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
                table.PrimaryKey("pk_user", x => x.id));

        migrationBuilder.CreateTable(
            name: "article",
            schema: "news",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false),
                status = table.Column<ArticleStatus>(type: "news.article_status", nullable: false),
                modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                published = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                user_id = table.Column<int>(type: "integer", nullable: true),
                content = table.Column<string>(type: "text", nullable: true),
                title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_article", x => x.id);
                table.ForeignKey(
                    name: "fk_article_user_user_id",
                    column: x => x.user_id,
                    principalSchema: "client",
                    principalTable: "user",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_article_user_id",
            schema: "news",
            table: "article",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_user_normalized_email",
            schema: "client",
            table: "user",
            column: "normalized_email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "article",
            schema: "news");

        migrationBuilder.DropTable(
            name: "user",
            schema: "client");

        migrationBuilder.DropSequence(
            name: "article_id_sequence",
            schema: "news");

        migrationBuilder.DropSequence(
            name: "user_id_sequence",
            schema: "client");
    }
}
