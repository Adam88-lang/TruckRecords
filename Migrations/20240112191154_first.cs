using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckRecords.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Tests_TestID",
                table: "TestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Trucks_TruckID",
                table: "TestResults");

            migrationBuilder.AlterColumn<int>(
                name: "TruckID",
                table: "TestResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TestID",
                table: "TestResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "TestResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateConducted",
                table: "TestResults",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "TestResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Tests_TestID",
                table: "TestResults",
                column: "TestID",
                principalTable: "Tests",
                principalColumn: "TestID");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Trucks_TruckID",
                table: "TestResults",
                column: "TruckID",
                principalTable: "Trucks",
                principalColumn: "TruckID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Tests_TestID",
                table: "TestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Trucks_TruckID",
                table: "TestResults");

            migrationBuilder.AlterColumn<int>(
                name: "TruckID",
                table: "TestResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TestID",
                table: "TestResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "TestResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateConducted",
                table: "TestResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "TestResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Tests_TestID",
                table: "TestResults",
                column: "TestID",
                principalTable: "Tests",
                principalColumn: "TestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Trucks_TruckID",
                table: "TestResults",
                column: "TruckID",
                principalTable: "Trucks",
                principalColumn: "TruckID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
