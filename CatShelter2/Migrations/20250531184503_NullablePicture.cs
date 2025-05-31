using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatShelter2.Migrations
{
    /// <inheritdoc />
    public partial class NullablePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Cats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UserViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfBirth = table.Column<int>(type: "int", nullable: false),
                    ArriveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatViewModel_UserViewModel_CarerId",
                        column: x => x.CarerId,
                        principalTable: "UserViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdoptionViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    AdoptionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionViewModel_CatViewModel_CatId",
                        column: x => x.CatId,
                        principalTable: "CatViewModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdoptionViewModel_UserViewModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionViewModel_CatId",
                table: "AdoptionViewModel",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionViewModel_UserId",
                table: "AdoptionViewModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CatViewModel_CarerId",
                table: "CatViewModel",
                column: "CarerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionViewModel");

            migrationBuilder.DropTable(
                name: "CatViewModel");

            migrationBuilder.DropTable(
                name: "UserViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Cats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
