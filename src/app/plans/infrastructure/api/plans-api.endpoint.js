import { BaseApiEndpoint } from "../../../shared/infrastructure/api/base-api.endpoint";
import { PlanAssembler } from "../assemblers/plan.assembler";

export class PlansApiEndpoint extends BaseApiEndpoint {
    constructor() {
        super("/plans", new PlanAssembler());
    }

    async getRecommendedPlans() {
        const response = await this.getFromPath("?recommended=true");
        const resources = response?.data ?? response ?? [];

        return this.assembler.toEntities(resources);
    }
}