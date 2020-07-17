using Microsoft.EntityFrameworkCore.Migrations;

namespace VMS.DataAccess.Migrations
{
    public partial class updateToModelsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerPrefersCenter",
                table: "Opportunities");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalStatus",
                table: "Volunteers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CenterType",
                table: "Opportunities",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterType",
                table: "Opportunities");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalStatus",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "VolunteerPrefersCenter",
                table: "Opportunities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
