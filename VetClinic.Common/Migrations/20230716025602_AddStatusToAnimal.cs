using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Common.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELiPwYLG9xUK5+pxHFLQec++/s55z8j7HMROJQtEcqnWEqXQ7DKJRuLD/yi6kRAY9w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Animals");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIL8bsP9ki0uRZAbJikuyArgIR166lRAr1Z8HgvxBsgQizpNUTaaFR5obJLs0YdEDg==");
        }
    }
}
