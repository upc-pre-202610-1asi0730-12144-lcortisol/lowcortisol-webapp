<template>
  <div class="usage-chart">
    <div
        v-for="item in items"
        :key="item.id"
        class="usage-chart__row"
    >
      <div class="usage-chart__meta">
        <span>{{ getResourceLabel(item.resourceType) }}</span>
        <strong>{{ item.value }} {{ item.unit }}</strong>
      </div>

      <div class="usage-chart__track">
        <div
            class="usage-chart__bar"
            :style="{ width: `${getWidth(item.value)}%` }"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { useTranslation } from "../../../../shared/application/services/translation.service";

defineProps({
  items: {
    type: Array,
    default: () => [],
  },
});

const { t } = useTranslation();

function getWidth(value) {
  return Math.min(100, Math.max(8, Number(value || 0) / 4));
}

function getResourceLabel(resourceType) {
  const keys = {
    water: "monitoring.resource.water",
    gas: "monitoring.resource.gas",
  };

  return t(keys[resourceType] ?? "monitoring.resource.water");
}
</script>

<style scoped>
.usage-chart {
  display: grid;
  gap: 16px;
}

.usage-chart__row {
  display: grid;
  gap: 8px;
}

.usage-chart__meta {
  display: flex;
  justify-content: space-between;
  gap: 14px;
  color: var(--color-text-muted);
  font-weight: 800;
}

.usage-chart__meta strong {
  color: var(--color-text);
}

.usage-chart__track {
  height: 12px;
  border-radius: var(--radius-pill);
  background: var(--color-surface-soft);
  overflow: hidden;
}

.usage-chart__bar {
  height: 100%;
  border-radius: var(--radius-pill);
  background: var(--color-primary);
}
</style>