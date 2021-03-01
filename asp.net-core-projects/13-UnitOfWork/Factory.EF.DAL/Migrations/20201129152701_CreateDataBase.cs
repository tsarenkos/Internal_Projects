using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.EF.DAL.Migrations
{
    public partial class CreateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriticalLevelTypes",
                columns: table => new
                {
                    CriticalLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriticalLevelValue = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalLevelTypes", x => x.CriticalLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Deliverers",
                columns: table => new
                {
                    DelivererId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelivererName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverers", x => x.DelivererId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusTypes",
                columns: table => new
                {
                    RequestStatusCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestStatusValue = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusTypes", x => x.RequestStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    DateOfDelivery = table.Column<DateTime>(type: "date", nullable: false),
                    DelivererId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineId);
                    table.ForeignKey(
                        name: "FK_Machines_Deliverers_DelivererId",
                        column: x => x.DelivererId,
                        principalTable: "Deliverers",
                        principalColumn: "DelivererId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestCreatorId = table.Column<int>(type: "int", nullable: false),
                    RequestHandlerId = table.Column<int>(type: "int", nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "date", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    InnerRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_RequestCreatorId",
                        column: x => x.RequestCreatorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_RequestHandlerId",
                        column: x => x.RequestHandlerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Requests_InnerRequestId",
                        column: x => x.InnerRequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_RequestStatusTypes_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatusTypes",
                        principalColumn: "RequestStatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Breakages",
                columns: table => new
                {
                    BreakageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    BreakageName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "date", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    CriticalLevelId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breakages", x => x.BreakageId);
                    table.ForeignKey(
                        name: "FK_Breakages_CriticalLevelTypes_CriticalLevelId",
                        column: x => x.CriticalLevelId,
                        principalTable: "CriticalLevelTypes",
                        principalColumn: "CriticalLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breakages_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breakages_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breakages_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breakages_CriticalLevelId",
                table: "Breakages",
                column: "CriticalLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Breakages_EmployeeId",
                table: "Breakages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Breakages_MachineId",
                table: "Breakages",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Breakages_RequestId",
                table: "Breakages",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_DelivererId",
                table: "Machines",
                column: "DelivererId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_InnerRequestId",
                table: "Requests",
                column: "InnerRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_MachineId",
                table: "Requests",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestCreatorId",
                table: "Requests",
                column: "RequestCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestHandlerId",
                table: "Requests",
                column: "RequestHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusId",
                table: "Requests",
                column: "RequestStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breakages");

            migrationBuilder.DropTable(
                name: "CriticalLevelTypes");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "RequestStatusTypes");

            migrationBuilder.DropTable(
                name: "Deliverers");
        }
    }
}
