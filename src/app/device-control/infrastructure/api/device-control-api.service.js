import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";

function normalizeDevice(device, sensors = [], valves = [], commands = []) {
    return {
        ...device,
        isOnline: device.status === "online",
        sensors: sensors.filter((sensor) => sensor.deviceId === device.id),
        valves: valves
            .filter((valve) => valve.deviceId === device.id)
            .map(normalizeValve),
        commands: commands.filter((command) => command.deviceId === device.id),
    };
}

function normalizeValve(valve) {
    return {
        ...valve,
        isOpen: valve.status === "open",
    };
}

function normalizeSensor(sensor) {
    return {
        ...sensor,
        hasExceededThreshold: Boolean(sensor.hasExceededThreshold),
    };
}

export class DeviceControlApiService {
    async getDevices() {
        const [devices, sensors, valves, commands] = await Promise.all([
            ApiClientService.get("/devices"),
            ApiClientService.get("/sensors"),
            ApiClientService.get("/valves"),
            ApiClientService.get("/commands"),
        ]);

        const normalizedSensors = sensors.map(normalizeSensor);

        return devices.map((device) =>
            normalizeDevice(device, normalizedSensors, valves, commands)
        );
    }

    async createDevice(payload) {
        return ApiClientService.post("/devices", {
            siteId: payload.siteId || "SITE-001",
            name: payload.name,
            type: payload.type || "hub",
            status: payload.status || "online",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async linkSensor(payload) {
        const sensor = {
            deviceId: payload.deviceId,
            siteId: payload.siteId,
            name: payload.name,
            resourceType: payload.resourceType || "water",
            currentValue: Number(payload.currentValue || 0),
            unit: payload.unit || "L",
            threshold: Number(payload.threshold || 300),
            status: "active",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        };

        sensor.hasExceededThreshold = sensor.currentValue > sensor.threshold;

        return ApiClientService.post("/sensors", sensor);
    }

    async closeValve(valveId) {
        return ApiClientService.patch(`/valves/${valveId}`, {
            status: "closed",
            openingPercentage: 0,
            updatedAt: new Date().toISOString(),
        });
    }

    async openValve(valveId) {
        return ApiClientService.patch(`/valves/${valveId}`, {
            status: "open",
            openingPercentage: 100,
            updatedAt: new Date().toISOString(),
        });
    }

    async executeSyncCommand(deviceId) {
        return ApiClientService.post("/commands", {
            deviceId,
            commandType: "sync",
            status: "executed",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async getCommands() {
        return ApiClientService.get("/commands");
    }

    async getSummary() {
        const [devices, sensors, valves, commands] = await Promise.all([
            ApiClientService.get("/devices"),
            ApiClientService.get("/sensors"),
            ApiClientService.get("/valves"),
            ApiClientService.get("/commands"),
        ]);

        return {
            totalDevices: devices.length,
            activeSensors: sensors.filter((sensor) => sensor.status === "active").length,
            closedValves: valves.filter((valve) => valve.status === "closed").length,
            totalSensors: sensors.length,
            totalValves: valves.length,
            totalCommands: commands.length,
        };
    }
}