using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCollector.DAL.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class DataValueTimeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DataValues_Time",
                table: "DataValues",
                column: "Time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DataValues_Time",
                table: "DataValues");
        }
    }
}
