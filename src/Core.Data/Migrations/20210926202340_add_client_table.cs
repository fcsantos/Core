using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Data.Migrations
{
    public partial class add_client_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    NIF = table.Column<string>(type: "char(14)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1"),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    SecretKey = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ApiKey = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IVBase64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Certificate = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    CertificatePathPfx = table.Column<string>(type: "varchar(300)", nullable: false),
                    PasswordPfx = table.Column<string>(type: "varchar(200)", nullable: false),
                    CertificatePathCer = table.Column<string>(type: "varchar(300)", nullable: false),
                    UsernameAT = table.Column<string>(type: "varchar(200)", nullable: false),
                    PasswordAT = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
