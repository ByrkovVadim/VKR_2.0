using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VKR_2._0.Migrations
{
    public partial class vacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VacancyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    personId = table.Column<string>(type: "text", nullable: false),
                    vacancyId = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Person_personId",
                        column: x => x.personId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback_Vacancy_vacancyId",
                        column: x => x.vacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    personId = table.Column<string>(type: "text", nullable: false),
                    vacancyId = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Person_personId",
                        column: x => x.personId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_Vacancy_vacancyId",
                        column: x => x.vacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_personId",
                table: "Feedback",
                column: "personId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_vacancyId",
                table: "Feedback",
                column: "vacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_personId",
                table: "Invitation",
                column: "personId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_vacancyId",
                table: "Invitation",
                column: "vacancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Vacancy");
        }
    }
}
