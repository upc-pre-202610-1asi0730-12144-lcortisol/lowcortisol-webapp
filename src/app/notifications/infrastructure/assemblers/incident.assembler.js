import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { Incident } from "../../domain/model/incident.entity";
import { IncidentResource } from "../resources/incident.resource";

export class IncidentAssembler extends BaseAssembler {
    toEntity(resource) {
        return new Incident({
            id: resource.id,
            alertId: resource.alertId,
            siteId: resource.siteId,
            title: resource.title,
            description: resource.description,
            priority: resource.priority,
            status: resource.status,
            assignedTo: resource.assignedTo,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new IncidentResource({
            id: entity.id,
            alertId: entity.alertId,
            siteId: entity.siteId,
            title: entity.title,
            description: entity.description,
            priority: entity.priority,
            status: entity.status,
            assignedTo: entity.assignedTo,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}