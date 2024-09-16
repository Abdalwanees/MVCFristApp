using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCFristApp.DAL.Data.Migrations
{
    public partial class AddRelationsEmployee_Department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "workForId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_workForId",
                table: "Employees",
                column: "workForId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_workForId",
                table: "Employees",
                column: "workForId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_workForId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_workForId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "workForId",
                table: "Employees");
        }
    }
}
