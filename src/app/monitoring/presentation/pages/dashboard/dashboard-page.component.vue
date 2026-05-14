<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('monitoring.dashboard.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('monitoring.dashboard.subtitle') }}
        </p>
      </div>

      <button class="btn-primary" type="button" @click="createRandomReading">
        {{ t('monitoring.dashboard.simulateReading') }}
      </button>
    </section>

    <section v-if="state.summary" class="grid grid-3 dashboard-summary">
      <MetricCard
          :label="t('monitoring.dashboard.waterMonitored')"
          :value="`${state.summary.totalWater} L`"
      />

      <MetricCard
          :label="t('monitoring.dashboard.gasMonitored')"
          :value="`${state.summary.totalGas} m³`"
      />

      <MetricCard
          :label="t('monitoring.dashboard.criticalAnomalies')"
          :value="state.summary.criticalAnomalies"
      />
    </section>

    <section class="grid grid-2">
      <UiCard :title="t('monitoring.dashboard.recentReadings')">
        <UsageChart :items="state.readings" />
      </UiCard>

      <UiCard :title="t('monitoring.dashboard.activeSession')">
        <div v-if="state.session" class="metric-list">
          <div class="metric-row">
            <span>{{ t('monitoring.dashboard.status') }}</span>
            <strong>
              {{ state.session.isActive ? t('monitoring.dashboard.active') : t('monitoring.dashboard.finished') }}
            </strong>
          </div>

          <div class="metric-row">
            <span>{{ t('monitoring.dashboard.totalReadings') }}</span>
            <strong>{{ state.session.totalReadings }}</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('monitoring.dashboard.detectedAnomalies') }}</span>
            <strong>{{ state.session.totalAnomalies }}</strong>
          </div>
        </div>
      </UiCard>
    </section>

    <section class="dashboard-bottom">
      <UiCard :title="t('monitoring.dashboard.anomalies')">
        <div v-if="state.anomalies.length === 0" class="empty-state">
          {{ t('monitoring.dashboard.noAnomalies') }}
        </div>

        <div v-else class="list">
          <div
              v-for="anomaly in state.anomalies"
              :key="anomaly.id"
              class="list-item"
          >
            <div>
              <strong>{{ getSeverityLabel(anomaly.severity) }}</strong>
              <span>{{ anomaly.description }}</span>
            </div>

            <span
                class="badge"
                :class="anomaly.severity === 'critical' ? 'badge-danger' : 'badge-warning'"
            >
              {{ getStatusLabel(anomaly.status) }}
            </span>
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

import MetricCard from "../../components/metric-card/metric-card.component.vue";
import UsageChart from "../../components/usage-chart/usage-chart.component.vue";

import { useMonitoringStore } from "../../../application/store/monitoring.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const {
  state,
  loadDashboard,
  createRandomReading,
} = useMonitoringStore();

const { t } = useTranslation();

onMounted(async () => {
  await loadDashboard();
});

function getSeverityLabel(severity) {
  const keys = {
    warning: "monitoring.status.warning",
    critical: "monitoring.status.critical",
  };

  return t(keys[severity] ?? "monitoring.status.warning");
}

function getStatusLabel(status) {
  const keys = {
    open: "monitoring.status.open",
    resolved: "monitoring.status.resolved",
    closed: "monitoring.status.closed",
  };

  return t(keys[status] ?? "monitoring.status.open");
}
</script>

<style scoped>
.dashboard-summary {
  margin-bottom: 20px;
}

.dashboard-bottom {
  margin-top: 20px;
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

.metric-row span,
.list-item span {
  color: var(--color-text-muted);
}

.metric-row strong,
.list-item strong {
  color: var(--color-text);
  font-weight: 900;
}

.empty-state {
  border: 1px dashed var(--color-border);
  border-radius: var(--radius-md);
  color: var(--color-text-muted);
  padding: 18px;
}

.list {
  display: grid;
  gap: 14px;
}

.list-item {
  display: flex;
  justify-content: space-between;
  gap: 14px;
  border-bottom: 1px solid var(--color-border);
  padding-bottom: 14px;
}

.list-item:last-child {
  border-bottom: none;
  padding-bottom: 0;
}

.list-item strong {
  display: block;
  margin-bottom: 4px;
}

@media (max-width: 700px) {
  .list-item,
  .metric-row {
    flex-direction: column;
  }
}
</style>