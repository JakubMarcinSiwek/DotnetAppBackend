using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinPro.Migrations
{
    /// <inheritdoc />
    public partial class migracja1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id_Place = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id_Place);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id_Role = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id_Role);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id_Trip = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Objective = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id_Trip);
                });

            migrationBuilder.CreateTable(
                name: "Bees",
                columns: table => new
                {
                    Id_Bee = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nickname = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Id_Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bees", x => x.Id_Bee);
                    table.ForeignKey(
                        name: "FK_Bees_Roles_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Roles",
                        principalColumn: "Id_Role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacesOnTrip",
                columns: table => new
                {
                    Id_Trip = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_Place = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacesOnTrip", x => new { x.Id_Trip, x.Id_Place });
                    table.ForeignKey(
                        name: "FK_PlacesOnTrip_Places_Id_Place",
                        column: x => x.Id_Place,
                        principalTable: "Places",
                        principalColumn: "Id_Place",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacesOnTrip_Trips_Id_Trip",
                        column: x => x.Id_Trip,
                        principalTable: "Trips",
                        principalColumn: "Id_Trip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeesOnTrip",
                columns: table => new
                {
                    Id_Bee = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_Trip = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeesOnTrip", x => new { x.Id_Bee, x.Id_Trip });
                    table.ForeignKey(
                        name: "FK_BeesOnTrip_Bees_Id_Bee",
                        column: x => x.Id_Bee,
                        principalTable: "Bees",
                        principalColumn: "Id_Bee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeesOnTrip_Trips_Id_Trip",
                        column: x => x.Id_Trip,
                        principalTable: "Trips",
                        principalColumn: "Id_Trip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bees_Id_Role",
                table: "Bees",
                column: "Id_Role");

            migrationBuilder.CreateIndex(
                name: "IX_BeesOnTrip_Id_Trip",
                table: "BeesOnTrip",
                column: "Id_Trip");

            migrationBuilder.CreateIndex(
                name: "IX_PlacesOnTrip_Id_Place",
                table: "PlacesOnTrip",
                column: "Id_Place");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeesOnTrip");

            migrationBuilder.DropTable(
                name: "PlacesOnTrip");

            migrationBuilder.DropTable(
                name: "Bees");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
