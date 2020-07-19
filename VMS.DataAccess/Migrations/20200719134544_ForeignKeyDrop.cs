using Microsoft.EntityFrameworkCore.Migrations;

namespace VMS.DataAccess.Migrations
{
    public partial class ForeignKeyDrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Volunteers_VolunteerId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_VolunteerId",
                table: "Opportunities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_VolunteerId",
                table: "Opportunities",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Volunteers_VolunteerId",
                table: "Opportunities",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
