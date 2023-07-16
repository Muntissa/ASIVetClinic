using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Common.Migrations
{
    /// <inheritdoc />
    public partial class ReceptionTypeToTreatmentState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Receptions");

            migrationBuilder.AddColumn<int>(
                name: "TreatmentState",
                table: "Receptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKV3T0KxXEmbmJqSS0jc6fhMnVy7USOIn40XB9CSgBdZO6otkThfaDwH67Uq4SK3PA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatmentState",
                table: "Receptions");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Receptions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELiPwYLG9xUK5+pxHFLQec++/s55z8j7HMROJQtEcqnWEqXQ7DKJRuLD/yi6kRAY9w==");
        }
    }
}
