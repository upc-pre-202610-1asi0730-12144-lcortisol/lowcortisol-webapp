export class BaseAssembler {
    toEntity(resource) {
        return resource;
    }

    toResource(entity) {
        return entity;
    }

    toResponse(response) {
        return response;
    }

    toEntities(resources = []) {
        return resources.map((resource) => this.toEntity(resource));
    }

    toResources(entities = []) {
        return entities.map((entity) => this.toResource(entity));
    }
}