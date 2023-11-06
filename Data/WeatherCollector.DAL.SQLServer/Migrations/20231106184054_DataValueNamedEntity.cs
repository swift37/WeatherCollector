using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCollector.DAL.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class DataValueNamedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DataValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DataValues");
        }
    }
}
