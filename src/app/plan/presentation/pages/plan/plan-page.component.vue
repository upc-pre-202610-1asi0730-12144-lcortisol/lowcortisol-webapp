<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('plans.page.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('plans.page.subtitle') }}
        </p>
      </div>

      <button
          v-if="state.subscription"
          class="btn-primary"
          type="button"
          @click="cancelSubscription"
      >
        {{ t('plans.page.cancelSubscription') }}
      </button>
    </section>

    <div v-if="!state.subscription" class="subscription-warning">
      {{ t('plans.page.noActiveSubscription') }}
    </div>

    <section class="grid grid-3 plans-summary">
      <UiCard :title="t('plans.page.activePlan')" compact>
        <p class="summary-number summary-text">
          {{ state.summary.activePlanName }}
        </p>
        <p class="summary-label">
          {{ getSubscriptionStatusLabel(state.subscription?.status) }}
        </p>
      </UiCard>

      <UiCard :title="t('plans.page.capacity')" compact>
        <p class="summary-number">
          {{ state.summary.maxSites }}
        </p>
        <p class="summary-label">
          {{ t('plans.page.allowedSites') }}
        </p>
      </UiCard>

      <UiCard :title="t('plans.page.payments')" compact>
        <p class="summary-number">
          S/ {{ Number(state.summary.totalPaid || 0).toFixed(2) }}
        </p>
        <p class="summary-label">
          {{ t('plans.page.totalPaid') }}
        </p>
      </UiCard>
    </section>

    <section class="plans-list">
      <PlanCard
          v-for="plan in state.plans"
          :key="plan.id"
          :plan="plan"
          :active="plan.id === state.subscription?.planId"
          @buy="openCheckout"
      />
    </section>

    <Teleport to="body">
      <div
          v-if="selectedPlan"
          class="checkout-overlay"
          @click.self="closeCheckout"
      >
        <aside class="checkout-drawer">
          <div class="checkout-handle"></div>

          <header class="checkout-header">
            <div>
              <h2>{{ t('plans.page.checkoutTitle') }}</h2>
              <p>{{ t('plans.page.checkoutSubtitle') }}</p>
            </div>

            <button class="checkout-close" type="button" @click="closeCheckout">
              ×
            </button>
          </header>

          <div class="selected-plan">
            <span>{{ t('plans.page.selectedPlan') }}</span>
            <strong>
              {{ selectedPlan.name }} · S/ {{ Number(selectedPlan.price).toFixed(2) }}
            </strong>
          </div>

          <form class="checkout-form" @submit.prevent="handlePurchase">
            <label class="form-field">
              <span>{{ t('plans.page.cardholderName') }}</span>
              <input
                  v-model="checkout.cardholderName"
                  type="text"
                  placeholder="Jean Loa"
              />
            </label>

            <label class="form-field">
              <span>{{ t('plans.page.cardNumber') }}</span>
              <input
                  :value="checkout.cardNumber"
                  type="text"
                  inputmode="numeric"
                  autocomplete="cc-number"
                  maxlength="19"
                  placeholder="4242 4242 4242 4242"
                  @input="handleCardNumberInput"
              />
            </label>

            <div class="form-grid">
              <label class="form-field">
                <span>{{ t('plans.page.expirationDate') }}</span>
                <input
                    :value="checkout.expirationDate"
                    type="text"
                    inputmode="numeric"
                    autocomplete="cc-exp"
                    maxlength="5"
                    placeholder="12/29"
                    @input="handleExpirationDateInput"
                />
              </label>

              <label class="form-field">
                <span>{{ t('plans.page.securityCode') }}</span>
                <input
                    :value="checkout.securityCode"
                    type="password"
                    inputmode="numeric"
                    autocomplete="cc-csc"
                    maxlength="3"
                    placeholder="123"
                    @input="handleSecurityCodeInput"
                />
              </label>
            </div>

            <div class="checkout-actions">
              <button class="btn-secondary" type="button" @click="closeCheckout">
                {{ t('plans.page.closeForm') }}
              </button>

              <button class="btn-primary" type="submit">
                {{ t('plans.page.confirmPurchase') }}
              </button>
            </div>
          </form>
        </aside>
      </div>
    </Teleport>

    <section class="grid grid-2 plans-bottom">
      <UiCard :title="t('plans.page.paymentHistory')">
        <div v-if="state.payments.length === 0" class="empty-state">
          {{ t('plans.page.noPayments') }}
        </div>

        <div v-else class="simple-list">
          <div
              v-for="payment in state.payments"
              :key="payment.id"
              class="simple-item"
          >
            <div>
              <strong>S/ {{ Number(payment.amount || 0).toFixed(2) }}</strong>
              <span>{{ payment.method }}</span>
            </div>

            <span class="badge badge-success">
              {{ getPaymentStatusLabel(payment.status) }}
            </span>
          </div>
        </div>
      </UiCard>

      <UiCard :title="t('plans.page.serviceRequest')">
        <div v-if="state.serviceRequest.length === 0" class="empty-state">
          {{ t('plans.page.noRequest') }}
        </div>

        <div v-else class="simple-list">
          <div
              v-for="request in state.serviceRequest"
              :key="request.id"
              class="simple-item"
          >
            <div>
              <strong>{{ getRequestTypeLabel(request.type) }}</strong>
              <span>{{ request.description }}</span>
            </div>

            <span class="badge badge-primary">
              {{ getRequestStatusLabel(request.status) }}
            </span>
          </div>
        </div>
      </UiCard>
    </section>
  </AppLayout>
</template>

<script setup>
import { onMounted, reactive, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

import AppLayout from "../../../../shared/presentation/components/app-layout/app-layout.component.vue";
import UiCard from "../../../../shared/presentation/components/ui-card/ui-card.component.vue";

import { usePlanStore } from "../../../application/store/plan.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const route = useRoute();
const router = useRouter();

const {
  state,
  loadPlanPage,
  subscribeToPlan,
  changePlan,
  cancelSubscription,
} = usePlanStore();

const { t } = useTranslation();

const selectedPlan = ref(null);

const checkout = reactive({
  cardholderName: "",
  cardNumber: "",
  expirationDate: "",
  securityCode: "",
});

onMounted(async () => {
  await loadPlanPage();
  await applyLandingSelectedPlan();
});

async function applyLandingSelectedPlan() {
  const planIdFromQuery = route.query.plan ? String(route.query.plan) : "";
  const planCodeFromQuery = route.query.code ? String(route.query.code) : "";

  const pendingPlanId =
      planIdFromQuery || localStorage.getItem("lowcortisol.pendingPlanId");

  const pendingPlanCode =
      planCodeFromQuery || localStorage.getItem("lowcortisol.pendingPlanCode");

  if (!pendingPlanId) {
    return;
  }

  const selected = state.plans.find((plan) => plan.id === pendingPlanId);

  if (!selected) {
    localStorage.removeItem("lowcortisol.pendingPlanId");
    localStorage.removeItem("lowcortisol.pendingPlanCode");
    return;
  }

  try {
    if (state.subscription?.planId === pendingPlanId) {
      clearPendingPlan();
      return;
    }

    if (state.subscription?.id) {
      await changePlan(pendingPlanId);
    } else {
      await subscribeToPlan(pendingPlanId);
    }

    clearPendingPlan();

    await router.replace({
      name: "plans",
    });
  } catch (error) {
    console.error("Could not apply landing selected plan:", {
      planId: pendingPlanId,
      code: pendingPlanCode,
      error,
    });
  }
}

function clearPendingPlan() {
  localStorage.removeItem("lowcortisol.pendingPlanId");
  localStorage.removeItem("lowcortisol.pendingPlanCode");
}

function openCheckout(plan) {
  selectedPlan.value = plan;
}

function closeCheckout() {
  selectedPlan.value = null;
  checkout.cardholderName = "";
  checkout.cardNumber = "";
  checkout.expirationDate = "";
  checkout.securityCode = "";
}

function onlyNumbers(value) {
  return String(value || "").replace(/\D/g, "");
}

function formatCardNumber(value) {
  return onlyNumbers(value)
      .slice(0, 16)
      .replace(/(.{4})/g, "$1 ")
      .trim();
}

function formatExpirationDate(value) {
  const numbers = onlyNumbers(value).slice(0, 4);

  if (numbers.length === 0) {
    return "";
  }

  if (numbers.length === 1) {
    const firstDigit = Number(numbers);

    if (firstDigit > 1) {
      return `0${firstDigit}`;
    }

    return numbers;
  }

  let month = numbers.slice(0, 2);
  let monthNumber = Number(month);

  if (monthNumber < 1) {
    month = "01";
  }

  if (monthNumber > 12) {
    month = "12";
  }

  if (numbers.length <= 2) {
    return month;
  }

  const yearInput = numbers.slice(2);
  const currentYearShort = new Date().getFullYear() % 100;

  if (yearInput.length === 1) {
    return `${month}/${yearInput}`;
  }

  let yearNumber = Number(yearInput);

  if (yearNumber < currentYearShort) {
    yearNumber = currentYearShort;
  }

  const year = String(yearNumber).padStart(2, "0");

  return `${month}/${year}`;
}

function formatSecurityCode(value) {
  return onlyNumbers(value).slice(0, 3);
}

function handleCardNumberInput(event) {
  checkout.cardNumber = formatCardNumber(event.target.value);
  event.target.value = checkout.cardNumber;
}

function handleExpirationDateInput(event) {
  checkout.expirationDate = formatExpirationDate(event.target.value);
  event.target.value = checkout.expirationDate;
}

function handleSecurityCodeInput(event) {
  checkout.securityCode = formatSecurityCode(event.target.value);
  event.target.value = checkout.securityCode;
}

async function handleCheckoutSubmit() {
  if (!selectedPlan.value) {
    return;
  }

  if (state.subscription?.id) {
    await changePlan(selectedPlan.value.id);
  } else {
    await subscribeToPlan(selectedPlan.value.id);
  }

  closeCheckout();
}

function getPaymentStatusLabel(status) {
  const labels = {
    paid: t("plans.status.paid"),
    pending: t("plans.status.pending"),
    failed: t("plans.status.failed"),
    refunded: t("plans.status.refunded"),
  };

  return labels[status] || status;
}

function getRequestTypeLabel(type) {
  const labels = {
    "change-plan": t("plans.request.changePlan"),
    cancellation: t("plans.request.cancellation"),
  };

  return labels[type] || type;
}

function getRequestStatusLabel(status) {
  const labels = {
    open: t("plans.status.open"),
    resolved: t("plans.status.resolved"),
    closed: t("plans.status.closed"),
  };

  return labels[status] || status;
}
</script>

<style scoped>
.subscription-warning {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-lg);
  background: var(--color-primary-light);
  color: var(--color-primary);
  font-weight: 900;
  line-height: 1.5;
  padding: 18px 22px;
  margin-bottom: 20px;
}

.plans-summary {
  margin-bottom: 20px;
}

.plans-list {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 20px;
}

.checkout-section,
.plans-bottom {
  margin-top: 20px;
}

.selected-plan {
  display: flex;
  justify-content: space-between;
  gap: 16px;
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  padding: 16px;
  margin-bottom: 18px;
}

.selected-plan span {
  color: var(--color-text-muted);
}

.selected-plan strong {
  color: var(--color-text);
}

.checkout-form {
  display: grid;
  gap: 16px;
}

.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 14px;
}

