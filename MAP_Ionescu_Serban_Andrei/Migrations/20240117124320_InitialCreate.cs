using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAP_Ionescu_Serban_Andrei.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    oppositeTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minutesPlayed = table.Column<int>(type: "int", nullable: false),
                    markedPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalStats",
                columns: table => new
                {
                    PersonalStatsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    overallScore = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalStats", x => x.PersonalStatsID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    CoachID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalStatsID = table.Column<int>(type: "int", nullable: true),
                    CoachName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    debutYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.CoachID);
                    table.ForeignKey(
                        name: "FK_Coach_PersonalStats_PersonalStatsID",
                        column: x => x.PersonalStatsID,
                        principalTable: "PersonalStats",
                        principalColumn: "PersonalStatsID");
                });

            migrationBuilder.CreateTable(
                name: "Baller",
                columns: table => new
                {
                    BallerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BallerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: true),
                    TShirtNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baller", x => x.BallerID);
                    table.ForeignKey(
                        name: "FK_Baller_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlan",
                columns: table => new
                {
                    GamePlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BallerID = table.Column<int>(type: "int", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlan", x => x.GamePlanID);
                    table.ForeignKey(
                        name: "FK_GamePlan_Baller_BallerID",
                        column: x => x.BallerID,
                        principalTable: "Baller",
                        principalColumn: "BallerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlan_Coach_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coach",
                        principalColumn: "CoachID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    BallerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => new { x.BallerID, x.MatchID });
                    table.ForeignKey(
                        name: "FK_Position_Baller_BallerID",
                        column: x => x.BallerID,
                        principalTable: "Baller",
                        principalColumn: "BallerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Position_Match_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Match",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baller_TeamID",
                table: "Baller",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Coach_PersonalStatsID",
                table: "Coach",
                column: "PersonalStatsID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlan_BallerID",
                table: "GamePlan",
                column: "BallerID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlan_CoachID",
                table: "GamePlan",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Position_MatchID",
                table: "Position",
                column: "MatchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlan");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "Baller");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "PersonalStats");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
