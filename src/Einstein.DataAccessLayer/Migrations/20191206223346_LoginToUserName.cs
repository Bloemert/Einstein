using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Einstein.DataAccessLayer.Migrations
{
    public partial class LoginToUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("272ebb4f-6549-4ec2-a41e-5329a7450118"));

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "EffectiveEndedBy", "EffectiveEndedOn", "EffectiveModifiedBy", "EffectiveModifiedOn", "EffectiveStartedBy", "EffectiveStartedOn", "Email", "EmployeeId", "ExpireDate", "FailedAttempts", "Firstname", "GoodLogins", "LastLogin", "Lastname", "PasswordData", "UserName" },
                values: new object[] { new Guid("3593c7d7-43b2-4dd1-a900-3618e29d5ffa"), true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(9999, 12, 31, 23, 59, 59, 997, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3593c7d7-43b2-4dd1-a900-3618e29d5ffa"), new DateTime(2019, 12, 6, 23, 33, 45, 641, DateTimeKind.Local).AddTicks(1637), "einstein@bloemert.com", null, null, 0, "Administrator", 0, null, "Bloemert", "8a8ab4ae3b063c19103ddf08ab7fda9ec118abc73fbb65c1cd12217adbdaed1d52e5db1337cbf0f275641494823c36e631ccc8ac353459177bd41718df0b56f6", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3593c7d7-43b2-4dd1-a900-3618e29d5ffa"));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "EffectiveEndedBy", "EffectiveEndedOn", "EffectiveModifiedBy", "EffectiveModifiedOn", "EffectiveStartedBy", "EffectiveStartedOn", "Email", "EmployeeId", "ExpireDate", "FailedAttempts", "Firstname", "GoodLogins", "LastLogin", "Lastname", "Login", "PasswordData" },
                values: new object[] { new Guid("272ebb4f-6549-4ec2-a41e-5329a7450118"), true, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(9999, 12, 31, 23, 59, 59, 997, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("272ebb4f-6549-4ec2-a41e-5329a7450118"), new DateTime(2019, 12, 6, 16, 5, 23, 986, DateTimeKind.Local).AddTicks(7368), "einstein@bloemert.com", null, null, 0, "Administrator", 0, null, "Bloemert", "admin", "8a8ab4ae3b063c19103ddf08ab7fda9ec118abc73fbb65c1cd12217adbdaed1d52e5db1337cbf0f275641494823c36e631ccc8ac353459177bd41718df0b56f6" });
        }
    }
}
