using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MinimumFund = table.Column<decimal>(type: "money", nullable: false),
                    MaximumFund = table.Column<decimal>(type: "money", nullable: false),
                    InsuranceCoefficient = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InquiryCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InquiryId = table.Column<int>(type: "int", nullable: false),
                    CoverageId = table.Column<int>(type: "int", nullable: false),
                    RequestedFund = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryCoverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryCoverages_Coverages_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryCoverages_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coverages",
                columns: new[] { "Id", "Description", "InsuranceCoefficient", "MaximumFund", "MinimumFund", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Surgical Coverage", 0.0052, 500000000m, 5000m, "Surgical", 1 },
                    { 2, "Dental Coverage", 0.0042, 400000000m, 4000m, "Dental", 2 },
                    { 3, "Hospitalization Coverage", 0.005, 200000000m, 2000m, "Hospitalization", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquiryCoverages_CoverageId",
                table: "InquiryCoverages",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryCoverages_InquiryId",
                table: "InquiryCoverages",
                column: "InquiryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InquiryCoverages");

            migrationBuilder.DropTable(
                name: "Coverages");

            migrationBuilder.DropTable(
                name: "Inquiries");
        }
    }
}
