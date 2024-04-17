using Microsoft.EntityFrameworkCore.Migrations;

namespace DisasterResponseSystem.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InkindTypes_AffectedIndividualsRequests_AffectedIndividualsRequestsID",
                table: "InkindTypes");

            migrationBuilder.DropIndex(
                name: "IX_InkindTypes_AffectedIndividualsRequestsID",
                table: "InkindTypes");

            migrationBuilder.DropColumn(
                name: "AffectedIndividualsRequestsID",
                table: "InkindTypes");

            migrationBuilder.CreateTable(
                name: "AffectedIndividualsInkinds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QTY = table.Column<double>(type: "float", nullable: false),
                    AffectedIndividualsRequestsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffectedIndividualsInkinds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AffectedIndividualsInkinds_AffectedIndividualsRequests_AffectedIndividualsRequestsID",
                        column: x => x.AffectedIndividualsRequestsID,
                        principalTable: "AffectedIndividualsRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffectedIndividualsInkinds_AffectedIndividualsRequestsID",
                table: "AffectedIndividualsInkinds",
                column: "AffectedIndividualsRequestsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffectedIndividualsInkinds");

            migrationBuilder.AddColumn<int>(
                name: "AffectedIndividualsRequestsID",
                table: "InkindTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InkindTypes_AffectedIndividualsRequestsID",
                table: "InkindTypes",
                column: "AffectedIndividualsRequestsID");

            migrationBuilder.AddForeignKey(
                name: "FK_InkindTypes_AffectedIndividualsRequests_AffectedIndividualsRequestsID",
                table: "InkindTypes",
                column: "AffectedIndividualsRequestsID",
                principalTable: "AffectedIndividualsRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
