<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('notifications.alerts.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('notifications.alerts.subtitle') }}
        </p>
      </div>

      <button class="btn-primary" type="button" @click="handleCreateAlert">
        {{ t('notifications.alerts.createAlert') }}
      </button>
    </section>

    <section class="grid grid-3 alerts-summary">
      <UiCard :title="t('notifications.alerts.registeredAlerts')" compact>
        <p class="summary-number">{{ state.summary.totalAlerts }}</p>
        <p class="summary-label">{{ t('notifications.alerts.registeredAlerts') }}</p>
      </UiCard>

      <UiCard :title="t('notifications.alerts.criticalAlerts')" compact>
        <p class="summary-number">{{ state.summary.criticalAlerts }}</p>
        <p class="summary-label">{{ t('notifications.alerts.criticalAlerts') }}</p>
      </UiCard>

      <UiCard :title="t('notifications.alerts.openIncidents')" compact>
        <p class="summary-number">{{ state.summary.openIncidents }}</p>
        <p class="summary-label">{{ t('notifications.alerts.openIncidents') }}</p>
      </UiCard>
    </section>

    <section class="grid grid-2">
      <UiCard :title="t('notifications.alerts.activeAlerts')">
        <div v-if="state.loading" class="empty-state">
          {{ t('notifications.alerts.loading') }}
        </div>

        <div v-else-if="state.error" class="error-state">
          {{ state.error }}
        </div>

        <div v-else-if="state.alerts.length === 0" class="empty-state">
          {{ t('notifications.alerts.noAlerts') }}
        </div>

        <div v-else class="alert-list">
          <AlertCard
              v-for="alert in state.alerts"
              :key="alert.id"
              :alert="alert"
              @resolve="resolveAlert"
              @close="closeAlert"
              @send="sendAlert"
          />
        </div>
      </UiCard>

      <UiCard :title="t('notifications.alerts.configuredThresholds')">
        <div v-if="state.thresholds.length === 0" class="empty-state">
          {{ t('notifications.alerts.noThresholds') }}
        </div>

        <div v-else class="threshold-list">
          <ThresholdCard
              v-for="threshold in state.thresholds"
              :key="threshold.id"
              :threshold="threshold"
          />
        </div>

        <button class="btn-secondary full-button" type="button" @click="handleCreateThreshold">
          {{ t('notifications.alerts.createThreshold') }}
        </button>
      </UiCard>
    </section>

    <section class="grid grid-2 alerts-bottom">
      <UiCard :title="t('notifications.alerts.incidents')">
        <div v-if="state.incidents.length === 0" class="empty-state">
          {{ t('notifications.alerts.noIncidents') }}
        </div>

        <div v-else class="simple-list">
          <div
              v-for="incident in state.incidents"
              :key="incident.id"
              class="simple-item"
          >
            <div>
              <strong>{{ incident.title }}</strong>
              <span>{{ incident.description }}</span>
            </div>

            <span class="badge" :class="getPriorityClass(incident.priority)">
              {{ getPriorityLabel(incident.priority) }}
            </span>
          </div>
        </div>
      </UiCard>

      <UiCard :title="t('notifications.alerts.channelsAndDeliveries')">
        <div class="metric-list">
          <div class="metric-row">
            <span>{{ t('notifications.alerts.enabledChannels') }}</span>
            <strong>{{ state.summary.channelsEnabled }}</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('notifications.alerts.sentAlerts') }}</span>
            <strong>{{ state.summary.deliveriesSent }}</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('notifications.alerts.activeThresholds') }}</span>
            <strong>{{ state.summary.activeThresholds }}</strong>
          </div>
        </div>
      </UiCard>
    </section>
  </AppLayout>
</template>

<script setup>
import { onMounted } from "vue";

import AppLayout from "../../../../shared/presentation/components/app-layout/app-layout.component.vue";
import UiCard from "../../../../shared/presentation/components/ui-card/ui-card.component.vue";

import AlertCard from "../../components/alert-card/alert-card.component.vue";
import ThresholdCard from "../../components/threshold-card/threshold-card.component.vue";

import { useNotificationStore } from "../../../application/store/notification.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const {
  state,
  loadAlertPage,
  createAlert,
  resolveAlert,
  closeAlert,
  createThreshold,
  sendAlert,
} = useNotificationStore();

const { t } = useTranslation();

onMounted(async () => {
  await loadAlertPage();
});

async function handleCreateAlert() {
  const nextNumber = state.summary.totalAlerts + 1;
  const isGas = nextNumber % 2 === 0;

  await createAlert({
    siteId: "SITE-001",
    sensorId: `SEN-AUTO-${nextNumber}`,
    resourceType: isGas ? "gas" : "water",
    title: `${t('notifications.alerts.automaticAlert')} ${nextNumber}`,
    description: t('notifications.alerts.automaticDescription'),
    severity: isGas ? "critical" : "warning",
  });
}

async function handleCreateThreshold() {
  const nextNumber = state.summary.totalThresholds + 1;
  const isGas = nextNumber % 2 === 0;

  await createThreshold({
    siteId: "SITE-001",
    sensorId: `SEN-THR-${nextNumber}`,
    resourceType: isGas ? "gas" : "water",
    warningLimit: isGas ? 90 : 260,
    criticalLimit: isGas ? 120 : 320,
    unit: isGas ? "m³" : "L",
  });
}

function getPriorityClass(priority) {
  const classes = {
    low: "badge-primary",
    medium: "badge-warning",
    high: "badge-danger",
    critical: "badge-danger",
  };

  return classes[priority] ?? "badge-primary";
}

function getPriorityLabel(priority) {
  const keys = {
    low: "notifications.priority.low",
    medium: "notifications.priority.medium",
    high: "notifications.priority.high",
    critical: "notifications.priority.critical",
  };

  return t(keys[priority] ?? "notifications.priority.unknown");
}
</script>

<style scoped>
.alerts-summary {
  margin-bottom: 20px;
}

.alerts-bottom {
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

.alert-list,
.threshold-list,
.simple-list {
  display: grid;
  gap: 14px;
}

.full-button {
  width: 100%;
  margin-top: 18px;
}

.simple-item {
  display: flex;
  justify-content: space-between;
  gap: 14px;
  border-bottom: 1px solid var(--color-border);
  padding-bottom: 14px;
}

.simple-item:last-child {
  border-bottom: none;
  padding-bottom: 0;
}

.simple-item strong {
  display: block;
  color: var(--color-text);
  margin-bottom: 4px;
}

.simple-item span {
  color: var(--color-text-muted);
  margin: 0;
}

.metric-list {
  display: grid;
}

.metric-row {
  display: flex;
  justify-content: space-between;
  gap: 16px;
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

@media (max-width: 700px) {
  .simple-item,
  .metric-row {
    flex-direction: column;
  }
}
</style>