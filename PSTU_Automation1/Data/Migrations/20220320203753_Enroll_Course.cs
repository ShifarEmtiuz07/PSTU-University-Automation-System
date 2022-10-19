using Microsoft.EntityFrameworkCore.Migrations;

namespace PSTU_Automation1.Data.Migrations
{
    public partial class Enroll_Course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enroll_Course",
                columns: table => new
                {
                    EnrolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    InstructorName = table.Column<string>(nullable: true),
                    CourseTitle = table.Column<string>(nullable: true),
                    CourseCode = table.Column<string>(nullable: true),
                    CourseCradit = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enroll_Course", x => x.EnrolId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enroll_Course");
        }
    }
}
