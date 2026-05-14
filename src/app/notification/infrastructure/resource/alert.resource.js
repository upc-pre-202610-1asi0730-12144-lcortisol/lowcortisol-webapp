import { BaseResource } from "../../../shared/infrastructure/resource/base.resource";

export class AlertResource extends BaseResource {
    constructor({
                    id = null,
                    siteId = "",
                    sensorId = "",
                    resourceType = "water",
                    title = "",
                    description = "",
                    severity = "warning",
                    status = "open",
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.siteId = siteId;
        this.sensorId = sensorId;
        this.resourceType = resourceType;
        this.title = title;
        this.description = description;
        this.severity = severity;
        this.status = status;
    }
}