using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddMissingCreatedColumnOnArticleTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "created",
            schema: "news",
            table: "article",
            type: "timestamp with time zone",
            nullable: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "created",
            schema: "news",
            table: "article");
    }
}
