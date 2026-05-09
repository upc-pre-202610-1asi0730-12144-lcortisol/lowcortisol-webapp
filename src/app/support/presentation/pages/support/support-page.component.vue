<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('support.page.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('support.page.subtitle') }}
        </p>
      </div>

      <button class="btn-primary" type="button" @click="handleCreateTicket">
        {{ t('support.page.createTicket') }}
      </button>
    </section>

    <section class="grid grid-3 support-summary">
      <UiCard :title="t('support.page.tickets')" compact>
        <p class="summary-number">{{ state.summary.totalTickets }}</p>
        <p class="summary-label">{{ t('support.page.registeredTickets') }}</p>
      </UiCard>

      <UiCard :title="t('support.page.agents')" compact>
        <p class="summary-number">{{ state.summary.availableAgents }}</p>
        <p class="summary-label">{{ t('support.page.availableAgents') }}</p>
      </UiCard>

      <UiCard :title="t('support.page.help')" compact>
        <p class="summary-number">{{ state.summary.totalArticles }}</p>
        <p class="summary-label">{{ t('support.page.publishedArticles') }}</p>
      </UiCard>
    </section>

    <section class="grid grid-2">
      <UiCard :title="t('support.page.supportTickets')">
        <div v-if="state.loading" class="empty-state">
          {{ t('support.page.loadingTickets') }}
        </div>

        <div v-else-if="state.error" class="error-state">
          {{ state.error }}
        </div>

        <div v-else class="ticket-list">
          <SupportTicketCard
              v-for="ticket in state.tickets"
              :key="ticket.id"
              :ticket="ticket"
              :active="ticket.id === state.selectedTicketId"
              @click="selectTicket(ticket.id)"
          />
        </div>
      </UiCard>

      <UiCard :title="t('support.page.ticketDetail')">
        <div v-if="selectedTicket" class="ticket-detail">
          <div class="ticket-detail__header">
            <div>
              <h3>{{ selectedTicket.title }}</h3>
              <p>{{ selectedTicket.description }}</p>
            </div>

            <span class="badge badge-primary">
              {{ getStatusLabel(selectedTicket.status) }}
            </span>
          </div>

          <div class="conversation">
            <div
                v-for="message in selectedConversation?.messages ?? []"
                :key="message.id"
                class="message"
            >
              <strong>{{ getSenderTypeLabel(message.senderType) }}</strong>
              <p>{{ message.content }}</p>
            </div>
          </div>

          <form class="message-form" @submit.prevent="handleSendMessage">
            <input
                v-model="messageContent"
                type="text"
                :placeholder="t('support.page.writeMessage')"
            />

            <button class="btn-secondary" type="submit">
              {{ t('support.page.send') }}
            </button>
          </form>

          <div class="ticket-actions">
            <button class="btn-secondary" type="button" @click="resolveTicket(selectedTicket.id)">
              {{ t('support.page.resolve') }}
            </button>

            <button class="btn-secondary" type="button" @click="closeTicket(selectedTicket.id)">
              {{ t('support.page.close') }}
            </button>
          </div>
        </div>

        <div v-else class="empty-state">
          {{ t('support.page.selectTicket') }}
        </div>
      </UiCard>
    </section>

    <section class="support-bottom">
      <UiCard :title="t('support.page.helpArticles')">
        <div class="article-list">
          <KnowledgeArticleCard
              v-for="article in state.articles"
              :key="article.id"
              :article="article"
          />
        </div>
      </UiCard>
    </section>
  </AppLayout>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";

import AppLayout from "../../../../shared/presentation/components/app-layout/app-layout.component.vue";
import UiCard from "../../../../shared/presentation/components/ui-card/ui-card.component.vue";

import SupportTicketCard from "../../components/support-ticket-card/support-ticket-card.component.vue";
import KnowledgeArticleCard from "../../components/knowledge-article-card/knowledge-article-card.component.vue";

import { useSupportStore } from "../../../application/store/support.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const {
  state,
  loadSupportPage,
  selectTicket,
  getSelectedTicket,
  getSelectedConversation,
  createTicket,
  resolveTicket,
  closeTicket,
  sendMessage,
} = useSupportStore();

const { t } = useTranslation();

const messageContent = ref("");

const selectedTicket = computed(() => getSelectedTicket());
const selectedConversation = computed(() => getSelectedConversation());

onMounted(async () => {
  await loadSupportPage();
});

async function handleCreateTicket() {
  const nextNumber = state.summary.totalTickets + 1;

  await createTicket({
    userId: "USR-001",
    siteId: "SITE-001",
    title: `${t('support.page.supportRequest')} ${nextNumber}`,
    description: t('support.page.supportRequestDescription'),
    category: nextNumber % 2 === 0 ? "device" : "technical",
  });
}

async function handleSendMessage() {
  if (!messageContent.value.trim()) return;

  await sendMessage(messageContent.value);
  messageContent.value = "";
}

function getStatusLabel(status) {
  const keys = {
    open: "support.status.open",
    assigned: "support.status.assigned",
    resolved: "support.status.resolved",
    closed: "support.status.closed",
  };

  return t(keys[status] ?? "support.status.unknown");
}

function getSenderTypeLabel(senderType) {
  const keys = {
    user: "support.sender.user",
    agent: "support.sender.agent",
    system: "support.sender.system",
  };

  return t(keys[senderType] ?? "support.sender.unknown");
}
</script>

<style scoped>
.support-summary {
  margin-bottom: 20px;
}

.support-bottom {
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

.ticket-list,
.article-list,
.conversation {
  display: grid;
  gap: 14px;
}

.ticket-detail {
  display: grid;
  gap: 18px;
}

.ticket-detail__header {
  display: flex;
  justify-content: space-between;
  gap: 16px;
}

.ticket-detail__header h3 {
  color: var(--color-text);
  font-size: 20px;
  font-weight: 900;
  margin: 0 0 6px;
}

.ticket-detail__header p {
  color: var(--color-text-muted);
  margin: 0;
}

.message {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  padding: 14px;
}

.message strong {
  color: var(--color-primary);
}

.message p {
  color: var(--color-text-muted);
  margin: 6px 0 0;
}

.message-form {
  display: grid;
  grid-template-columns: 1fr auto;
  gap: 10px;
}

.message-form input {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  color: var(--color-text);
  padding: 14px 16px;
  outline: none;
}

.ticket-actions {
  display: flex;
  gap: 10px;
}

@media (max-width: 700px) {
  .message-form,
  .ticket-detail__header {
    grid-template-columns: 1fr;
    flex-direction: column;
  }
}
</style>