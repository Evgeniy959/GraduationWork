using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddClientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Dates",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Email",
                keyValue: "admin@mail.ru",
                column: "Password",
                value: "$2a$11$HT0hIdFxvBDBo7vzeOX.SuzNPJkZ5CepKJj2yTX2k95Wr5t/Um6H.");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_ClientId",
                table: "Dates",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dates_Clients_ClientId",
                table: "Dates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dates_Clients_ClientId",
                table: "Dates");

            migrationBuilder.DropIndex(
                name: "IX_Dates_ClientId",
                table: "Dates");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Dates");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Email",
                keyValue: "admin@mail.ru",
                column: "Password",
                value: "$2a$11$xVVAUM2iF45b22Qu7tli6O0.tuumqLvkkBc.XIPWvbdguwiLgbCYK");
        }
    }
}
