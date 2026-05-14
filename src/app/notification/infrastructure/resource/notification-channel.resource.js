import { BaseResource } from "../../../shared/infrastructure/resource/base.resource";

export class NotificationChannelResource extends BaseResource {
    constructor({
                    id = null,
                    name = "",
                    type = "email",
                    destination = "",
                    enabled = true,
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.name = name;
        this.type = type;
        this.destination = destination;
        this.enabled = enabled;
    }
}