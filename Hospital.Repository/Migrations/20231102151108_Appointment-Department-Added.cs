using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repo.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentDepartmentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Appointments");
        }
    }
}
