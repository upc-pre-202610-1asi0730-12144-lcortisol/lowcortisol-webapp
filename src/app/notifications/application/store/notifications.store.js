import { reactive, readonly } from "vue";
import { NotificationsFacade } from "../services/notifications.facade";

const notificationsFacade = new NotificationsFacade();

const state = reactive({
    alerts: [],
    thresholds: [],
    incidents: [],
    channels: [],
    deliveries: [],
    summary: {
        totalAlerts: 0,
        openAlerts: 0,
        criticalAlerts: 0,
        totalThresholds: 0,
        activeThresholds: 0,
        totalIncidents: 0,
        openIncidents: 0,
        channelsEnabled: 0,
        deliveriesSent: 0,
    },
    loading: false,
    error: null,
    message: "",
});

async function loadAlertsPage() {
    state.loading = true;
    state.error = null;

    try {
        state.alerts = await notificationsFacade.getAlerts();
        state.thresholds = await notificationsFacade.getThresholds();
        state.incidents = await notificationsFacade.getIncidents();
        state.channels = await notificationsFacade.getChannels();
        state.deliveries = await notificationsFacade.getDeliveries();
        state.summary = await notificationsFacade.getSummary();
    } catch (error) {
        state.error = error.message || "No se pudieron cargar las alertas.";
    } finally {
        state.loading = false;
    }
}

async function createAlert(payload) {
    const alert = await notificationsFacade.createAlert(payload);

    await loadAlertsPage();

    state.message = "Alerta creada correctamente.";

    return alert;
}

async function resolveAlert(alertId) {
    const alert = await notificationsFacade.updateAlert({
        alertId,
        status: "resolved",
    });

    await loadAlertsPage();

    state.message = "Alerta resuelta correctamente.";

    return alert;
}

async function closeAlert(alertId) {
    const alert = await notificationsFacade.updateAlert({
        alertId,
        status: "closed",
    });

    await loadAlertsPage();

    state.message = "Alerta cerrada correctamente.";

    return alert;
}

async function createThreshold(payload) {
    const threshold = await notificationsFacade.createThreshold(payload);

    await loadAlertsPage();

    state.message = "Umbral creado correctamente.";

    return threshold;
}

async function sendAlert(alertId) {
    const deliveries = await notificationsFacade.sendAlert(alertId);

    await loadAlertsPage();

    state.message = "Alerta enviada por los canales activos.";

    return deliveries;
}

export function useNotificationsStore() {
    return {
        state: readonly(state),
        loadAlertsPage,
        createAlert,
        resolveAlert,
        closeAlert,
        createThreshold,
        sendAlert,
    };
}