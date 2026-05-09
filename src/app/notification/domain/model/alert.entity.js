import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class Alert extends BaseEntity {
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

    assign() {
        this.status = "assigned";
        this.updateTimestamp();
    }

    resolve() {
        this.status = "resolved";
        this.updateTimestamp();
    }

    close() {
        this.status = "closed";
        this.updateTimestamp();
    }

    get isOpen() {
        return this.status === "open";
    }

    get severityLabel() {
        const labels = {
            info: "Informativa",
            warning: "Advertencia",
            critical: "Crítica",
        };

        return labels[this.severity] ?? "Sin severidad";
    }

    get statusLabel() {
        const labels = {
            open: "Abierta",
            assigned: "Asignada",
            resolved: "Resuelta",
            closed: "Cerrada",
        };

        return labels[this.status] ?? "Sin estado";
    }

    get resourceLabel() {
        const labels = {
            water: "Agua",
            gas: "Gas",
        };

        return labels[this.resourceType] ?? "Recurso";
    }
}