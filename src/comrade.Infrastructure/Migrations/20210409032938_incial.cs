#region

using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace comrade.Infrastructure.Migrations
{
    public partial class incial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AIRP_AIRPLANE",
                table => new
                {
                    AIRP_SQ_AIRPLANE = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AIRP_TX_CODIGO = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    AIRP_TX_MODELO = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    AIRP_QT_PASSAGEIRO = table.Column<int>("int", nullable: false),
                    AIRP_DT_REGISTRO = table.Column<string>("varchar(48)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_AIRP_AIRPLANE", x => x.AIRP_SQ_AIRPLANE); });

            migrationBuilder.CreateTable(
                "USSI_USUARIO_SISTEMA",
                table => new
                {
                    USSI_SQ_USUARIO_SISTEMA = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USSI_TX_NOME = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    USSI_TX_EMAIL = table.Column<string>("varchar(255)", maxLength: 255, nullable: true),
                    USSI_TX_SENHA = table.Column<string>("varchar(1023)", maxLength: 1023, nullable: false),
                    USSI_ST_SITUACAO = table.Column<int>("int", nullable: false),
                    USSI_TX_MATRICULA = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    USSI_DT_REGISTRO = table.Column<string>("varchar(48)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_USSI_USUARIO_SISTEMA", x => x.USSI_SQ_USUARIO_SISTEMA); });

            migrationBuilder.CreateIndex(
                "IX_AIRPLANE_CODIGO",
                "AIRP_AIRPLANE",
                "AIRP_TX_CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_USUARIO_SISTEMA_EMAIL",
                "USSI_USUARIO_SISTEMA",
                "USSI_TX_EMAIL",
                unique: true,
                filter: "[USSI_TX_EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_USUARIO_SISTEMA_MATRICULA",
                "USSI_USUARIO_SISTEMA",
                "USSI_TX_MATRICULA",
                unique: true);

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations",
                "20210409032938_incial_Up.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AIRP_AIRPLANE");

            migrationBuilder.DropTable(
                "USSI_USUARIO_SISTEMA");
        }
    }
}