<template>
  <article class="ui-card" :class="{ 'ui-card--compact': compact }">
    <header v-if="title" class="ui-card__header">
      <h2 class="ui-card__title">
        {{ title }}
      </h2>
    </header>

    <slot />
  </article>
</template>

<script setup>
defineProps({
  title: {
    type: String,
    default: "",
  },
  eyebrow: {
    type: String,
    default: "",
  },
  compact: {
    type: Boolean,
    default: false,
  },
});
</script>

<style scoped>
.ui-card {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-lg);
  background: var(--color-surface);
  box-shadow: var(--shadow-soft);
  padding: 24px;
}

.ui-card--compact {
  padding: 22px;
}

.ui-card__header {
  margin-bottom: 16px;
}

.ui-card__title {
  color: var(--color-text);
  font-size: 22px;
  font-weight: 900;
  line-height: 1.1;
  margin: 0;
}
</style>