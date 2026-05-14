<template>
  <nav class="bottom-navigation" aria-label="Navegación principal">
    <RouterLink
        v-for="item in items"
        :key="item.name"
        :to="item.to"
        class="bottom-navigation__item"
        :class="{ 'bottom-navigation__item--active': route.name === item.name }"
    >
      <span class="bottom-navigation__icon">
        {{ item.icon }}
      </span>

      <span class="bottom-navigation__label">
        {{ t(item.labelKey) }}
      </span>
    </RouterLink>
  </nav>
</template>

<script setup>
import { useRoute } from "vue-router";
import { useTranslation } from "../../../application/services/translation.service";

const route = useRoute();
const { t } = useTranslation();

const items = [
  {
    name: "dashboard",
    labelKey: "nav.dashboard",
    icon: "⌂",
    to: {
      name: "dashboard",
    },
  },
  {
    name: "sites",
    labelKey: "nav.sites",
    icon: "⌘",
    to: {
      name: "sites",
    },
  },
  {
    name: "devices",
    labelKey: "nav.devices",
    icon: "◈",
    to: {
      name: "devices",
    },
  },
  {
    name: "alerts",
    labelKey: "nav.alerts",
    icon: "!",
    to: {
      name: "alerts",
    },
  },
  {
    name: "reports",
    labelKey: "nav.reports",
    icon: "▣",
    to: {
      name: "reports",
    },
  },
  {
    name: "plans",
    labelKey: "nav.plans",
    icon: "◇",
    to: {
      name: "plans",
    },
  },
  {
    name: "support",
    labelKey: "nav.support",
    icon: "?",
    to: {
      name: "support",
    },
  },
  {
    name: "profile",
    labelKey: "nav.profile",
    icon: "●",
    to: {
      name: "profile",
    },
  },
];
</script>

<style scoped>
.bottom-navigation {
  position: fixed;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 30;
  display: grid;
  grid-template-columns: repeat(8, minmax(0, 1fr));
  gap: 8px;
  min-height: var(--bottom-nav-height);
  padding: 10px 14px;
  border-top: 1px solid var(--color-border);
  background: rgba(255, 255, 255, 0.94);
  backdrop-filter: blur(14px);
}

.bottom-navigation__item {
  display: inline-flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 4px;
  border: 2px solid transparent;
  border-radius: var(--radius-md);
  color: var(--color-text-muted);
  font-weight: 900;
  padding: 8px 6px;
}

.bottom-navigation__item--active {
  border-color: var(--color-text);
  color: var(--color-text);
  background: #ffffff;
}

.bottom-navigation__icon {
  font-size: 17px;
  line-height: 1;
}

.bottom-navigation__label {
  font-size: 10px;
  text-transform: uppercase;
  letter-spacing: 0.02em;
}

@media (max-width: 900px) {
  .bottom-navigation {
    grid-template-columns: repeat(4, minmax(0, 1fr));
    max-height: 132px;
    overflow-y: auto;
  }
}

@media (max-width: 520px) {
  .bottom-navigation {
    grid-template-columns: repeat(4, minmax(0, 1fr));
    gap: 6px;
    padding: 8px;
  }

  .bottom-navigation__label {
    font-size: 9px;
  }
}
</style>