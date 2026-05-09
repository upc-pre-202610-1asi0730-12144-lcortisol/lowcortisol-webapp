import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { AlertDelivery } from "../../domain/model/alert-delivery.entity";
import { AlertDeliveryResource } from "../resources/alert-delivery.resource";

export class AlertDeliveryAssembler extends BaseAssembler {
    toEntity(resource) {
        return new AlertDelivery({
            id: resource.id,
            alertId: resource.alertId,
            channelId: resource.channelId,
            status: resource.status,
            sentAt: resource.sentAt,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new AlertDeliveryResource({
            id: entity.id,
            alertId: entity.alertId,
            channelId: entity.channelId,
            status: entity.status,
            sentAt: entity.sentAt,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}