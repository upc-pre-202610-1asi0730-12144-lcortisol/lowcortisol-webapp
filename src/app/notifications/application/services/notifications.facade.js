import { NotificationsApiService } from "../../infrastructure/api/notifications-api.service";

export class NotificationsFacade {
    constructor() {
        this.notificationsApiService = new NotificationsApiService();
    }

    async getAlerts() {
        return this.notificationsApiService.getAlerts();
    }

    async createAlert(payload) {
        return this.notificationsApiService.createAlert(payload);
    }

    async updateAlert(payload) {
        return this.notificationsApiService.updateAlert(payload);
    }

    async getThresholds() {
        return this.notificationsApiService.getThresholds();
    }

    async createThreshold(payload) {
        return this.notificationsApiService.createThreshold(payload);
    }

    async getIncidents() {
        return this.notificationsApiService.getIncidents();
    }

    async getChannels() {
        return this.notificationsApiService.getChannels();
    }

    async sendAlert(alertId) {
        return this.notificationsApiService.sendAlert(alertId);
    }

    async getDeliveries() {
        return this.notificationsApiService.getDeliveries();
    }

    async getSummary() {
        return this.notificationsApiService.getSummary();
    }
}