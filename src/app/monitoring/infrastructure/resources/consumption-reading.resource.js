import { BaseResource } from "../../../shared/infrastructure/resources/base.resource";

export class ConsumptionReadingResource extends BaseResource {
    constructor({
                    id = null,
                    sessionId = "",
                    siteId = "",
                    sensorId = "",
                    resourceType = "water",
                    value = 0,
                    unit = "L",
                    recordedAt = new Date(),
                    status = "normal",
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.sessionId = sessionId;
        this.siteId = siteId;
        this.sensorId = sensorId;
        this.resourceType = resourceType;
        this.value = value;
        this.unit = unit;
        this.recordedAt = recordedAt;
        this.status = status;
    }
}