import { BaseResource } from "../../../shared/infrastructure/resource/base.resource";

export class IncidentResource extends BaseResource {
    constructor({
                    id = null,
                    alertId = "",
                    siteId = "",
                    title = "",
                    description = "",
                    priority = "medium",
                    status = "open",
                    assignedTo = "",
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.alertId = alertId;
        this.siteId = siteId;
        this.title = title;
        this.description = description;
        this.priority = priority;
        this.status = status;
        this.assignedTo = assignedTo;
    }
}