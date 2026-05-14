import { DeviceControlApiService } from "../../infrastructure/api/device-control-api.service";

export class DeviceControlFacade {
    constructor() {
        this.deviceControlApiService = new DeviceControlApiService();
    }

    async getDevices() {
        return this.deviceControlApiService.getDevices();
    }

    async getDeviceById(deviceId) {
        return this.deviceControlApiService.getDeviceById(deviceId);
    }

    async getDevicesBySite(siteId) {
        return this.deviceControlApiService.getDevicesBySite(siteId);
    }

    async createDevice(payload) {
        return this.deviceControlApiService.createDevice(payload);
    }

    async getSensors() {
        return this.deviceControlApiService.getSensors();
    }

    async linkSensor(payload) {
        return this.deviceControlApiService.linkSensor(payload);
    }

    async getValves() {
        return this.deviceControlApiService.getValves();
    }

    async updateValveStatus(payload) {
        return this.deviceControlApiService.updateValveStatus(payload);
    }

    async executeCommand(payload) {
        return this.deviceControlApiService.executeCommand(payload);
    }

    async getCommands() {
        return this.deviceControlApiService.getCommands();
    }

    async getSummary() {
        return this.deviceControlApiService.getSummary();
    }
}