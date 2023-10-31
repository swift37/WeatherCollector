using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCollector.DAL.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class DataOject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "DataValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataValues_ObjectId",
                table: "DataValues",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DataObjects_Name",
                table: "DataObjects",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_DataValues_DataObjects_ObjectId",
                table: "DataValues",
                column: "ObjectId",
                principalTable: "DataObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataValues_DataObjects_ObjectId",
                table: "DataValues");

            migrationBuilder.DropTable(
                name: "DataObjects");

            migrationBuilder.DropIndex(
                name: "IX_DataValues_ObjectId",
                table: "DataValues");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "DataValues");
        }
    }
}
