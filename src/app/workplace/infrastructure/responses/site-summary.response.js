import { BaseResponse } from "../../../shared/infrastructure/responses/base.response";

export class SiteSummaryResponse extends BaseResponse {
    constructor({ data = null, message = "", success = true, errors = [] } = {}) {
        super({ data, message, success, errors });
    }
}