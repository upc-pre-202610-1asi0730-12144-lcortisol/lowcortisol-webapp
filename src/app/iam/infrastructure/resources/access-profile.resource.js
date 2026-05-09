import { BaseResource } from "../../../shared/infrastructure/resources/base.resource";

export class AccessProfileResource extends BaseResource {
    constructor({
                    id = null,
                    name = "",
                    role = "owner",
                    permissions = [],
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.name = name;
        this.role = role;
        this.permissions = permissions;
    }
}