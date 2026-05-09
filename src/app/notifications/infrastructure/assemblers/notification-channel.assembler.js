import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { NotificationChannel } from "../../domain/model/notification-channel.entity";
import { NotificationChannelResource } from "../resources/notification-channel.resource";

export class NotificationChannelAssembler extends BaseAssembler {
    toEntity(resource) {
        return new NotificationChannel({
            id: resource.id,
            name: resource.name,
            type: resource.type,
            destination: resource.destination,
            enabled: resource.enabled,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new NotificationChannelResource({
            id: entity.id,
            name: entity.name,
            type: entity.type,
            destination: entity.destination,
            enabled: entity.enabled,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}