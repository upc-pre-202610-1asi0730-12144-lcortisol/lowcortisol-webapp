import { BaseApiEndpoint } from "../../../shared/infrastructure/api/base-api.endpoint";
import { ConsumptionReadingAssembler } from "../assemblers/consumption-reading.assembler";

export class ReadingsApiEndpoint extends BaseApiEndpoint {
    constructor() {
        super("/readings", new ConsumptionReadingAssembler());
    }

    async getReadingsBySite(siteId) {
        const response = await this.getFromPath(`?siteId=${siteId}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }

    async getReadingsByResource(resourceType) {
        const response = await this.getFromPath(`?resourceType=${resourceType}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }
}