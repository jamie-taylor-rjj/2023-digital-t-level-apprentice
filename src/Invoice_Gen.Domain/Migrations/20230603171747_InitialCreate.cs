using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Invoice_Gen.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false),
                    ClientAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ContactName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "ClientAddress", "ClientName", "ContactEmail", "ContactName" },
                values: new object[,]
                {
                    { 1, "4509 Clement Street, Atlanta, GA 30331", "Muller Inc", "AaronDBaker@dayrep.com", "Bradley S Crooks" },
                    { 2, "2545 James Street, Fairport, NY 14450", "Gutkowski Inc", "AnnaLWitt@teleworm.us", "Anna L. Witt" },
                    { 3, "923 Euclid Avenue, San Luis Obispo, CA 93401", "Hoeger - Gislason", "SusanMRailey@armyspy.com", "Susan Railey" },
                    { 4, "2607 Goldcliff Circle, Washington, DC 20005", "Toy Group", "StanleyDRogers@dayrep.com", "Stanley D. Rogers" },
                    { 5, "1381 Monroe Street, Houston, TX 77030", "Upton, Gleason and Cronin", "TammyWFinley@dayrep.com", "Tammy W. Finley" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
