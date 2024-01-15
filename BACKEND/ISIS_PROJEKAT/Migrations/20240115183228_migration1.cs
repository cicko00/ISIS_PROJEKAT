using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISIS_PROJEKAT.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoadDatasHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PTID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Load = table.Column<double>(type: "float", nullable: true),
                    isWeekend = table.Column<bool>(type: "bit", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    FeelsLike = table.Column<double>(type: "float", nullable: true),
                    Dew = table.Column<double>(type: "float", nullable: true),
                    Humidity = table.Column<double>(type: "float", nullable: true),
                    Precip = table.Column<double>(type: "float", nullable: true),
                    Snow = table.Column<double>(type: "float", nullable: true),
                    SnowDepth = table.Column<double>(type: "float", nullable: true),
                    WindGust = table.Column<double>(type: "float", nullable: true),
                    WindSpeed = table.Column<double>(type: "float", nullable: true),
                    WindDir = table.Column<double>(type: "float", nullable: true),
                    SeaLevelPressure = table.Column<double>(type: "float", nullable: true),
                    CloudCover = table.Column<double>(type: "float", nullable: true),
                    Visibilty = table.Column<double>(type: "float", nullable: true),
                    UVIndex = table.Column<double>(type: "float", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadDatasHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoadDatasPrediction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Load = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadDatasPrediction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WheatherForecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    FeelsLike = table.Column<double>(type: "float", nullable: true),
                    Dew = table.Column<double>(type: "float", nullable: true),
                    Humidity = table.Column<double>(type: "float", nullable: true),
                    Precip = table.Column<double>(type: "float", nullable: true),
                    Snow = table.Column<double>(type: "float", nullable: true),
                    SnowDepth = table.Column<double>(type: "float", nullable: true),
                    WindGust = table.Column<double>(type: "float", nullable: true),
                    WindSpeed = table.Column<double>(type: "float", nullable: true),
                    WindDir = table.Column<double>(type: "float", nullable: true),
                    SeaLevelPressure = table.Column<double>(type: "float", nullable: true),
                    CloudCover = table.Column<double>(type: "float", nullable: true),
                    Visibilty = table.Column<double>(type: "float", nullable: true),
                    UVIndex = table.Column<double>(type: "float", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WheatherForecasts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadDatasHistory");

            migrationBuilder.DropTable(
                name: "LoadDatasPrediction");

            migrationBuilder.DropTable(
                name: "WheatherForecasts");
        }
    }
}
