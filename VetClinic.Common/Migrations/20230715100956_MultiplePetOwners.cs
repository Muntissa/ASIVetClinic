using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VetClinic.Common.Migrations
{
    /// <inheritdoc />
    public partial class MultiplePetOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Owners_OwnerId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_AspNetUsers_EmployeeId1",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_EmployeeId1",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Receptions");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Receptions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Receptions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Receptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Receptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Owners",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentDate",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Animals",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AnimalHospitalInfos",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AnimalHospitalInfos",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalOwner",
                columns: table => new
                {
                    AnimalsId = table.Column<int>(type: "integer", nullable: false),
                    OwnersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalOwner", x => new { x.AnimalsId, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_AnimalOwner_Animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalOwner_Owners_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-b815-455a-8908-8133983c9200", null, "Главный врач", "ГЛАВНЫЙ ВРАЧ" },
                    { "22222222-b815-455a-8908-8133983c9200", null, "Врач", "ВРАЧ" },
                    { "33333333-b815-455a-8908-8133983c9200", null, "Регистратор", "РЕГИСТРАТОР" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELeXtq8rdlSju21mOR07SjZ/2m+x7m7vji4gC5PFuOo+4rpiiOZoiuVAGP4ZQjJnuw==");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_AnimalId",
                table: "Receptions",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_EmployeeId",
                table: "Receptions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_OwnerId",
                table: "Receptions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalOwner_OwnersId",
                table: "AnimalOwner",
                column: "OwnersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_Animals_AnimalId",
                table: "Receptions",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_AspNetUsers_EmployeeId",
                table: "Receptions",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_Owners_OwnerId",
                table: "Receptions",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_Animals_AnimalId",
                table: "Receptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_AspNetUsers_EmployeeId",
                table: "Receptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_Owners_OwnerId",
                table: "Receptions");

            migrationBuilder.DropTable(
                name: "AnimalOwner");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_AnimalId",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_EmployeeId",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_OwnerId",
                table: "Receptions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-b815-455a-8908-8133983c9200");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-b815-455a-8908-8133983c9200");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-b815-455a-8908-8133983c9200");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Receptions");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Receptions");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Receptions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Receptions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "Receptions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Owners",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentDate",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Animals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Animals",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AnimalHospitalInfos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AnimalHospitalInfos",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEF3HmD2NPBkqRrubDHgY316XpjQQuD2RuaDUFmANkaS+9UWtgxOjjw4DmUjCUmXZ8w==");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_EmployeeId1",
                table: "Receptions",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Owners_OwnerId",
                table: "Animals",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_AspNetUsers_EmployeeId1",
                table: "Receptions",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
