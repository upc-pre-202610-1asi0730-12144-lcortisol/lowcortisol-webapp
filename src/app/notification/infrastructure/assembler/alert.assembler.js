import { BaseAssembler } from "../../../shared/infrastructure/assembler/base.assembler";
import { Alert } from "../../domain/model/alert.entity";
import { AlertResource } from "../resource/alert.resource";

export class AlertAssembler extends BaseAssembler {
    toEntity(resource) {
        return new Alert({
            id: resource.id,
            siteId: resource.siteId,
            sensorId: resource.sensorId,
            resourceType: resource.resourceType,
            title: resource.title,
            description: resource.description,
            severity: resource.severity,
            status: resource.status,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new AlertResource({
            id: entity.id,
            siteId: entity.siteId,
            sensorId: entity.sensorId,
            resourceType: entity.resourceType,
            title: entity.title,
            description: entity.description,
            severity: entity.severity,
            status: entity.status,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}