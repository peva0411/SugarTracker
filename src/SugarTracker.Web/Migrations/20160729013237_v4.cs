using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SugarTracker.Web.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Readings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RawReadingId",
                table: "Readings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Readings",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "RawReadingId",
                table: "Readings",
                nullable: false);
        }
    }
}
