import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";

export class SitesApiService {
    async getWorkplace() {
        const workplaces = await ApiClientService.get("/workplaces");
        return workplaces[0] || null;
    }

    async getSites(type = "all") {
        if (type && type !== "all") {
            return ApiClientService.get("/sites", { type });
        }

        return ApiClientService.get("/sites");
    }

    async getSiteById(siteId) {
        return ApiClientService.get(`/sites/${siteId}`);
    }

    async createSite(payload) {
        return ApiClientService.post("/sites", {
            workplaceId: payload.workplaceId || "WORKPLACE-001",
            name: payload.name,
            address: payload.address,
            type: payload.type || "residential",
            status: payload.status || "active",
            waterConsumption: Number(payload.waterConsumption || 0),
            gasConsumption: Number(payload.gasConsumption || 0),
            activeSensors: Number(payload.activeSensors || 0),
            activeIncidents: Number(payload.activeIncidents || 0),
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async updateSite(siteId, payload) {
        return ApiClientService.patch(`/sites/${siteId}`, {
            ...payload,
            updatedAt: new Date().toISOString(),
        });
    }

    async deleteSite(siteId) {
        return ApiClientService.delete(`/sites/${siteId}`);
    }

    async getSiteMembers(siteId = "") {
        if (siteId) {
            return ApiClientService.get("/siteMembers", { siteId });
        }

        return ApiClientService.get("/siteMembers");
    }

    async assignMemberToSite(payload) {
        return ApiClientService.post("/siteMembers", {
            siteId: payload.siteId,
            userId: payload.userId,
            fullName: payload.fullName,
            email: payload.email,
            role: payload.role || "operator",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async getSiteDeviceAssignments(siteId = "") {
        if (siteId) {
            return ApiClientService.get("/siteDeviceAssignments", { siteId });
        }

        return ApiClientService.get("/siteDeviceAssignments");
    }

    async assignDeviceToSite(payload) {
        return ApiClientService.post("/siteDeviceAssignments", {
            siteId: payload.siteId,
            deviceId: payload.deviceId,
            deviceName: payload.deviceName,
            deviceType: payload.deviceType || "sensor",
            status: "active",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async getSummary() {
        const [sites, members, assignments] = await Promise.all([
            ApiClientService.get("/sites"),
            ApiClientService.get("/siteMembers"),
            ApiClientService.get("/siteDeviceAssignments"),
        ]);

        return {
            totalSites: sites.length,
            activeSites: sites.filter((site) => site.status === "active").length,
            maintenanceSites: sites.filter((site) => site.status === "maintenance").length,
            totalMembers: members.length,
            totalDeviceAssignments: assignments.length,
            waterConsumption: sites.reduce(
                (total, site) => total + Number(site.waterConsumption || 0),
                0
            ),
            gasConsumption: sites.reduce(
                (total, site) => total + Number(site.gasConsumption || 0),
                0
            ),
        };
    }
}