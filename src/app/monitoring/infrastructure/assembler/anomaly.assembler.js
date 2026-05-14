import { BaseAssembler } from "../../../shared/infrastructure/assembler/base.assembler";
import { Anomaly } from "../../domain/model/anomaly.entity";
import { AnomalyResource } from "../resource/anomaly.resource";

export class AnomalyAssembler extends BaseAssembler {
    toEntity(resource) {
        return new Anomaly({
            id: resource.id,
            readingId: resource.readingId,
            siteId: resource.siteId,
            resourceType: resource.resourceType,
            severity: resource.severity,
            description: resource.description,
            detectedAt: resource.detectedAt,
            status: resource.status,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new AnomalyResource({
            id: entity.id,
            readingId: entity.readingId,
            siteId: entity.siteId,
            resourceType: entity.resourceType,
            severity: entity.severity,
            description: entity.description,
            detectedAt: entity.detectedAt,
            status: entity.status,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}