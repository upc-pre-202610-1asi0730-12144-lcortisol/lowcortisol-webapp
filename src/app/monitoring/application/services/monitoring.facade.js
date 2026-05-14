import { MonitoringApiService } from "../../infrastructure/api/monitoring-api.service";

export class MonitoringFacade {
    constructor() {
        this.monitoringApiService = new MonitoringApiService();
    }

    async getSession() {
        return this.monitoringApiService.getSession();
    }

    async getReadings(payload = {}) {
        return this.monitoringApiService.getReadings(payload);
    }

    async createReading(payload) {
        return this.monitoringApiService.createReading(payload);
    }

    async getAnomalies() {
        return this.monitoringApiService.getAnomalies();
    }

    async getReports() {
        return this.monitoringApiService.getReports();
    }

    async createReport(payload) {
        return this.monitoringApiService.createReport(payload);
    }

    async getSummary() {
        return this.monitoringApiService.getSummary();
    }
}