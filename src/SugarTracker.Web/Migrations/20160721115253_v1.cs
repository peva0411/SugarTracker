using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SugarTracker.Web.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawReadings",
                columns: table => new
                {
                    RawReadingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromPhoneNumber = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    ReadingTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawReadings", x => x.RawReadingId);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    ReadingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(nullable: true),
                    RawReadingId = table.Column<int>(nullable: false),
                    ReadingTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.ReadingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawReadings");

            migrationBuilder.DropTable(
                name: "Readings");
        }
    }
}
