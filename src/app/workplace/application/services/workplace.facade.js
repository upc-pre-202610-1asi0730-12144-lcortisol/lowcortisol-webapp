import { WorkplaceApiService } from "../../infrastructure/api/workplace-api.service";

export class WorkplaceFacade {
    constructor() {
        this.workplaceApiService = new WorkplaceApiService();
    }

    async getWorkplace() {
        return this.workplaceApiService.getWorkplace();
    }

    async getSites(type = "all") {
        return this.workplaceApiService.getSites(type);
    }

    async getSiteById(siteId) {
        return this.workplaceApiService.getSiteById(siteId);
    }

    async createSite(payload) {
        return this.workplaceApiService.createSite(payload);
    }

    async assignMemberToSite(payload) {
        return this.workplaceApiService.assignMemberToSite(payload);
    }

    async assignDeviceToSite(payload) {
        return this.workplaceApiService.assignDeviceToSite(payload);
    }

    async getMembersBySite(siteId) {
        return this.workplaceApiService.getMembersBySite(siteId);
    }

    async getAssignmentsBySite(siteId) {
        return this.workplaceApiService.getAssignmentsBySite(siteId);
    }

    async getSummary() {
        return this.workplaceApiService.getSummary();
    }
}