import { reactive, readonly } from "vue";
import { PlansFacade } from "../services/plans.facade";

const plansFacade = new PlansFacade();

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

async function loadPlansPage() {
    state.loading = true;
    state.error = null;

    try {
        state.plans = await plansFacade.getPlans();
        state.subscription = await plansFacade.getActiveSubscription();
        state.payments = await plansFacade.getPayments();
        state.serviceRequests = await plansFacade.getServiceRequests();
        state.summary = await plansFacade.getSummary();
    } catch (error) {
        state.error = error.message || "No se pudieron cargar los planes.";
    } finally {
        state.loading = false;
    }
}

async function subscribeToPlan(planId) {
    const subscription = await plansFacade.subscribe({
        userId: "USR-001",
        workplaceId: "WORKPLACE-001",
        planId,
        paymentMethod: "card",
    });

    await loadPlansPage();

    state.message = "Suscripción creada correctamente.";

    return subscription;
}

async function changePlan(planId) {
    const subscription = await plansFacade.changePlan({
        subscriptionId: state.subscription?.id,
        newPlanId: planId,
    });

    await loadPlansPage();

    state.message = "Plan actualizado correctamente.";

    return subscription;
}

async function cancelSubscription() {
    const subscription = await plansFacade.cancelSubscription({
        subscriptionId: state.subscription?.id,
        reason: "Cancelación solicitada desde la vista de planes.",
    });

    await loadPlansPage();

    state.message = "Suscripción cancelada correctamente.";

    return subscription;
}

function getActivePlan() {
    return state.plans.find((plan) => plan.id === state.subscription?.planId) ?? null;
}

export function usePlansStore() {
    return {
        state: readonly(state),
        loadPlansPage,
        subscribeToPlan,
        changePlan,
        cancelSubscription,
        getActivePlan,
    };
}