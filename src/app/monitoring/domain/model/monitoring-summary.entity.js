import { BaseEntity } from "../../../shared/domain/model/base.entity";

export class MonitoringSummary extends BaseEntity {
    constructor({
                    id = null,
                    totalWater = 0,
                    totalGas = 0,
                    activeSensors = 0,
                    activeSessions = 0,
                    criticalAnomalies = 0,
                    reportsGenerated = 0,
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.totalWater = totalWater;
        this.totalGas = totalGas;
        this.activeSensors = activeSensors;
        this.activeSessions = activeSessions;
        this.criticalAnomalies = criticalAnomalies;
        this.reportsGenerated = reportsGenerated;
    }
}