using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKR_2._0.Migrations
{
    public partial class vacancyCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "Vacancy",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_EmployeeId1",
                table: "Vacancy",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancy_Employee_EmployeeId1",
                table: "Vacancy",
                column: "EmployeeId1",
                principalTable: "Employee",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancy_Employee_EmployeeId1",
                table: "Vacancy");

            migrationBuilder.DropIndex(
                name: "IX_Vacancy_EmployeeId1",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Vacancy");
        }
    }
}
