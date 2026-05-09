import { BaseResource } from "../../../shared/infrastructure/resources/base.resource";

export class AlertDeliveryResource extends BaseResource {
    constructor({
                    id = null,
                    alertId = "",
                    channelId = "",
                    status = "pending",
                    sentAt = null,
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.alertId = alertId;
        this.channelId = channelId;
        this.status = status;
        this.sentAt = sentAt;
    }
}