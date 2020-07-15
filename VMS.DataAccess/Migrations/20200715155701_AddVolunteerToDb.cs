using Microsoft.EntityFrameworkCore.Migrations;

namespace VMS.DataAccess.Migrations
{
    public partial class AddVolunteerToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 25, nullable: false),
                    UserName = table.Column<string>(maxLength: 25, nullable: false),
                    Password = table.Column<string>(maxLength: 25, nullable: false),
                    VolunteerPrefersCenter = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    Availability = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    HomePhone = table.Column<string>(nullable: true),
                    WorkPhone = table.Column<string>(nullable: true),
                    CellPhone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    License = table.Column<string>(nullable: true),
                    EmergencyContactName = table.Column<string>(nullable: false),
                    EmergencyContactHomePhone = table.Column<string>(nullable: true),
                    EmergencyContactCellPhone = table.Column<string>(nullable: false),
                    EmergencyContactEmail = table.Column<string>(nullable: true),
                    IsDriversLicenseOnFile = table.Column<bool>(nullable: false),
                    IsSsCardOnFile = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ApprovalStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteers");
        }
    }
}
