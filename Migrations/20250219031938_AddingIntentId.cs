using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce2.Migrations
{
    /// <inheritdoc />
    public partial class AddingIntentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

          
        }
    }
}
