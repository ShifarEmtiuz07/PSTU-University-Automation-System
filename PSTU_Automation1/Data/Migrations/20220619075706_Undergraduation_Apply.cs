using Microsoft.EntityFrameworkCore.Migrations;

namespace PSTU_Automation1.Data.Migrations
{
    public partial class Undergraduation_Apply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Undergraduation_Apply",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Father_Name = table.Column<string>(nullable: true),
                    Mother_Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostCode = table.Column<int>(nullable: false),
                    Contact = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Undergraduation_Apply", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Undergraduation_Apply");
        }
    }
}
