using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class MakeUserIdMandatory : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_article_user_user_id",
            schema: "news",
            table: "article");

        migrationBuilder.AlterColumn<int>(
            name: "user_id",
            schema: "news",
            table: "article",
            type: "integer",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);

        migrationBuilder.AddForeignKey(
            name: "fk_article_user_user_id",
            schema: "news",
            table: "article",
            column: "user_id",
            principalSchema: "client",
            principalTable: "user",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_article_user_user_id",
            schema: "news",
            table: "article");

        migrationBuilder.AlterColumn<int>(
            name: "user_id",
            schema: "news",
            table: "article",
            type: "integer",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AddForeignKey(
            name: "fk_article_user_user_id",
            schema: "news",
            table: "article",
            column: "user_id",
            principalSchema: "client",
            principalTable: "user",
            principalColumn: "id");
    }
}
