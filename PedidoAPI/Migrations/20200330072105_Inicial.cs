using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PedidoAPI.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Embalagems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    TempoPreparo = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embalagems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opcionais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    TempoAdd = table.Column<int>(nullable: false),
                    ValorAdd = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sabores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    AddTempo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sabores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TamanhoId = table.Column<int>(nullable: false),
                    SaborID = table.Column<int>(nullable: false),
                    ValorFinal = table.Column<double>(nullable: false),
                    Finalizado = table.Column<bool>(nullable: false),
                    TempoEspera = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Sabores_SaborID",
                        column: x => x.SaborID,
                        principalTable: "Sabores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Embalagems_TamanhoId",
                        column: x => x.TamanhoId,
                        principalTable: "Embalagems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpcionalPedido",
                columns: table => new
                {
                    PedidoId = table.Column<int>(nullable: false),
                    OpcionaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionalPedido", x => new { x.OpcionaisId, x.PedidoId });
                    table.ForeignKey(
                        name: "FK_OpcionalPedido_Opcionais_OpcionaisId",
                        column: x => x.OpcionaisId,
                        principalTable: "Opcionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpcionalPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Embalagems",
                columns: new[] { "Id", "Descricao", "Nome", "TempoPreparo", "Valor" },
                values: new object[,]
                {
                    { 1, "Copo Pequeno de 300 ML", "Pequeno", 5, 10.0 },
                    { 2, "Copo Medio de 500 ML", "Medio", 7, 13.0 },
                    { 3, "Copo Grande de 700 ML", "Grande", 10, 15.0 }
                });

            migrationBuilder.InsertData(
                table: "Opcionais",
                columns: new[] { "Id", "Descricao", "Nome", "TempoAdd", "ValorAdd" },
                values: new object[,]
                {
                    { 1, "Será Adicionado Granola Como Opcional!", "Granola", 0, 0.0 },
                    { 2, "Sera Adicionado Paçoca no Sorvete", "Paçoca", 3, 3.0 },
                    { 3, "Sera Adicionado como opcional Leite ninho", "Leite Ninho", 0, 3.0 }
                });

            migrationBuilder.InsertData(
                table: "Sabores",
                columns: new[] { "Id", "AddTempo", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, 0, "Sorvete de Açai Sabor Morango", "Morango" },
                    { 2, 0, "Sorvete com Açai Sabor Banana", "Banana" },
                    { 3, 5, "Sorvete de Açai  Com Kiwi", "Kiwi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcionalPedido_PedidoId",
                table: "OpcionalPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_SaborID",
                table: "Pedidos",
                column: "SaborID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_TamanhoId",
                table: "Pedidos",
                column: "TamanhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpcionalPedido");

            migrationBuilder.DropTable(
                name: "Opcionais");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Sabores");

            migrationBuilder.DropTable(
                name: "Embalagems");
        }
    }
}
