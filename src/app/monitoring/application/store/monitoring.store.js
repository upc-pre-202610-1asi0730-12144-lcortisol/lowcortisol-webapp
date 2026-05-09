import { reactive, readonly } from "vue";
import { MonitoringFacade } from "../services/monitoring.facade";

const monitoringFacade = new MonitoringFacade();

const state = reactive({
    session: null,
    readings: [],
    anomalies: [],
    reports: [],
    summary: null,
    selectedResourceType: "all",
    loading: false,
    error: null,
});

async function loadDashboard() {
    state.loading = true;
    state.error = null;

    try {
        state.session = await monitoringFacade.getSession();
        state.readings = await monitoringFacade.getReadings({
            resourceType: state.selectedResourceType,
        });
        state.anomalies = await monitoringFacade.getAnomalies();
        state.reports = await monitoringFacade.getReports();
        state.summary = await monitoringFacade.getSummary();
    } catch (error) {
        state.error = error.message || "No se pudo cargar el monitoreo.";
    } finally {
        state.loading = false;
    }
}

async function filterReadingsByResource(resourceType) {
    state.selectedResourceType = resourceType;
    state.readings = await monitoringFacade.getReadings({ resourceType });
}

async function createRandomReading() {
    const isWater = Math.random() > 0.5;

    const reading = await monitoringFacade.createReading({
        siteId: isWater ? "SITE-001" : "SITE-003",
        sensorId: isWater ? "SEN-WATER-AUTO" : "SEN-GAS-AUTO",
        resourceType: isWater ? "water" : "gas",
        value: isWater
            ? Math.floor(180 + Math.random() * 180)
            : Math.floor(60 + Math.random() * 90),
        unit: isWater ? "L" : "m³",
    });

    await loadDashboard();

    return reading;
}

async function createReport(siteId = "SITE-001") {
    const report = await monitoringFacade.createReport({
        siteId,
        period: "weekly",
    });

    await loadDashboard();

    return report;
}

export function useMonitoringStore() {
    return {
        state: readonly(state),
        loadDashboard,
        filterReadingsByResource,
        createRandomReading,
        createReport,
    };
}