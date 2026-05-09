import { BaseApiEndpoint } from "../../../shared/infrastructure/api/base-api.endpoint";
import { SiteAssembler } from "../assemblers/site.assembler";

export class SiteApiEndpoint extends BaseApiEndpoint {
    constructor() {
        super("/sites", new SiteAssembler());
    }

    async getSitesByWorkplace(workplaceId) {
        const response = await this.getFromPath(`?workplaceId=${workplaceId}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }

    async getSitesByType(type) {
        const response = await this.getFromPath(`?type=${type}`);
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }

    async getActiveSites() {
        const response = await this.getFromPath("/active");
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }
}