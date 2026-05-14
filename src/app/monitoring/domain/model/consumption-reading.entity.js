import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class ConsumptionReading extends BaseEntity {
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
        this.recordedAt = recordedAt ? new Date(recordedAt) : new Date();
        this.status = status;
    }

    markAsWarning() {
        this.status = "warning";
        this.updateTimestamp();
    }

    markAsCritical() {
        this.status = "critical";
        this.updateTimestamp();
    }

    get resourceLabel() {
        const labels = {
            water: "Agua",
            gas: "Gas",
        };

        return labels[this.resourceType] ?? "Recurso";
    }

    get statusLabel() {
        const labels = {
            normal: "Normal",
            warning: "Advertencia",
            critical: "Crítico",
        };

        return labels[this.status] ?? "Sin estado";
    }
}