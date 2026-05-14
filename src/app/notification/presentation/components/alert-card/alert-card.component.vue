<template>
  <article class="alert-card">
    <div>
      <h3>{{ alert.title }}</h3>
      <p>{{ alert.description }}</p>

      <div class="alert-card__meta">
        <span class="badge" :class="severityClass">
          {{ getSeverityLabel(alert.severity) }}
        </span>

        <span class="badge badge-primary">
          {{ getStatusLabel(alert.status) }}
        </span>
      </div>
    </div>

    <div class="alert-card__actions">
      <button
          v-if="alert.status !== 'resolved'"
          class="btn-secondary"
          type="button"
          @click="$emit('resolve', alert.id)"
      >
        {{ t('notifications.actions.resolve') }}
      </button>

      <button
          v-if="alert.status !== 'closed'"
          class="btn-secondary"
          type="button"
          @click="$emit('close', alert.id)"
      >
        {{ t('notifications.actions.close') }}
      </button>

      <button
          class="btn-secondary"
          type="button"
          @click="$emit('send', alert.id)"
      >
        {{ t('notifications.actions.send') }}
      </button>
    </div>
  </article>
</template>

<script setup>
import { computed } from "vue";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const props = defineProps({
  alert: {
    type: Object,
    required: true,
  },
});

defineEmits(["resolve", "close", "send"]);

const { t } = useTranslation();

const severityClass = computed(() => {
  const classes = {
    info: "badge-primary",
    warning: "badge-warning",
    critical: "badge-danger",
  };

  return classes[props.alert.severity] ?? "badge-primary";
});

function getSeverityLabel(severity) {
  const keys = {
    info: "notifications.severity.info",
    warning: "notifications.severity.warning",
    critical: "notifications.severity.critical",
  };

  return t(keys[severity] ?? "notifications.severity.unknown");
}

function getStatusLabel(status) {
  const keys = {
    open: "notifications.status.open",
    assigned: "notifications.status.assigned",
    resolved: "notifications.status.resolved",
    closed: "notifications.status.closed",
  };

  return t(keys[status] ?? "notifications.status.unknown");
}
</script>

<style scoped>
.alert-card {
  display: flex;
  justify-content: space-between;
  gap: 18px;
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  padding: 16px;
}

.alert-card h3 {
  color: var(--color-text);
  font-size: 18px;
  font-weight: 900;
  margin: 0 0 6px;
}

.alert-card p {
  color: var(--color-text-muted);
  line-height: 1.5;
  margin: 0;
}

.alert-card__meta,
.alert-card__actions {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
}

.alert-card__meta {
  margin-top: 12px;
}

.alert-card__actions {
  align-items: flex-start;
  justify-content: flex-end;
}

@media (max-width: 700px) {
  .alert-card {
    flex-direction: column;
  }

  .alert-card__actions {
    justify-content: flex-start;
  }
}
</style>