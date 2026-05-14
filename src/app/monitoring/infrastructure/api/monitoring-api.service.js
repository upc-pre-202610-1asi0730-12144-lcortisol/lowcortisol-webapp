import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";

export class MonitoringApiService {
    async getSummary() {
        const [readings, anomalies] = await Promise.all([
            ApiClientService.get("/readings"),
            ApiClientService.get("/anomalies"),
        ]);

        return {
            totalWater: readings
                .filter((reading) => reading.resourceType === "water")
                .reduce((total, reading) => total + Number(reading.value || 0), 0),
            totalGas: readings
                .filter((reading) => reading.resourceType === "gas")
                .reduce((total, reading) => total + Number(reading.value || 0), 0),
            criticalAnomalies: anomalies.filter((anomaly) => anomaly.severity === "critical").length,
        };
    }

    async getActiveSession() {
        const sessions = await ApiClientService.get("/monitoringSessions", {
            isActive: true,
        });

        return sessions[0] || null;
    }

    async getReadings() {
        return ApiClientService.get("/readings");
    }

    async getAnomalies() {
        return ApiClientService.get("/anomalies");
    }

    async createRandomReading() {
        const isGas = Math.random() > 0.5;
        const value = isGas
            ? Math.floor(Math.random() * 160)
            : Math.floor(Math.random() * 420);

        const threshold = isGas ? 120 : 300;
        const status = value > threshold ? "critical" : "normal";

        const reading = await ApiClientService.post("/readings", {
            sensorId: isGas ? "SEN-GAS-AUTO" : "SEN-WATER-AUTO",
            siteId: "SITE-001",
            resourceType: isGas ? "gas" : "water",
            value,
            unit: isGas ? "m³" : "L",
            status,
            measuredAt: new Date().toISOString(),
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        const session = await this.getActiveSession();

        if (session) {
            await ApiClientService.patch(`/monitoringSessions/${session.id}`, {
                totalReadings: Number(session.totalReadings || 0) + 1,
                totalAnomalies:
                    Number(session.totalAnomalies || 0) + (status === "critical" ? 1 : 0),
                updatedAt: new Date().toISOString(),
            });
        }

        if (status === "critical") {
            await ApiClientService.post("/anomalies", {
                readingId: reading.id,
                siteId: reading.siteId,
                severity: "critical",
                description: `Consumo crítico de ${isGas ? "Gas" : "Agua"}: ${value} ${reading.unit}`,
                status: "open",
                createdAt: new Date().toISOString(),
                updatedAt: new Date().toISOString(),
            });
        }

        return reading;
    }

    async getReports() {
        return ApiClientService.get("/reports");
    }

    async createReport(siteId = "SITE-001") {
        const [readings, anomalies] = await Promise.all([
            ApiClientService.get("/readings"),
            ApiClientService.get("/anomalies"),
        ]);

        return ApiClientService.post("/reports", {
            siteId,
            period: "weekly",
            totalWater: readings
                .filter((reading) => reading.resourceType === "water")
                .reduce((total, reading) => total + Number(reading.value || 0), 0),
            totalGas: readings
                .filter((reading) => reading.resourceType === "gas")
                .reduce((total, reading) => total + Number(reading.value || 0), 0),
            anomaliesCount: anomalies.length,
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }
}