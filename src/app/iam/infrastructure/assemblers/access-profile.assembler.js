import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { AccessProfile } from "../../domain/model/access-profile.entity";
import { AccessProfileResource } from "../resources/access-profile.resource";

export class AccessProfileAssembler extends BaseAssembler {
    toEntity(resource) {
        return new AccessProfile({
            id: resource.id,
            name: resource.name,
            role: resource.role,
            permissions: resource.permissions,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new AccessProfileResource({
            id: entity.id,
            name: entity.name,
            role: entity.role,
            permissions: entity.permissions,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}