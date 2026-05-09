import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class Incident extends BaseEntity {
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

    assignTo(userId) {
        this.assignedTo = userId;
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

    get priorityLabel() {
        const labels = {
            low: "Baja",
            medium: "Media",
            high: "Alta",
            critical: "Crítica",
        };

        return labels[this.priority] ?? "Sin prioridad";
    }

    get statusLabel() {
        const labels = {
            open: "Abierto",
            assigned: "Asignado",
            resolved: "Resuelto",
            closed: "Cerrado",
        };

        return labels[this.status] ?? "Sin estado";
    }
}