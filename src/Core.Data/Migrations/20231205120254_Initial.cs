using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "varchar(200)", nullable: false),
                    Number = table.Column<string>(type: "varchar(50)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(250)", nullable: true),
                    PostalCode = table.Column<string>(type: "char(8)", nullable: false),
                    District = table.Column<string>(type: "varchar(100)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    NIF = table.Column<string>(type: "char(14)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1"),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Controllers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ControllerName = table.Column<string>(type: "varchar(100)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controllers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Document = table.Column<string>(type: "char(14)", nullable: false),
                    TypeSupplier = table.Column<string>(type: "varchar(30)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Adresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Adresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionName = table.Column<string>(type: "varchar(100)", nullable: true),
                    ControllerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Controllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "Controllers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "varchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ControllerId",
                table: "Actions",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers",
                column: "AddressId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Controllers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
