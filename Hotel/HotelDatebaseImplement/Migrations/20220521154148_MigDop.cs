using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelDatebaseImplement.Migrations
{
    public partial class MigDop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "Conferences");

            migrationBuilder.RenameColumn(
                name: "DataOf",
                table: "Conferences",
                newName: "DateOf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOf",
                table: "Conferences",
                newName: "DataOf");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "Conferences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
