import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";
import { AuthSessionService } from "../../../shared/application/services/auth-session.service";

function getCurrentUserId() {
    const user = AuthSessionService.getCurrentUser();

    if (!user) {
        throw new Error("No hay sesión activa.");
    }

    return user.id;
}

export class PlanApiService {
    async getPlans() {
        return ApiClientService.get("/plans");
    }

    async getPlanById(planId) {
        return ApiClientService.get(`/plans/${planId}`);
    }

    async getActiveSubscription() {
        const userId = getCurrentUserId();

        const subscriptions = await ApiClientService.get("/subscriptions", {
            userId,
            status: "active",
        });

        return subscriptions[0] || null;
    }

    async subscribe(payload) {
        const userId = getCurrentUserId();
        const plan = await this.getPlanById(payload.planId);

        const subscription = await ApiClientService.post("/subscriptions", {
            userId,
            workplaceId: payload.workplaceId || "WORKPLACE-001",
            planId: payload.planId,
            status: "active",
            startedAt: new Date().toISOString(),
            expiresAt: null,
            autoRenew: true,
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        await ApiClientService.post("/payments", {
            subscriptionId: subscription.id,
            amount: plan.price,
            currency: plan.currency || "PEN",
            method: payload.paymentMethod || "card",
            status: "paid",
            paidAt: new Date().toISOString(),
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        return subscription;
    }

    async changePlan(payload) {
        const activeSubscription = await this.getActiveSubscription();

        if (!activeSubscription) {
            throw new Error("No tienes una suscripción activa.");
        }

        const plan = await this.getPlanById(payload.newPlanId);

        const updatedSubscription = await ApiClientService.patch(`/subscriptions/${activeSubscription.id}`, {
            planId: payload.newPlanId,
            updatedAt: new Date().toISOString(),
        });

        await ApiClientService.post("/payments", {
            subscriptionId: activeSubscription.id,
            amount: plan.price,
            currency: plan.currency || "PEN",
            method: "card",
            status: "paid",
            paidAt: new Date().toISOString(),
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        await ApiClientService.post("/serviceRequests", {
            subscriptionId: activeSubscription.id,
            type: "change-plan",
            description: `Cambio solicitado al plan ${plan.name}.`,
            status: "resolved",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        return updatedSubscription;
    }

    async cancelSubscription(payload = {}) {
        const activeSubscription = await this.getActiveSubscription();

        if (!activeSubscription) {
            throw new Error("No tienes una suscripción activa.");
        }

        const cancelledSubscription = await ApiClientService.patch(`/subscriptions/${activeSubscription.id}`, {
            status: "cancelled",
            autoRenew: false,
            updatedAt: new Date().toISOString(),
        });

        await ApiClientService.post("/serviceRequests", {
            subscriptionId: activeSubscription.id,
            type: "cancellation",
            description: payload.reason || "Cancelación solicitada por el usuario.",
            status: "open",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        return cancelledSubscription;
    }

    async getPayments() {
        const userId = getCurrentUserId();

        const subscriptions = await ApiClientService.get("/subscriptions", {
            userId,
        });

        const subscriptionIds = subscriptions.map((subscription) => subscription.id);
        const payments = await ApiClientService.get("/payments");

        return payments.filter((payment) => subscriptionIds.includes(payment.subscriptionId));
    }

    async getServiceRequest() {
        const userId = getCurrentUserId();

        const subscriptions = await ApiClientService.get("/subscriptions", {
            userId,
        });

        const subscriptionIds = subscriptions.map((subscription) => subscription.id);
        const request = await ApiClientService.get("/serviceRequests");

        return request.filter((request) => subscriptionIds.includes(request.subscriptionId));
    }

    async getSummary() {
        const plans = await this.getPlans();
        const subscription = await this.getActiveSubscription();
        const payments = await this.getPayments();
        const serviceRequest = await this.getServiceRequest();

        const activePlan = subscription
            ? plans.find((plan) => plan.id === subscription.planId)
            : null;

        return {
            totalPlans: plans.length,
            activePlanName: activePlan?.name || "Sin plan",
            subscriptionStatus: subscription?.status || "inactive",
            totalPayments: payments.length,
            totalPaid: payments.reduce((total, payment) => total + Number(payment.amount || 0), 0),
            serviceRequest: serviceRequest.length,
            maxSites: activePlan?.maxSites || 0,
            maxDevices: activePlan?.maxDevices || 0,
        };
    }
}