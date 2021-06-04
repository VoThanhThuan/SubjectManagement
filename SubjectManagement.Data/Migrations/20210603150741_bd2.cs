using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectManagement.Data.Migrations
{
    public partial class bd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlan",
                table: "Subject",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlan",
                table: "Subject");
        }
    }
}
