using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LowCortisol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consumption_reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    period = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    total_water_consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_water_unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    total_gas_consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_gas_unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    generated_at_utc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumption_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    location = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    threshold_value = table.Column<decimal>(type: "TEXT", nullable: true),
                    threshold_unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "incidents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    device_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    reported_by_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    assigned_user_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    severity = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incidents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "monitoring_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    device_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    started_at_utc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monitoring_sessions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    recipient_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    channel = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    body = table.Column<string>(type: "TEXT", maxLength: 600, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    phone = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    password_hash = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    is_active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_lines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    label = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    water_consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    water_unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    gas_consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    gas_unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    consumption_report_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_lines", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_lines_consumption_reports_consumption_report_id",
                        column: x => x.consumption_report_id,
                        principalTable: "consumption_reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sensors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    sensor_type = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    location = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    device_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sensors", x => x.id);
                    table.ForeignKey(
                        name: "FK_sensors_devices_device_id",
                        column: x => x.device_id,
                        principalTable: "devices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "incident_history_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    action = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    incident_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incident_history_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_incident_history_entries_incidents_incident_id",
                        column: x => x.incident_id,
                        principalTable: "incidents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monitoring_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    device_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    event_type = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    monitoring_session_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monitoring_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_monitoring_events_monitoring_sessions_monitoring_session_id",
                        column: x => x.monitoring_session_id,
                        principalTable: "monitoring_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sensor_readings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    device_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    sensor_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    sensor_type = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    reading_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    recorded_at_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    monitoring_session_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sensor_readings", x => x.id);
                    table.ForeignKey(
                        name: "FK_sensor_readings_monitoring_sessions_monitoring_session_id",
                        column: x => x.monitoring_session_id,
                        principalTable: "monitoring_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification_attempts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    attempted_at_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    failure_reason = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    notification_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_attempts", x => x.id);
                    table.ForeignKey(
                        name: "FK_notification_attempts_notifications_notification_id",
                        column: x => x.notification_id,
                        principalTable: "notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_incident_history_entries_incident_id",
                table: "incident_history_entries",
                column: "incident_id");

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_events_monitoring_session_id",
                table: "monitoring_events",
                column: "monitoring_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_attempts_notification_id",
                table: "notification_attempts",
                column: "notification_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_lines_consumption_report_id",
                table: "report_lines",
                column: "consumption_report_id");

            migrationBuilder.CreateIndex(
                name: "IX_sensor_readings_monitoring_session_id",
                table: "sensor_readings",
                column: "monitoring_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_sensors_device_id",
                table: "sensors",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_accounts_email",
                table: "user_accounts",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incident_history_entries");

            migrationBuilder.DropTable(
                name: "monitoring_events");

            migrationBuilder.DropTable(
                name: "notification_attempts");

            migrationBuilder.DropTable(
                name: "report_lines");

            migrationBuilder.DropTable(
                name: "sensor_readings");

            migrationBuilder.DropTable(
                name: "sensors");

            migrationBuilder.DropTable(
                name: "user_accounts");

            migrationBuilder.DropTable(
                name: "incidents");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "consumption_reports");

            migrationBuilder.DropTable(
                name: "monitoring_sessions");

            migrationBuilder.DropTable(
                name: "devices");
        }
    }
}
