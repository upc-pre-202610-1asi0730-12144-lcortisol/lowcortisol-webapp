import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class DeviceCommand extends BaseEntity {
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
        this.executedAt = executedAt ? new Date(executedAt) : null;
    }

    markAsExecuted() {
        this.status = "executed";
        this.executedAt = new Date();
        this.updateTimestamp();
    }

    markAsFailed() {
        this.status = "failed";
        this.updateTimestamp();
    }

    get statusLabel() {
        const labels = {
            pending: "Pendiente",
            executed: "Ejecutado",
            failed: "Fallido",
        };

        return labels[this.status] ?? "Sin estado";
    }

    get commandLabel() {
        const labels = {
            sync: "Sincronizar",
            closeValve: "Cerrar válvula",
            openValve: "Abrir válvula",
            reboot: "Reiniciar",
        };

        return labels[this.commandType] ?? "Comando";
    }
}