import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";

export class NotificationApiService {
    async getAlerts() {
        return ApiClientService.get("/alerts");
    }

    async createAlert(payload) {
        const alert = await ApiClientService.post("/alerts", {
            siteId: payload.siteId,
            sensorId: payload.sensorId,
            resourceType: payload.resourceType || "water",
            title: payload.title,
            description: payload.description,
            severity: payload.severity || "warning",
            status: "open",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        if (alert.severity === "critical" || alert.severity === "warning") {
            await ApiClientService.post("/incidents", {
                alertId: alert.id,
                siteId: alert.siteId,
                title: `Incidente: ${alert.title}`,
                description: alert.description,
                priority: alert.severity === "critical" ? "critical" : "medium",
                status: "open",
                assignedTo: "",
                createdAt: new Date().toISOString(),
                updatedAt: new Date().toISOString(),
            });
        }

        return alert;
    }

    async resolveAlert(alertId) {
        return ApiClientService.patch(`/alerts/${alertId}`, {
            status: "resolved",
            updatedAt: new Date().toISOString(),
        });
    }

    async closeAlert(alertId) {
        return ApiClientService.patch(`/alerts/${alertId}`, {
            status: "closed",
            updatedAt: new Date().toISOString(),
        });
    }

    async getThresholds() {
        return ApiClientService.get("/thresholds");
    }

    async createThreshold(payload) {
        return ApiClientService.post("/thresholds", {
            siteId: payload.siteId,
            sensorId: payload.sensorId,
            resourceType: payload.resourceType || "water",
            warningLimit: Number(payload.warningLimit || 250),
            criticalLimit: Number(payload.criticalLimit || 320),
            unit: payload.unit || "L",
            enabled: true,
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async getIncidents() {
        return ApiClientService.get("/incidents");
    }

    async getNotificationChannels() {
        return ApiClientService.get("/notificationChannels");
    }

    async getAlertDeliveries() {
        return ApiClientService.get("/alertDeliveries");
    }

    async sendAlert(alertId) {
        const channels = await this.getNotificationChannels();
        const enabledChannels = channels.filter((channel) => channel.enabled);

        const deliveries = await Promise.all(
            enabledChannels.map((channel) =>
                ApiClientService.post("/alertDeliveries", {
                    alertId,
                    channelId: channel.id,
                    status: "sent",
                    sentAt: new Date().toISOString(),
                    createdAt: new Date().toISOString(),
                    updatedAt: new Date().toISOString(),
                })
            )
        );

        return deliveries;
    }

    async getSummary() {
        const [alerts, thresholds, incidents, channels, deliveries] = await Promise.all([
            ApiClientService.get("/alerts"),
            ApiClientService.get("/thresholds"),
            ApiClientService.get("/incidents"),
            ApiClientService.get("/notificationChannels"),
            ApiClientService.get("/alertDeliveries"),
        ]);

        return {
            totalAlerts: alerts.length,
            openAlerts: alerts.filter((alert) => alert.status === "open").length,
            criticalAlerts: alerts.filter((alert) => alert.severity === "critical").length,
            totalThresholds: thresholds.length,
            activeThresholds: thresholds.filter((threshold) => threshold.enabled).length,
            totalIncidents: incidents.length,
            openIncidents: incidents.filter((incident) => incident.status !== "closed").length,
            channelsEnabled: channels.filter((channel) => channel.enabled).length,
            deliveriesSent: deliveries.filter((delivery) => delivery.status === "sent").length,
        };
    }
}