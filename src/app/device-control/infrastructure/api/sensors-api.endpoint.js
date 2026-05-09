import { BaseApiEndpoint } from "../../../shared/infrastructure/api/base-api.endpoint";
import { SensorAssembler } from "../assemblers/sensor.assembler";

export class SensorsApiEndpoint extends BaseApiEndpoint {
    constructor() {
        super("/sensors", new SensorAssembler());
    }

    async getSensorsByDevice(deviceId) {
        const response = await this.getFromPath(`?deviceId=${deviceId}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }

    async getSensorsBySite(siteId) {
        const response = await this.getFromPath(`?siteId=${siteId}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }
}