.form-field {
  display: grid;
  gap: 8px;
}

.form-field span {
  color: var(--color-text-muted);
  font-weight: 800;
}

.form-field input {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  color: var(--color-text);
  padding: 14px 16px;
  outline: none;
}

.checkout-actions {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}

.summary-number {
  color: var(--color-text);
  font-size: 34px;
  font-weight: 900;
  line-height: 1;
  margin: 0 0 8px;
}

.summary-text {
  font-size: 24px;
}

.summary-label {
  color: var(--color-text-muted);
  margin: 0;
}

.empty-state {
  border: 1px dashed var(--color-border);
  border-radius: var(--radius-md);
  color: var(--color-text-muted);
  padding: 18px;
}

.simple-list {
  display: grid;
  gap: 14px;
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

@media (max-width: 900px) {
  .plans-list,
  .form-grid {
    grid-template-columns: 1fr;
  }

  .simple-item,
  .selected-plan,
  .checkout-actions {
    flex-direction: column;
  }
}

.checkout-overlay {
  position: fixed;
  inset: 0;
  z-index: 100;
  display: grid;
  place-items: center;
  background: rgba(7, 20, 47, 0.38);
  backdrop-filter: blur(8px);
  padding: 24px;
  animation: overlayIn 180ms ease-out;
}

.checkout-drawer {
  width: min(100%, 760px);
  max-height: min(92vh, 760px);
  overflow-y: auto;
  border: 1px solid var(--color-border);
  border-radius: 28px 28px var(--radius-lg) var(--radius-lg);
  background: var(--color-surface);
  box-shadow: 0 28px 80px rgba(15, 23, 42, 0.28);
  padding: 18px 24px 24px;
  animation: drawerUp 240ms ease-out;
}

.checkout-handle {
  width: 54px;
  height: 6px;
  border-radius: var(--radius-pill);
  background: var(--color-border);
  margin: 0 auto 18px;
}

.checkout-header {
  display: flex;
  justify-content: space-between;
  gap: 18px;
  align-items: flex-start;
  margin-bottom: 18px;
}

.checkout-header h2 {
  color: var(--color-text);
  font-size: 24px;
  font-weight: 900;
  line-height: 1.1;
  margin: 0 0 8px;
}

.checkout-header p {
  color: var(--color-text-muted);
  line-height: 1.5;
  margin: 0;
}

.checkout-close {
  display: grid;
  place-items: center;
  width: 42px;
  height: 42px;
  border: 1px solid var(--color-border);
  border-radius: 50%;
  background: var(--color-surface-soft);
  color: var(--color-text);
  font-size: 26px;
  font-weight: 900;
  line-height: 1;
}

.selected-plan {
  display: flex;
  justify-content: space-between;
  gap: 16px;
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  padding: 16px;
  margin-bottom: 18px;
}

.selected-plan span {
  color: var(--color-text-muted);
}

.selected-plan strong {
  color: var(--color-text);
  text-align: right;
}

.checkout-form {
  display: grid;
  gap: 16px;
}

.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 14px;
}

