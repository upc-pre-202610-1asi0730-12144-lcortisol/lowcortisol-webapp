import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { DeviceCommand } from "../../domain/model/device-command.entity";
import { DeviceCommandResource } from "../resources/device-command.resource";

export class DeviceCommandAssembler extends BaseAssembler {
    toEntity(resource) {
        return new DeviceCommand({
            id: resource.id,
            deviceId: resource.deviceId,
            commandType: resource.commandType,
            payload: resource.payload,
            status: resource.status,
            executedAt: resource.executedAt,
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new DeviceCommandResource({
            id: entity.id,
            deviceId: entity.deviceId,
            commandType: entity.commandType,
            payload: entity.payload,
            status: entity.status,
            executedAt: entity.executedAt,
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}