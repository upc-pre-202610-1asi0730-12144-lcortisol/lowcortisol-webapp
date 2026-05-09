import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { MonitoringSummary } from "../../domain/model/monitoring-summary.entity";
import { MonitoringSummaryResource } from "../resources/monitoring-summary.resource";

export class MonitoringSummaryAssembler extends BaseAssembler {
    toEntity(resource) {
        return new MonitoringSummary({
            id: resource.id,
            totalWater: resource.totalWater,
            totalGas: resource.totalGas,
            activeSensors: resource.activeSensors,
            activeSessions: resource.activeSessions,
            criticalAnomalies: resource.criticalAnomalies,
            reportsGenerated: resource.reportsGenerated,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new MonitoringSummaryResource({
            id: entity.id,
            totalWater: entity.totalWater,
            totalGas: entity.totalGas,
            activeSensors: entity.activeSensors,
            activeSessions: entity.activeSessions,
            criticalAnomalies: entity.criticalAnomalies,
            reportsGenerated: entity.reportsGenerated,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}