import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class NotificationChannel extends BaseEntity {
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

    enable() {
        this.enabled = true;
        this.updateTimestamp();
    }

    disable() {
        this.enabled = false;
        this.updateTimestamp();
    }

    get typeLabel() {
        const labels = {
            email: "Correo",
            sms: "SMS",
            whatsapp: "WhatsApp",
            push: "Push",
        };

        return labels[this.type] ?? "Canal";
    }
}