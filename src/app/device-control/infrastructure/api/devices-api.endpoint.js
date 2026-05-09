import { BaseApiEndpoint } from "../../../shared/infrastructure/api/base-api.endpoint";
import { DeviceAssembler } from "../assemblers/device.assembler";

export class DevicesApiEndpoint extends BaseApiEndpoint {
    constructor() {
        super("/devices", new DeviceAssembler());
    }

    async getDevicesBySite(siteId) {
        const response = await this.getFromPath(`?siteId=${siteId}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }

    async executeCommand(deviceId, resource) {
        return this.postToPath(`/${deviceId}/commands`, resource);
    }
}