<template>
  <section class="checkout-panel">
    <UiCard :title="t('plans.page.checkoutTitle')">
      <div class="checkout-header">
        <div>
          <p class="checkout-subtitle">
            {{ t("plans.page.checkoutSubtitle") }}
          </p>

          <strong class="selected-plan">
            {{ t("plans.page.selectedPlan") }}:
            {{ plan.name }} · S/ {{ Number(plan.price || 0).toFixed(2) }}
          </strong>
        </div>

        <button
          class="btn-secondary"
          type="button"
          @click="$emit('close')"
        >
          {{ t("plans.page.closeForm") }}
        </button>
      </div>

      <form class="checkout-form" @submit.prevent="handleSubmit">
        <label class="form-field">
          <span>{{ t("plans.page.cardholderName") }}</span>
          <input
            v-model="checkout.cardholderName"
            type="text"
            autocomplete="cc-name"
            required
            placeholder="Jean Loa"
          />
        </label>

        <label class="form-field">
          <span>{{ t("plans.page.cardNumber") }}</span>
          <input
            v-model="checkout.cardNumber"
            type="text"
            inputmode="numeric"
            autocomplete="cc-number"
            maxlength="19"
            required
            placeholder="4242 4242 4242 4242"
            @input="handleCardNumberInput"
          />
        </label>

        <div class="checkout-row">
          <label class="form-field">
            <span>{{ t("plans.page.expirationDate") }}</span>
            <input
              v-model="checkout.expirationDate"
              type="text"
              inputmode="numeric"
              autocomplete="cc-exp"
              maxlength="5"
              required
              placeholder="MM/YY"
              @input="handleExpirationDateInput"
            />
          </label>

          <label class="form-field">
            <span>{{ t("plans.page.securityCode") }}</span>
            <input
              v-model="checkout.securityCode"
              type="text"
              inputmode="numeric"
              autocomplete="cc-csc"
              maxlength="3"
              required
              placeholder="123"
              @input="handleSecurityCodeInput"
            />
          </label>
        </div>

        <button class="btn-primary" type="submit">
          {{ t("plans.page.confirmPurchase") }}
        </button>
      </form>
    </UiCard>
  </section>
</template>

<script setup>
import { reactive } from "vue";

import UiCard from "../../../../../../shared/presentation/components/ui-card/ui-card.component.vue";
import { useTranslation } from "../../../../../../shared/application/services/translation.service";

defineProps({
  plan: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits(["submit", "close"]);

const { t } = useTranslation();

const checkout = reactive({
  cardholderName: "",
  cardNumber: "",
  expirationDate: "",
  securityCode: "",
});

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

function handleSubmit() {
  emit("submit", {
    cardholderName: checkout.cardholderName,
    cardNumber: checkout.cardNumber,
    expirationDate: checkout.expirationDate,
    securityCode: checkout.securityCode,
  });
}
</script>

<style scoped>
.checkout-panel {
  scroll-margin-top: 120px;
}

.checkout-header {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  align-items: flex-start;
  margin-bottom: 24px;
}

.checkout-subtitle {
  color: var(--color-text-muted);
  margin: 0 0 12px;
}

.selected-plan {
  display: block;
  font-size: 1.1rem;
}

.checkout-form {
  display: grid;
  gap: 18px;
}

.checkout-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 18px;
}

.form-field {
  display: grid;
  gap: 8px;
}

.form-field span {
  color: var(--color-text-muted);
  font-weight: 900;
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
}

.btn-secondary {
  border: 1px solid var(--color-border);
  border-radius: var(--radius-md);
  background: var(--color-surface-soft);
  color: var(--color-text);
  font-weight: 900;
  padding: 12px 16px;
  cursor: pointer;
}

@media (max-width: 1100px) {
  .checkout-header {
    flex-direction: column;
  }

  .checkout-row {
    grid-template-columns: 1fr;
  }
}
</style>
