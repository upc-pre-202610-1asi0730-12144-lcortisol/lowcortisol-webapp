<template>
  <article class="article-card">
    <div>
      <h3>{{ article.title }}</h3>
      <p>{{ article.summary }}</p>
    </div>

    <div class="article-card__footer">
      <span class="badge badge-primary">
        {{ getCategoryLabel(article.category) }}
      </span>

      <span class="article-card__helpful">
        {{ article.helpfulCount }} {{ t('support.article.helpful') }}
      </span>
    </div>
  </article>
</template>

<script setup>
import { useTranslation } from "../../../../shared/application/services/translation.service";

defineProps({
  article: {
    type: Object,
    required: true,
  },
});

const { t } = useTranslation();

function getCategoryLabel(category) {
  const keys = {
    technical: "support.category.technical",
    billing: "support.category.billing",
    device: "support.category.device",
    alerts: "support.category.alerts",
  };

  return t(keys[category] ?? "support.category.support");
}
</script>

<style scoped>
.article-card {
  display: grid;
  gap: 14px;
  border-bottom: 1px solid var(--color-border);
  padding-bottom: 14px;
}

.article-card:last-child {
  border-bottom: none;
  padding-bottom: 0;
}

.article-card h3 {
  color: var(--color-text);
  font-size: 16px;
  font-weight: 900;
  margin: 0 0 4px;
}

.article-card p {
  color: var(--color-text-muted);
  line-height: 1.5;
  margin: 0;
}

.article-card__footer {
  display: flex;
  justify-content: space-between;
  gap: 12px;
  align-items: center;
}

.article-card__helpful {
  color: var(--color-text-muted);
  font-weight: 800;
}
</style>