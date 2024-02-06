using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KQApi.Migrations
{
    public partial class CityInfoDBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrationTypes",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registration_Type_Schools = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDNo = table.Column<double>(type: "float", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<double>(type: "float", nullable: false),
                    Teacher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<double>(type: "float", nullable: false),
                    LinkTitleNoMenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Agree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Confirm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<double>(type: "float", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AltSchool1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltSchool2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltTeacher1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltTeacher2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registration_type_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reshoum_hetsonee_bdekaa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeacherId = table.Column<double>(type: "float", nullable: false),
                    MithamId = table.Column<double>(type: "float", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registration_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationTypes");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
