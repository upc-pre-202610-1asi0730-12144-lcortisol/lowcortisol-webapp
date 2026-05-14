import { reactive, readonly } from "vue";
import { NotificationFacade } from "../services/notification.facade";

const notificationFacade = new NotificationFacade();

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

async function loadAlertPage() {
    state.loading = true;
    state.error = null;

    try {
        state.alerts = await notificationFacade.getAlerts();
        state.thresholds = await notificationFacade.getThresholds();
        state.incidents = await notificationFacade.getIncidents();
        state.channels = await notificationFacade.getChannels();
        state.deliveries = await notificationFacade.getDeliveries();
        state.summary = await notificationFacade.getSummary();
    } catch (error) {
        state.error = error.message || "No se pudieron cargar las alertas.";
    } finally {
        state.loading = false;
    }
}

async function createAlert(payload) {
    const alert = await notificationFacade.createAlert(payload);

    await loadAlertPage();

    state.message = "Alerta creada correctamente.";

    return alert;
}

async function resolveAlert(alertId) {
    const alert = await notificationFacade.updateAlert({
        alertId,
        status: "resolved",
    });

    await loadAlertPage();

    state.message = "Alerta resuelta correctamente.";

    return alert;
}

async function closeAlert(alertId) {
    const alert = await notificationFacade.updateAlert({
        alertId,
        status: "closed",
    });

    await loadAlertPage();

    state.message = "Alerta cerrada correctamente.";

    return alert;
}

async function createThreshold(payload) {
    const threshold = await notificationFacade.createThreshold(payload);

    await loadAlertPage();

    state.message = "Umbral creado correctamente.";

    return threshold;
}

async function sendAlert(alertId) {
    const deliveries = await notificationFacade.sendAlert(alertId);

    await loadAlertPage();

    state.message = "Alerta enviada por los canales activos.";

    return deliveries;
}

export function useNotificationStore() {
    return {
        state: readonly(state),
        loadAlertPage,
        createAlert,
        resolveAlert,
        closeAlert,
        createThreshold,
        sendAlert,
    };
}