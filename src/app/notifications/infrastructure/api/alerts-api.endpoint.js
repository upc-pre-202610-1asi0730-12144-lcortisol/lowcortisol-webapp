import { BaseApiEndpoint } from "../../../shared/infrastructure/api/base-api.endpoint";
import { AlertAssembler } from "../assemblers/alert.assembler";

export class AlertsApiEndpoint extends BaseApiEndpoint {
    constructor() {
        super("/alerts", new AlertAssembler());
    }

    async getAlertsBySite(siteId) {
        const response = await this.getFromPath(`?siteId=${siteId}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }

    async resolveAlert(alertId) {
        const response = await this.postToPath(`/${alertId}/resolve`);
        const resource = response?.data ?? response;

        return this.assembler.toEntity(resource);
    }

    async closeAlert(alertId) {
        const response = await this.postToPath(`/${alertId}/close`);
        const resource = response?.data ?? response;

        return this.assembler.toEntity(resource);
    }
}