import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { Site } from "../../domain/model/site.entity";
import { SiteResource } from "../resources/site.resource";

export class SiteAssembler extends BaseAssembler {
    toEntity(resource) {
        return new Site({
            id: resource.id,
            workplaceId: resource.workplaceId,
            name: resource.name,
            address: resource.address,
            type: resource.type,
            status: resource.status,
            waterConsumption: resource.waterConsumption,
            gasConsumption: resource.gasConsumption,
            activeSensors: resource.activeSensors,
            activeIncidents: resource.activeIncidents,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new SiteResource({
            id: entity.id,
            workplaceId: entity.workplaceId,
            name: entity.name,
            address: entity.address,
            type: entity.type,
            status: entity.status,
            waterConsumption: entity.waterConsumption,
            gasConsumption: entity.gasConsumption,
            activeSensors: entity.activeSensors,
            activeIncidents: entity.activeIncidents,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}