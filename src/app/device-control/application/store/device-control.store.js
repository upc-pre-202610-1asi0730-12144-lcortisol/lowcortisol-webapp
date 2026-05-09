import { reactive, readonly } from "vue";
import { DeviceControlFacade } from "../services/device-control.facade";

const deviceControlFacade = new DeviceControlFacade();

const state = reactive({
    devices: [],
    sensors: [],
    valves: [],
    commands: [],
    summary: {
        totalDevices: 0,
        onlineDevices: 0,
        totalSensors: 0,
        activeSensors: 0,
        totalValves: 0,
        openValves: 0,
        closedValves: 0,
        commandsExecuted: 0,
    },
    selectedDeviceId: null,
    loading: false,
    error: null,
    message: "",
});

async function loadDevicePage() {
    state.loading = true;
    state.error = null;

    try {
        state.devices = await deviceControlFacade.getDevices();
        state.sensors = await deviceControlFacade.getSensors();
        state.valves = await deviceControlFacade.getValves();
        state.commands = await deviceControlFacade.getCommands();
        state.summary = await deviceControlFacade.getSummary();

        if (!state.selectedDeviceId && state.devices.length > 0) {
            state.selectedDeviceId = state.devices[0].id;
        }
    } catch (error) {
        state.error = error.message || "No se pudo cargar la gestión de dispositivos.";
    } finally {
        state.loading = false;
    }
}

function selectDevice(deviceId) {
    state.selectedDeviceId = deviceId;
}

function getSelectedDevice() {
    return state.devices.find((device) => device.id === state.selectedDeviceId) ?? null;
}

async function createDevice(payload) {
    const device = await deviceControlFacade.createDevice(payload);

    await loadDevicePage();

    state.selectedDeviceId = device.id;
    state.message = "Dispositivo creado correctamente.";

    return device;
}

async function linkSensor(payload) {
    const sensor = await deviceControlFacade.linkSensor(payload);

    await loadDevicePage();

    state.message = "Sensor vinculado correctamente.";

    return sensor;
}

async function closeValve(valveId) {
    const valve = await deviceControlFacade.updateValveStatus({
        valveId,
        status: "closed",
        openingPercentage: 0,
    });

    await loadDevicePage();

    state.message = "Válvula cerrada correctamente.";

    return valve;
}

async function openValve(valveId) {
    const valve = await deviceControlFacade.updateValveStatus({
        valveId,
        status: "open",
        openingPercentage: 100,
    });

    await loadDevicePage();

    state.message = "Válvula abierta correctamente.";

    return valve;
}

async function executeSyncCommand(deviceId) {
    const command = await deviceControlFacade.executeCommand({
        deviceId,
        commandType: "sync",
        payload: {
            source: "devices-page",
        },
    });

    await loadDevicePage();

    state.message = "Comando ejecutado correctamente.";

    return command;
}

export function useDeviceControlStore() {
    return {
        state: readonly(state),
        loadDevicePage,
        selectDevice,
        getSelectedDevice,
        createDevice,
        linkSensor,
        closeValve,
        openValve,
        executeSyncCommand,
    };
}