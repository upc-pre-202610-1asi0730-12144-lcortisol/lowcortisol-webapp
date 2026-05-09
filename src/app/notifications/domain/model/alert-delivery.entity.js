import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class AlertDelivery extends BaseEntity {
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
        this.sentAt = sentAt ? new Date(sentAt) : null;
    }

    markAsSent() {
        this.status = "sent";
        this.sentAt = new Date();
        this.updateTimestamp();
    }

    markAsFailed() {
        this.status = "failed";
        this.updateTimestamp();
    }

    get statusLabel() {
        const labels = {
            pending: "Pendiente",
            sent: "Enviada",
            failed: "Fallida",
        };

        return labels[this.status] ?? "Sin estado";
    }
}