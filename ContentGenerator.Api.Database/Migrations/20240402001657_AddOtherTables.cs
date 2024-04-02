using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentGenerator.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WhatsApp",
                newName: "WhatsAppId");

            migrationBuilder.CreateTable(
                name: "Destinos",
                columns: table => new
                {
                    DestinosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinos", x => x.DestinosId);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "Humor",
                columns: table => new
                {
                    HumorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humor", x => x.HumorId);
                });

            migrationBuilder.CreateTable(
                name: "TipoAssunto",
                columns: table => new
                {
                    TipoAssuntoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assunto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAssunto", x => x.TipoAssuntoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoHomenagem",
                columns: table => new
                {
                    TipoHomenagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoHomenagem", x => x.TipoHomenagemId);
                });

            migrationBuilder.CreateTable(
                name: "TipoValidacao",
                columns: table => new
                {
                    TipoValidacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoValidacao", x => x.TipoValidacaoId);
                });

            migrationBuilder.CreateTable(
                name: "ContasEnvio",
                columns: table => new
                {
                    ContasEnvioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Configuracao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasEnvio", x => x.ContasEnvioId);
                    table.ForeignKey(
                        name: "FK_ContasEnvio_Destinos_DestinosId",
                        column: x => x.DestinosId,
                        principalTable: "Destinos",
                        principalColumn: "DestinosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assunto",
                columns: table => new
                {
                    AssuntoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoValidacaoId = table.Column<int>(type: "int", nullable: false),
                    HumorId = table.Column<int>(type: "int", nullable: false),
                    DestinosId = table.Column<int>(type: "int", nullable: false),
                    TipoAssuntoId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjEveAssunto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostOriginal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataValida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostValidado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataPublicacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImagemPost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncluirImg = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assunto", x => x.AssuntoId);
                    table.ForeignKey(
                        name: "FK_Assunto_Destinos_DestinosId",
                        column: x => x.DestinosId,
                        principalTable: "Destinos",
                        principalColumn: "DestinosId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assunto_Humor_HumorId",
                        column: x => x.HumorId,
                        principalTable: "Humor",
                        principalColumn: "HumorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assunto_TipoAssunto_TipoAssuntoId",
                        column: x => x.TipoAssuntoId,
                        principalTable: "TipoAssunto",
                        principalColumn: "TipoAssuntoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assunto_TipoValidacao_TipoValidacaoId",
                        column: x => x.TipoValidacaoId,
                        principalTable: "TipoValidacao",
                        principalColumn: "TipoValidacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homenagem",
                columns: table => new
                {
                    HomenagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinosId = table.Column<int>(type: "int", nullable: false),
                    HumorId = table.Column<int>(type: "int", nullable: false),
                    TipoValidacaoId = table.Column<int>(type: "int", nullable: false),
                    TipoHomenagemId = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    ObjEveAssunto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homenagem", x => x.HomenagemId);
                    table.ForeignKey(
                        name: "FK_Homenagem_Destinos_DestinosId",
                        column: x => x.DestinosId,
                        principalTable: "Destinos",
                        principalColumn: "DestinosId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homenagem_Humor_HumorId",
                        column: x => x.HumorId,
                        principalTable: "Humor",
                        principalColumn: "HumorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homenagem_TipoHomenagem_TipoHomenagemId",
                        column: x => x.TipoHomenagemId,
                        principalTable: "TipoHomenagem",
                        principalColumn: "TipoHomenagemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homenagem_TipoValidacao_TipoValidacaoId",
                        column: x => x.TipoValidacaoId,
                        principalTable: "TipoValidacao",
                        principalColumn: "TipoValidacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publicacao",
                columns: table => new
                {
                    PublicacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssuntoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacao", x => x.PublicacaoId);
                    table.ForeignKey(
                        name: "FK_Publicacao_Assunto_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assunto",
                        principalColumn: "AssuntoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assunto_DestinosId",
                table: "Assunto",
                column: "DestinosId");

            migrationBuilder.CreateIndex(
                name: "IX_Assunto_HumorId",
                table: "Assunto",
                column: "HumorId");

            migrationBuilder.CreateIndex(
                name: "IX_Assunto_TipoAssuntoId",
                table: "Assunto",
                column: "TipoAssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_Assunto_TipoValidacaoId",
                table: "Assunto",
                column: "TipoValidacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContasEnvio_DestinosId",
                table: "ContasEnvio",
                column: "DestinosId");

            migrationBuilder.CreateIndex(
                name: "IX_Homenagem_DestinosId",
                table: "Homenagem",
                column: "DestinosId");

            migrationBuilder.CreateIndex(
                name: "IX_Homenagem_HumorId",
                table: "Homenagem",
                column: "HumorId");

            migrationBuilder.CreateIndex(
                name: "IX_Homenagem_TipoHomenagemId",
                table: "Homenagem",
                column: "TipoHomenagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Homenagem_TipoValidacaoId",
                table: "Homenagem",
                column: "TipoValidacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacao_AssuntoId",
                table: "Publicacao",
                column: "AssuntoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContasEnvio");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Homenagem");

            migrationBuilder.DropTable(
                name: "Publicacao");

            migrationBuilder.DropTable(
                name: "TipoHomenagem");

            migrationBuilder.DropTable(
                name: "Assunto");

            migrationBuilder.DropTable(
                name: "Destinos");

            migrationBuilder.DropTable(
                name: "Humor");

            migrationBuilder.DropTable(
                name: "TipoAssunto");

            migrationBuilder.DropTable(
                name: "TipoValidacao");

            migrationBuilder.RenameColumn(
                name: "WhatsAppId",
                table: "WhatsApp",
                newName: "Id");
        }
    }
}
