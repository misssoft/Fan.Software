using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Software.Data.Migrations
{
    public partial class Removetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestioinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    QuestionMessage = table.Column<string>(nullable: true),
                    QuestionSummary = table.Column<string>(nullable: true),
                    QuestionerId = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestioinId);
                    table.ForeignKey(
                        name: "FK_Questions_AspNetUsers_QuestionerId",
                        column: x => x.QuestionerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerMessage = table.Column<string>(nullable: true),
                    AnswererId = table.Column<string>(nullable: true),
                    QuestionQuestioinId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_AnswererId",
                        column: x => x.AnswererId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionQuestioinId",
                        column: x => x.QuestionQuestioinId,
                        principalTable: "Questions",
                        principalColumn: "QuestioinId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AnswererId",
                table: "Answers",
                column: "AnswererId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionQuestioinId",
                table: "Answers",
                column: "QuestionQuestioinId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionerId",
                table: "Questions",
                column: "QuestionerId");
        }
    }
}
