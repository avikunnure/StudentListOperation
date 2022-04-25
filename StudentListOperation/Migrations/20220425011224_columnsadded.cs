using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentListOperation.Migrations
{
    public partial class columnsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Marks",
                table: "StudentSubjects",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marks",
                table: "StudentSubjects");
        }
    }
}
