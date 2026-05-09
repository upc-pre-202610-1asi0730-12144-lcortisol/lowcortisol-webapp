<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('monitoring.reports.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('monitoring.reports.subtitle') }}
        </p>
      </div>

      <button class="btn-primary" type="button" @click="handleCreateReport">
        {{ t('monitoring.reports.generateReport') }}
      </button>
    </section>

    <section class="grid grid-3 reports-summary">
      <MetricCard
          :label="t('monitoring.reports.generatedReports')"
          :value="state.reports.length"
      />

      <MetricCard
          :label="t('monitoring.reports.availableReadings')"
          :value="state.readings.length"
      />

      <MetricCard
          :label="t('monitoring.reports.anomalies')"
          :value="state.anomalies.length"
      />
    </section>

    <section class="grid grid-2">
      <UiCard :title="t('monitoring.reports.consumptionReports')">
        <div v-if="state.reports.length === 0" class="empty-state">
          {{ t('monitoring.reports.noReports') }}
        </div>

        <div v-else class="report-list">
          <article
              v-for="report in state.reports"
              :key="report.id"
              class="report-card"
          >
            <div>
              <h3>{{ getPeriodLabel(report.period) }}</h3>
              <p>{{ t('monitoring.reports.site') }}: {{ report.siteId }}</p>
            </div>

            <div class="report-metrics">
              <span>{{ report.totalWater }} L</span>
              <span>{{ report.totalGas }} m³</span>
              <span>{{ report.anomaliesCount }} {{ t('monitoring.reports.anomalies').toLowerCase() }}</span>
            </div>
          </article>
        </div>
      </UiCard>

      <UiCard :title="t('monitoring.reports.baseReadings')">
        <div class="reading-list">
          <div
              v-for="reading in state.readings"
              :key="reading.id"
              class="reading-item"
          >
            <div>
              <strong>{{ getResourceLabel(reading.resourceType) }}</strong>
              <span>{{ reading.sensorId }}</span>
            </div>

            <span class="badge" :class="getStatusClass(reading.status)">
              {{ reading.value }} {{ reading.unit }}
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

import { useMonitoringStore } from "../../../application/store/monitoring.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const {
  state,
  loadDashboard,
  createReport,
} = useMonitoringStore();

const { t } = useTranslation();

onMounted(async () => {
  await loadDashboard();
});

async function handleCreateReport() {
  await createReport("SITE-001");
}

function getStatusClass(status) {
  const classes = {
    normal: "badge-success",
    warning: "badge-warning",
    critical: "badge-danger",
  };

  return classes[status] ?? "badge-primary";
}

function getResourceLabel(resourceType) {
  const keys = {
    water: "monitoring.resources.water",
    gas: "monitoring.resources.gas",
  };

  return t(keys[resourceType] ?? "monitoring.resources.water");
}

function getPeriodLabel(period) {
  const keys = {
    daily: "monitoring.periods.daily",
    weekly: "monitoring.periods.weekly",
    monthly: "monitoring.periods.monthly",
  };

  return t(keys[period] ?? "monitoring.periods.custom");
}
</script>

<style scoped>
.reports-summary {
  margin-bottom: 20px;
}

.empty-state {
  border: 1px dashed var(--color-border);
  border-radius: var(--radius-md);
  color: var(--color-text-muted);
  padding: 18px;
}

.report-list,
.reading-list {
  display: grid;
  gap: 14px;
}

.report-card,
.reading-item {
  display: flex;
  justify-content: space-between;
  gap: 16px;
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  padding: 16px;
}

.report-card h3 {
  color: var(--color-text);
  font-weight: 900;
  margin: 0 0 4px;
}

.report-card p,
.reading-item span {
  color: var(--color-text-muted);
  margin: 0;
}

.report-metrics {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
  justify-content: flex-end;
}

.report-metrics span {
  border-radius: var(--radius-pill);
  background: var(--color-primary-light);
  color: var(--color-primary);
  font-size: 12px;
  font-weight: 900;
  padding: 6px 10px;
}

.reading-item strong {
  display: block;
  color: var(--color-text);
  font-weight: 900;
  margin-bottom: 4px;
}

@media (max-width: 700px) {
  .report-card,
  .reading-item {
    flex-direction: column;
  }

  .report-metrics {
    justify-content: flex-start;
  }
}
</style>