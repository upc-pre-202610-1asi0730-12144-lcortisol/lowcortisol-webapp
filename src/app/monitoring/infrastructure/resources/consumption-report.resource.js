import { BaseResource } from "../../../shared/infrastructure/resources/base.resource";

export class ConsumptionReportResource extends BaseResource {
    constructor({
                    id = null,
                    siteId = "",
                    period = "monthly",
                    startDate = null,
                    endDate = null,
                    totalWater = 0,
                    totalGas = 0,
                    readingsCount = 0,
                    anomaliesCount = 0,
                    createdAt = null,
                    updatedAt = null,
                } = {}) {
        super({ id, createdAt, updatedAt });

        this.siteId = siteId;
        this.period = period;
        this.startDate = startDate;
        this.endDate = endDate;
        this.totalWater = totalWater;
        this.totalGas = totalGas;
        this.readingsCount = readingsCount;
        this.anomaliesCount = anomaliesCount;
    }
}