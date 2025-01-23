using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstrutoraDesbravador.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "varchar(30)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(30)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "date", nullable: false),
                    StatusProjeto = table.Column<int>(type: "integer", nullable: false),
                    RiscoProjeto = table.Column<int>(type: "integer", nullable: false),
                    ResponsavelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projeto_Funcionario_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Funcionario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjetoFuncionarios",
                columns: table => new
                {
                    ProjetoId = table.Column<int>(type: "integer", nullable: false),
                    FuncionarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoFuncionarios", x => new { x.FuncionarioId, x.ProjetoId });
                    table.ForeignKey(
                        name: "FK_ProjetoFuncionarios_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjetoFuncionarios_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ResponsavelId",
                table: "Projeto",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoFuncionarios_ProjetoId",
                table: "ProjetoFuncionarios",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjetoFuncionarios");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Funcionario");
        }
    }
}
