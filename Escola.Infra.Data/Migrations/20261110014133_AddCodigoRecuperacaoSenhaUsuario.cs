using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Escola.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCodigoRecuperacaoSenhaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativa",
                table: "Matriculas");

            migrationBuilder.CreateTable(
                name: "CodigosRecuperacaoSenha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    CodigoHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    CodigoSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiraEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JaFoiUsado = table.Column<bool>(type: "boolean", nullable: false),
                    TentativasRestantes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosRecuperacaoSenha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigosRecuperacaoSenha_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodigosRecuperacaoSenha_UsuarioId",
                table: "CodigosRecuperacaoSenha",
                column: "UsuarioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigosRecuperacaoSenha");

            migrationBuilder.AddColumn<bool>(
                name: "Ativa",
                table: "Matriculas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
