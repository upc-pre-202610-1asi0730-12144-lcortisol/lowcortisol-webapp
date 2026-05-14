import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class Anomaly extends BaseEntity {
    constructor({
                    id = null,
                    readingId = "",
                    siteId = "",
                    resourceType = "water",
                    severity = "warning",
                    description = "",
                    detectedAt = new Date(),
                    status = "open",
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.readingId = readingId;
        this.siteId = siteId;
        this.resourceType = resourceType;
        this.severity = severity;
        this.description = description;
        this.detectedAt = detectedAt ? new Date(detectedAt) : new Date();
        this.status = status;
    }

    resolve() {
        this.status = "resolved";
        this.updateTimestamp();
    }

    get severityLabel() {
        const labels = {
            warning: "Advertencia",
            critical: "Crítico",
        };

        return labels[this.severity] ?? "Sin severidad";
    }
}