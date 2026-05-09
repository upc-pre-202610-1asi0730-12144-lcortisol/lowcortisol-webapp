<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('workplace.sites.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('workplace.sites.subtitle') }}
        </p>
      </div>

      <button class="btn-primary" type="button" @click="handleCreateSite">
        {{ t('workplace.sites.registerSite') }}
      </button>
    </section>

    <section class="sites-toolbar">
      <SiteFilter
          v-model="selectedType"
          @update:model-value="handleFilterChange"
      />
    </section>

    <section v-if="state.summary" class="grid grid-3 sites-summary">
      <UiCard :title="t('workplace.sites.registeredSites')" compact>
        <p class="summary-number">
          {{ state.summary.totalSites }}
        </p>
        <p class="summary-label">
          {{ t('workplace.sites.registeredSites') }}
        </p>
      </UiCard>

      <UiCard :title="t('workplace.sites.activeSites')" compact>
        <p class="summary-number">
          {{ state.summary.activeSites }}
        </p>
        <p class="summary-label">
          {{ t('workplace.sites.activeSites') }}
        </p>
      </UiCard>

      <UiCard :title="t('workplace.sites.assignedDevices')" compact>
        <p class="summary-number">
          {{ state.summary.totalDeviceAssignments }}
        </p>
        <p class="summary-label">
          {{ t('workplace.sites.assignedDevices') }}
        </p>
      </UiCard>
    </section>

    <section class="grid grid-2">
      <UiCard :title="t('workplace.sites.sitesList')">
        <div v-if="state.loading" class="empty-state">
          {{ t('workplace.sites.loading') }}
        </div>

        <div v-else-if="state.error" class="error-state">
          {{ state.error }}
        </div>

        <div v-else-if="state.sites.length === 0" class="empty-state">
          {{ t('workplace.sites.emptySites') }}
        </div>

        <div v-else class="site-list">
          <button
              v-for="site in state.sites"
              :key="site.id"
              type="button"
              class="site-card"
              :class="{ 'site-card--active': site.id === state.selectedSiteId }"
              @click="selectSite(site.id)"
          >
            <div>
              <h3 class="site-card__title">
                {{ site.name }}
              </h3>
              <p class="site-card__address">
                {{ site.address }}
              </p>
            </div>

            <div class="site-card__meta">
              <span class="badge badge-primary">
                {{ getSiteTypeLabel(site.type) }}
              </span>

              <span class="badge" :class="getStatusBadgeClass(site.status)">
                {{ getStatusLabel(site.status) }}
              </span>
            </div>
          </button>
        </div>
      </UiCard>

      <UiCard :title="t('workplace.sites.siteDetail')">
        <div v-if="selectedSite" class="site-detail">
          <div class="site-detail__header">
            <div>
              <h3 class="site-detail__title">
                {{ selectedSite.name }}
              </h3>
              <p class="site-detail__subtitle">
                {{ selectedSite.address }}
              </p>
            </div>

            <span class="badge badge-primary">
              {{ getSiteTypeLabel(selectedSite.type) }}
            </span>
          </div>

          <div class="metric-list">
            <div class="metric-row">
              <span>{{ t('workplace.sites.waterConsumption') }}</span>
              <strong>{{ selectedSite.waterConsumption }} L</strong>
            </div>

            <div class="metric-row">
              <span>{{ t('workplace.sites.gasConsumption') }}</span>
              <strong>{{ selectedSite.gasConsumption }} m³</strong>
            </div>

            <div class="metric-row">
              <span>{{ t('workplace.sites.activeSensors') }}</span>
              <strong>{{ selectedSite.activeSensors }}</strong>
            </div>

            <div class="metric-row">
              <span>{{ t('workplace.sites.activeIncidents') }}</span>
              <strong>{{ selectedSite.activeIncidents }}</strong>
            </div>
          </div>
        </div>

        <div v-else class="empty-state">
          {{ t('workplace.sites.selectSite') }}
        </div>
      </UiCard>
    </section>

    <section class="grid grid-2 sites-bottom">
      <UiCard :title="t('workplace.sites.assignedMembers')">
        <div v-if="state.members.length === 0" class="empty-state">
          {{ t('workplace.sites.noMembers') }}
        </div>

        <div v-else class="relation-list">
          <div
              v-for="member in state.members"
              :key="member.id"
              class="relation-item"
          >
            <div>
              <strong>{{ member.fullName }}</strong>
              <span>{{ member.email }}</span>
            </div>

            <span class="badge badge-success">
              {{ getRoleLabel(member.role) }}
            </span>
          </div>
        </div>

        <button class="btn-secondary relation-action" type="button" @click="handleAssignMember">
          {{ t('workplace.sites.assignMember') }}
        </button>
      </UiCard>

      <UiCard :title="t('workplace.sites.assignedDevicesTitle')">
        <div v-if="state.assignments.length === 0" class="empty-state">
          {{ t('workplace.sites.noDevices') }}
        </div>

        <div v-else class="relation-list">
          <div
              v-for="assignment in state.assignments"
              :key="assignment.id"
              class="relation-item"
          >
            <div>
              <strong>{{ assignment.deviceName }}</strong>
              <span>{{ getDeviceTypeLabel(assignment.deviceType) }}</span>
            </div>

            <span class="badge badge-primary">
              {{ assignment.status }}
            </span>
          </div>
        </div>

        <button class="btn-secondary relation-action" type="button" @click="handleAssignDevice">
          {{ t('workplace.sites.assignDevice') }}
        </button>
      </UiCard>
    </section>

    <section class="sites-bottom">
      <UiCard :title="t('workplace.sites.summary')">
        <div v-if="state.summary" class="metric-list">
          <div class="metric-row">
            <span>{{ t('workplace.sites.totalWater') }}</span>
            <strong>{{ state.summary.waterConsumption }} L</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('workplace.sites.totalGas') }}</span>
            <strong>{{ state.summary.gasConsumption }} m³</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('workplace.sites.registeredMembers') }}</span>
            <strong>{{ state.summary.totalMembers }}</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('workplace.sites.maintenanceSites') }}</span>
            <strong>{{ state.summary.maintenanceSites }}</strong>
          </div>
        </div>
      </UiCard>
    </section>
  </AppLayout>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";

