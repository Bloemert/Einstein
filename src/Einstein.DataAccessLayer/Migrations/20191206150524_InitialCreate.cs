using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Einstein.DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EffectiveStartedOn = table.Column<DateTime>(nullable: false),
                    EffectiveStartedBy = table.Column<Guid>(nullable: false),
                    EffectiveModifiedOn = table.Column<DateTime>(nullable: false),
                    EffectiveModifiedBy = table.Column<Guid>(nullable: false),
                    EffectiveEndedOn = table.Column<DateTime>(nullable: false),
                    EffectiveEndedBy = table.Column<Guid>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmployedSince = table.Column<DateTime>(nullable: true),
                    EmployeeNumber = table.Column<int>(nullable: false),
                    AvailabilityPerWeek = table.Column<int>(nullable: false),
                    FunctionLevel = table.Column<string>(nullable: true),
                    FunctionTitle = table.Column<string>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EffectiveStartedOn = table.Column<DateTime>(nullable: false),
                    EffectiveStartedBy = table.Column<Guid>(nullable: false),
                    EffectiveModifiedOn = table.Column<DateTime>(nullable: false),
                    EffectiveModifiedBy = table.Column<Guid>(nullable: false),
                    EffectiveEndedOn = table.Column<DateTime>(nullable: false),
                    EffectiveEndedBy = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    PasswordData = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    FailedAttempts = table.Column<int>(nullable: false),
                    GoodLogins = table.Column<int>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "EffectiveEndedBy", "EffectiveEndedOn", "EffectiveModifiedBy", "EffectiveModifiedOn", "EffectiveStartedBy", "EffectiveStartedOn", "Email", "EmployeeId", "ExpireDate", "FailedAttempts", "Firstname", "GoodLogins", "LastLogin", "Lastname", "Login", "PasswordData" },
                values: new object[] { new Guid("272ebb4f-6549-4ec2-a41e-5329a7450118"), true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(9999, 12, 31, 23, 59, 59, 997, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("272ebb4f-6549-4ec2-a41e-5329a7450118"), new DateTime(2019, 12, 6, 16, 5, 23, 986, DateTimeKind.Local).AddTicks(7368), "einstein@bloemert.com", null, null, 0, "Administrator", 0, null, "Bloemert", "admin", "8a8ab4ae3b063c19103ddf08ab7fda9ec118abc73fbb65c1cd12217adbdaed1d52e5db1337cbf0f275641494823c36e631ccc8ac353459177bd41718df0b56f6" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
