import { reactive, readonly } from "vue";
import { PlanFacade } from "../services/plan.facade";

const planFacade = new PlanFacade();

const state = reactive({
    plans: [],
    subscription: null,
    payments: [],
    serviceRequests: [],
    summary: {
        totalPlans: 0,
        activePlanName: "",
        subscriptionStatus: "",
        totalPayments: 0,
        totalPaid: 0,
        serviceRequests: 0,
        maxSites: 0,
        maxDevices: 0,
    },
    loading: false,
    error: null,
    message: "",
});

async function loadPlanPage() {
    state.loading = true;
    state.error = null;

    try {
        state.plans = await planFacade.getPlans();
        state.subscription = await planFacade.getActiveSubscription();
        state.payments = await planFacade.getPayments();
        state.serviceRequests = await planFacade.getServiceRequests();
        state.summary = await planFacade.getSummary();
    } catch (error) {
        state.error = error.message || "No se pudieron cargar los planes.";
    } finally {
        state.loading = false;
    }
}

async function subscribeToPlan(planId) {
    const subscription = await planFacade.subscribe({
        userId: "USR-001",
        workplaceId: "WORKPLACE-001",
        planId,
        paymentMethod: "card",
    });

    await loadPlanPage();

    state.message = "Suscripción creada correctamente.";

    return subscription;
}

async function changePlan(planId) {
    const subscription = await planFacade.changePlan({
        subscriptionId: state.subscription?.id,
        newPlanId: planId,
    });

    await loadPlanPage();

    state.message = "Plan actualizado correctamente.";

    return subscription;
}

async function cancelSubscription() {
    const subscription = await planFacade.cancelSubscription({
        subscriptionId: state.subscription?.id,
        reason: "Cancelación solicitada desde la vista de planes.",
    });

    await loadPlanPage();

    state.message = "Suscripción cancelada correctamente.";

    return subscription;
}

function getActivePlan() {
    return state.plans.find((plan) => plan.id === state.subscription?.planId) ?? null;
}

export function usePlanStore() {
    return {
        state: readonly(state),
        loadPlanPage,
        subscribeToPlan,
        changePlan,
        cancelSubscription,
        getActivePlan,
    };
}