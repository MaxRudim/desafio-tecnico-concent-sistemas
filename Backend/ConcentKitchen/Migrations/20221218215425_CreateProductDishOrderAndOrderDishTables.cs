﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcentKitchen.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductDishOrderAndOrderDishTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "OrderDishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DishPrice = table.Column<float>(type: "real", nullable: false),
                    DishPreparationTimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    DishCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DishIngredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDishId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                    table.ForeignKey(
                        name: "FK_Dishes_OrderDishes_OrderDishId",
                        column: x => x.OrderDishId,
                        principalTable: "OrderDishes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDishId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderDishes_OrderDishId",
                        column: x => x.OrderDishId,
                        principalTable: "OrderDishes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_OrderDishId",
                table: "Dishes",
                column: "OrderDishId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDishId",
                table: "Orders",
                column: "OrderDishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "OrderDishes");
        }
    }
}
