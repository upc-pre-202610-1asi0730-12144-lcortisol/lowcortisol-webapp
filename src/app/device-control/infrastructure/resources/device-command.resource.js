import { BaseResource } from "../../../shared/infrastructure/resources/base.resource";

export class DeviceCommandResource extends BaseResource {
    constructor({
                    id = null,
                    deviceId = "",
                    commandType = "sync",
                    payload = {},
                    status = "pending",
                    executedAt = null,
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.deviceId = deviceId;
        this.commandType = commandType;
        this.payload = payload;
        this.status = status;
        this.executedAt = executedAt;
    }
}