import AppLayout from "../../../../shared/presentation/components/app-layout/app-layout.component.vue";
import UiCard from "../../../../shared/presentation/components/ui-card/ui-card.component.vue";
import SiteFilter from "../../components/site-filter/site-filter.component.vue";

import { useWorkplaceStore } from "../../../application/store/workplace.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const {
  state,
  loadWorkplace,
  filterSitesByType,
  selectSite,
  createSite,
  assignMemberToSite,
  assignDeviceToSite,
  getSelectedSite,
} = useWorkplaceStore();

const { t } = useTranslation();

const selectedType = ref("all");

const selectedSite = computed(() => getSelectedSite());

onMounted(async () => {
  await loadWorkplace();
});

async function handleFilterChange(type) {
  selectedType.value = type;
  await filterSitesByType(type);
}

async function handleCreateSite() {
  const nextNumber = state.summary ? state.summary.totalSites + 1 : state.sites.length + 1;

  await createSite({
    workplaceId: state.workplace?.id ?? "WORKPLACE-001",
    name: `${t('workplace.sites.newSite')} ${nextNumber}`,
    address: t('workplace.sites.pendingAddress'),
    type: "residential",
    status: "active",
  });
}

async function handleAssignMember() {
  if (!state.selectedSiteId) return;

  const nextNumber = state.members.length + 1;

  await assignMemberToSite({
    siteId: state.selectedSiteId,
    userId: `USR-AUTO-${nextNumber}`,
    fullName: `Responsable ${nextNumber}`,
    email: `responsable${nextNumber}@lowcortisol.com`,
    role: "operator",
  });
}

async function handleAssignDevice() {
  if (!state.selectedSiteId) return;

  const nextNumber = state.assignments.length + 1;

  await assignDeviceToSite({
    siteId: state.selectedSiteId,
    deviceId: `DEV-AUTO-${nextNumber}`,
    deviceName: `${t('workplace.deviceTypes.sensor')} ${nextNumber}`,
    deviceType: "sensor",
  });
}

function getStatusBadgeClass(status) {
  const classes = {
    active: "badge-success",
    inactive: "badge-danger",
    maintenance: "badge-warning",
  };

  return classes[status] ?? "badge-primary";
}

