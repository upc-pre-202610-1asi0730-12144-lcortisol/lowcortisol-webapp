import { PlansApiService } from "../../infrastructure/api/plans-api.service";

export class PlansFacade {
    constructor() {
        this.plansApiService = new PlansApiService();
    }

    async getPlans() {
        return this.plansApiService.getPlans();
    }

    async getPlanById(planId) {
        return this.plansApiService.getPlanById(planId);
    }

    async getActiveSubscription() {
        return this.plansApiService.getActiveSubscription();
    }

    async subscribe(payload) {
        return this.plansApiService.subscribe(payload);
    }

    async changePlan(payload) {
        return this.plansApiService.changePlan(payload);
    }

    async cancelSubscription(payload) {
        return this.plansApiService.cancelSubscription(payload);
    }

    async getPayments() {
        return this.plansApiService.getPayments();
    }

    async getServiceRequests() {
        return this.plansApiService.getServiceRequests();
    }

    async getSummary() {
        return this.plansApiService.getSummary();
    }
}