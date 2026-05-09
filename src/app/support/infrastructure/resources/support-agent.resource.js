import { BaseResource } from "../../../shared/infrastructure/resources/base.resource";

export class SupportAgentResource extends BaseResource {
    constructor({
                    id = null,
                    fullName = "",
                    email = "",
                    specialty = "technical",
                    status = "available",
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.fullName = fullName;
        this.email = email;
        this.specialty = specialty;
        this.status = status;
    }
}