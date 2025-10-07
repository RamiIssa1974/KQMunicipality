using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KQApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "VTeachers");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Students",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRegisteredStudents",
                table: "VTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VTeachers",
                table: "VTeachers",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VTeachers",
                table: "VTeachers");

            migrationBuilder.DropColumn(
                name: "NumberOfRegisteredStudents",
                table: "VTeachers");

            migrationBuilder.RenameTable(
                name: "VTeachers",
                newName: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "id");
        }
    }
}
