using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MES_Cycle.QUERY.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cycle",
                columns: table => new
                {
                    CycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Parts_per_cycle = table.Column<int>(type: "int", nullable: false),
                    Finished = table.Column<int>(type: "int", nullable: false),
                    ProductionOrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MachineConfigId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cycle", x => x.CycleId);
                });

            migrationBuilder.CreateTable(
                name: "MachineConfig",
                columns: table => new
                {
                    MachineConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Grit = table.Column<int>(type: "int", nullable: false),
                    Cycle_duration = table.Column<int>(type: "int", nullable: false),
                    Operator_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineIdSeq = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineConfig", x => x.MachineConfigId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cycle");

            migrationBuilder.DropTable(
                name: "MachineConfig");
        }
    }
}
