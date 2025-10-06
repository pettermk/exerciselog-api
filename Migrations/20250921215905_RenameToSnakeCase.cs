using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseLogApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Timeseries",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Timeseries",
                newName: "tags");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "Timeseries",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Dimension",
                table: "Timeseries",
                newName: "dimension");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Timeseries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Timeseries",
                newName: "date_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "Timeseries",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "tags",
                table: "Timeseries",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "Timeseries",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "dimension",
                table: "Timeseries",
                newName: "Dimension");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Timeseries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "date_time",
                table: "Timeseries",
                newName: "Timestamp");
        }
    }
}