function getStatusLabel(status) {
  const keys = {
    active: "workplace.status.active",
    inactive: "workplace.status.inactive",
    maintenance: "workplace.status.maintenance",
  };

  return t(keys[status] ?? "workplace.status.unknown");
}

function getSiteTypeLabel(type) {
  const keys = {
    residential: "workplace.sites.residential",
    business: "workplace.sites.business",
    industrial: "workplace.sites.industrial",
  };

  return t(keys[type] ?? "workplace.sites.residential");
}

function getRoleLabel(role) {
  const keys = {
    owner: "workplace.roles.owner",
    admin: "workplace.roles.admin",
    operator: "workplace.roles.operator",
    viewer: "workplace.roles.viewer",
  };

  return t(keys[role] ?? "workplace.roles.unknown");
}

function getDeviceTypeLabel(type) {
  const keys = {
    sensor: "workplace.deviceTypes.sensor",
    valve: "workplace.deviceTypes.valve",
    hub: "workplace.deviceTypes.hub",
  };

  return t(keys[type] ?? "workplace.deviceTypes.device");
}
</script>

<style scoped>
.sites-toolbar {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 22px;
}

.sites-summary {
  margin-bottom: 20px;
}

.sites-bottom {
  margin-top: 20px;
}

.summary-number {
  color: var(--color-text);
  font-size: 34px;
  font-weight: 900;
  line-height: 1;
  margin: 0 0 8px;
}

.summary-label {
  color: var(--color-text-muted);
  font-size: 14px;
  margin: 0;
}

.empty-state,
.error-state {
  border: 1px dashed var(--color-border);
  border-radius: var(--radius-md);
  color: var(--color-text-muted);
  padding: 18px;
}

.error-state {
  color: var(--color-danger);
  background: #fef2f2;
  border-color: #fecaca;
}

.site-list {
  display: grid;
  gap: 14px;
}

.site-card {
  display: flex;
  justify-content: space-between;
  gap: 16px;
  width: 100%;
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  padding: 16px;
  text-align: left;
}

.site-card--active {
  border-color: var(--color-primary);
  background: var(--color-primary-light);
}

.site-card__title {
  color: var(--color-text);
  font-size: 18px;
  font-weight: 900;
  margin: 0 0 4px;
}

.site-card__address {
  color: var(--color-text-muted);
  font-size: 14px;
  margin: 0;
}

.site-card__meta {
  display: flex;
  align-items: flex-start;
  gap: 8px;
  flex-wrap: wrap;
  justify-content: flex-end;
}

.site-detail {
  display: grid;
  gap: 20px;
}

.site-detail__header {
  display: flex;
  justify-content: space-between;
  gap: 18px;
  align-items: flex-start;
}

.site-detail__title {
  font-size: 22px;
  font-weight: 900;
  margin: 0 0 6px;
}

.site-detail__subtitle {
  color: var(--color-text-muted);
  margin: 0;
}

.metric-list {
  display: grid;
}

.metric-row {
  display: flex;
  justify-content: space-between;
  gap: 18px;
  border-bottom: 1px solid var(--color-border);
  padding: 14px 0;
}

.metric-row:first-child {
  padding-top: 0;
}

.metric-row:last-child {
  border-bottom: none;
  padding-bottom: 0;
}

.metric-row span {
  color: var(--color-text-muted);
}

.metric-row strong {
  color: var(--color-text);
  font-weight: 900;
}

.relation-list {
  display: grid;
  gap: 12px;
}

.relation-item {
  display: flex;
  justify-content: space-between;
  gap: 14px;
  border-bottom: 1px solid var(--color-border);
  padding-bottom: 12px;
}

.relation-item:last-child {
  border-bottom: none;
  padding-bottom: 0;
}

.relation-item strong {
  display: block;
  color: var(--color-text);
  font-weight: 900;
  margin-bottom: 4px;
}

.relation-item span {
  color: var(--color-text-muted);
  font-size: 14px;
}

.relation-action {
  margin-top: 18px;
  width: 100%;
}

@media (max-width: 700px) {
  .sites-toolbar {
    justify-content: stretch;
  }

  .site-card,
  .site-detail__header,
  .relation-item {
    flex-direction: column;
  }

  .site-card__meta {
    justify-content: flex-start;
  }
}
</style>