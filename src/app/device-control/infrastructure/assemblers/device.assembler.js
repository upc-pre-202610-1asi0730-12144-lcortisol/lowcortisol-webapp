import { BaseAssembler } from "../../../shared/infrastructure/assemblers/base.assembler";
import { Device } from "../../domain/model/device.entity";
import { DeviceResource } from "../resources/device.resource";

import { SensorAssembler } from "./sensor.assembler";
import { ValveAssembler } from "./valve.assembler";
import { DeviceCommandAssembler } from "./device-command.assembler";

export class DeviceAssembler extends BaseAssembler {
    constructor() {
        super();

        this.sensorAssembler = new SensorAssembler();
        this.valveAssembler = new ValveAssembler();
        this.deviceCommandAssembler = new DeviceCommandAssembler();
    }

    toEntity(resource) {
        return new Device({
            id: resource.id,
            siteId: resource.siteId,
            name: resource.name,
            type: resource.type,
            status: resource.status,
            sensors: this.sensorAssembler.toEntities(resource.sensors ?? []),
            valves: this.valveAssembler.toEntities(resource.valves ?? []),
            commands: this.deviceCommandAssembler.toEntities(resource.commands ?? []),
            createdAt: resource.createdAt,
            updatedAt: resource.updatedAt,
        });
    }

    toResource(entity) {
        return new DeviceResource({
            id: entity.id,
            siteId: entity.siteId,
            name: entity.name,
            type: entity.type,
            status: entity.status,
            sensors: this.sensorAssembler.toResources(entity.sensors ?? []),
            valves: this.valveAssembler.toResources(entity.valves ?? []),
            commands: this.deviceCommandAssembler.toResources(entity.commands ?? []),
            createdAt: entity.createdAt,
            updatedAt: entity.updatedAt,
        });
    }
}