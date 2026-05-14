import { NotificationApiService } from "../../infrastructure/api/notification-api.service";

export class NotificationFacade {
    constructor() {
        this.notificationApiService = new NotificationApiService();
    }

    async getAlerts() {
        return this.notificationApiService.getAlerts();
    }

    async createAlert(payload) {
        return this.notificationApiService.createAlert(payload);
    }

    async updateAlert(payload) {
        return this.notificationApiService.updateAlert(payload);
    }

    async getThresholds() {
        return this.notificationApiService.getThresholds();
    }

    async createThreshold(payload) {
        return this.notificationApiService.createThreshold(payload);
    }

    async getIncidents() {
        return this.notificationApiService.getIncidents();
    }

    async getChannels() {
        return this.notificationApiService.getChannels();
    }

    async sendAlert(alertId) {
        return this.notificationApiService.sendAlert(alertId);
    }

    async getDeliveries() {
        return this.notificationApiService.getDeliveries();
    }

    async getSummary() {
        return this.notificationApiService.getSummary();
    }
}