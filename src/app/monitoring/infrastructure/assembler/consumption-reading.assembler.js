import { BaseAssembler } from "../../../shared/infrastructure/assembler/base.assembler";
import { ConsumptionReading } from "../../domain/model/consumption-reading.entity";
import { ConsumptionReadingResource } from "../resource/consumption-reading.resource";

export class ConsumptionReadingAssembler extends BaseAssembler {
    toEntity(resource) {
        return new ConsumptionReading({
            id: resource.id,
            sessionId: resource.sessionId,
            siteId: resource.siteId,
            sensorId: resource.sensorId,
            resourceType: resource.resourceType,
            value: resource.value,
            unit: resource.unit,
            recordedAt: resource.recordedAt,
            status: resource.status,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new ConsumptionReadingResource({
            id: entity.id,
            sessionId: entity.sessionId,
            siteId: entity.siteId,
            sensorId: entity.sensorId,
            resourceType: entity.resourceType,
            value: entity.value,
            unit: entity.unit,
            recordedAt: entity.recordedAt,
            status: entity.status,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}