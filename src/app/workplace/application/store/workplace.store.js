import { reactive, readonly } from "vue";
import { WorkplaceFacade } from "../services/workplace.facade";

const workplaceFacade = new WorkplaceFacade();

const state = reactive({
    workplace: null,
    sites: [],
    members: [],
    assignments: [],
    selectedType: "all",
    selectedSiteId: null,
    summary: null,
    loading: false,
    error: null,
});

async function loadWorkplace() {
    state.loading = true;
    state.error = null;

    try {
        state.workplace = await workplaceFacade.getWorkplace();
        state.sites = await workplaceFacade.getSites(state.selectedType);
        state.summary = await workplaceFacade.getSummary();

        if (!state.selectedSiteId && state.sites.length > 0) {
            state.selectedSiteId = state.sites[0].id;
        }

        await loadSelectedSiteRelations();
    } catch (error) {
        state.error = error.message || "No se pudo cargar el workplace.";
    } finally {
        state.loading = false;
    }
}

async function filterSitesByType(type) {
    state.selectedType = type;
    state.sites = await workplaceFacade.getSites(type);

    if (state.sites.length > 0) {
        state.selectedSiteId = state.sites[0].id;
        await loadSelectedSiteRelations();
    }
}

async function selectSite(siteId) {
    state.selectedSiteId = siteId;
    await loadSelectedSiteRelations();
}

async function loadSelectedSiteRelations() {
    if (!state.selectedSiteId) {
        state.members = [];
        state.assignments = [];
        return;
    }

    state.members = await workplaceFacade.getMembersBySite(state.selectedSiteId);
    state.assignments = await workplaceFacade.getAssignmentsBySite(state.selectedSiteId);
}

async function createSite(payload) {
    const site = await workplaceFacade.createSite(payload);

    state.sites = await workplaceFacade.getSites(state.selectedType);
    state.summary = await workplaceFacade.getSummary();
    state.selectedSiteId = site.id;

    await loadSelectedSiteRelations();

    return site;
}

async function assignMemberToSite(payload) {
    const member = await workplaceFacade.assignMemberToSite(payload);

    state.summary = await workplaceFacade.getSummary();
    await loadSelectedSiteRelations();

    return member;
}

async function assignDeviceToSite(payload) {
    const assignment = await workplaceFacade.assignDeviceToSite(payload);

    state.summary = await workplaceFacade.getSummary();
    await loadSelectedSiteRelations();

    return assignment;
}

function getSelectedSite() {
    return state.sites.find((site) => site.id === state.selectedSiteId) ?? null;
}

export function useWorkplaceStore() {
    return {
        state: readonly(state),
        loadWorkplace,
        filterSitesByType,
        selectSite,
        createSite,
        assignMemberToSite,
        assignDeviceToSite,
        getSelectedSite,
    };
}