.form-field {
  display: grid;
  gap: 8px;
}

.form-field span {
  color: var(--color-text-muted);
  font-weight: 800;
}

.form-field input {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  color: var(--color-text);
  padding: 14px 16px;
  outline: none;
}

.form-field input:focus {
  border-color: var(--color-primary);
  background: #ffffff;
  box-shadow: 0 0 0 4px rgba(47, 128, 237, 0.12);
}

.checkout-actions {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
  margin-top: 4px;
}

@keyframes overlayIn {
  from {
    opacity: 0;
  }

  to {
    opacity: 1;
  }
}

@keyframes drawerUp {
  from {
    opacity: 0;
    transform: translateY(34px) scale(0.98);
  }

  to {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}

@media (max-width: 700px) {
  .checkout-overlay {
    padding: 12px;
  }

  .checkout-drawer {
    width: 100%;
    max-height: 88vh;
    border-radius: 24px 24px 18px 18px;
    padding: 14px 18px 20px;
  }

  .checkout-header,
  .selected-plan,
  .checkout-actions {
    flex-direction: column;
  }

  .selected-plan strong {
    text-align: left;
  }

  .form-grid {
    grid-template-columns: 1fr;
  }

  .checkout-actions .btn-primary,
  .checkout-actions .btn-secondary {
    width: 100%;
  }
}
</style>