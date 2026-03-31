using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VineyardManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatabaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vineyards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ClimateLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VineyardId = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    Rainfall = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClimateLogs_Vineyards_VineyardId",
                        column: x => x.VineyardId,
                        principalTable: "Vineyards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrapeVarieties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeVarieties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VineyardId = table.Column<int>(type: "int", nullable: false),
                    GrapeVarietyId = table.Column<int>(type: "int", nullable: false),
                    AreaSize = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plots_GrapeVarieties_GrapeVarietyId",
                        column: x => x.GrapeVarietyId,
                        principalTable: "GrapeVarieties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plots_Vineyards_VineyardId",
                        column: x => x.VineyardId,
                        principalTable: "Vineyards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlotId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldActivities_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Harvests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlotId = table.Column<int>(type: "int", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityKG = table.Column<double>(type: "float", nullable: false),
                    SugarContent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harvests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Harvests_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClimateLogs_VineyardId",
                table: "ClimateLogs",
                column: "VineyardId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldActivities_PlotId",
                table: "FieldActivities",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvests_PlotId",
                table: "Harvests",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_GrapeVarietyId",
                table: "Plots",
                column: "GrapeVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_VineyardId",
                table: "Plots",
                column: "VineyardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimateLogs");

            migrationBuilder.DropTable(
                name: "FieldActivities");

            migrationBuilder.DropTable(
                name: "Harvests");

            migrationBuilder.DropTable(
                name: "Plots");

            migrationBuilder.DropTable(
                name: "GrapeVarieties");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vineyards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
