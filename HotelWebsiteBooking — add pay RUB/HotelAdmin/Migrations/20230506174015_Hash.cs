using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAdmin.Migrations
{
    /// <inheritdoc />
    public partial class Hash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Email",
                keyValue: "admin@bk.ru");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin@mail.ru", "$2a$11$xVVAUM2iF45b22Qu7tli6O0.tuumqLvkkBc.XIPWvbdguwiLgbCYK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Email",
                keyValue: "admin@mail.ru");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin@bk.ru", "admin" });
        }
    }
}
