<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">
          {{ t('profile.title') }}
        </h1>

        <p class="page-subtitle">
          {{ t('profile.subtitle') }}
        </p>
      </div>

      <button class="btn-primary" type="button" @click="handleSaveProfile">
        {{ t('actions.saveChanges') }}
      </button>
    </section>

    <section class="profile-grid">
      <UiCard :title="t('profile.user')">
        <div v-if="state.user" class="profile-card">
          <div class="avatar">
            {{ state.user.initials }}
          </div>

          <h2 class="profile-name">
            {{ state.user.fullName }}
          </h2>

          <p class="profile-email">
            {{ state.user.email }}
          </p>

          <span class="badge badge-success">
            {{ state.user.statusLabel }}
          </span>
        </div>
      </UiCard>

      <UiCard :title="t('profile.accessProfile')">
        <div v-if="state.accessProfile" class="metric-list">
          <div class="metric-row">
            <span>{{ t('profile.name') }}</span>
            <strong>{{ state.accessProfile.name }}</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('profile.role') }}</span>
            <strong>{{ state.accessProfile.roleLabel }}</strong>
          </div>

          <div class="metric-row">
            <span>{{ t('profile.permissions') }}</span>
            <strong>{{ state.accessProfile.permissions.length }}</strong>
          </div>
        </div>
      </UiCard>
    </section>

    <section class="grid grid-2 profile-bottom">
      <UiCard :title="t('profile.updateData')">
        <form class="form" @submit.prevent="handleSaveProfile">
          <label class="form-field">
            <span>{{ t('auth.fields.fullName') }}</span>
            <input v-model="form.fullName" type="text" />
          </label>

          <label class="form-field">
            <span>{{ t('auth.fields.phone') }}</span>
            <input v-model="form.phone" type="text" />
          </label>

          <button class="btn-secondary" type="submit">
            {{ t('actions.updateProfile') }}
          </button>
        </form>
      </UiCard>

      <UiCard :title="t('profile.activeSession')">
        <div class="metric-list">
          <div class="metric-row">
            <span>{{ t('profile.status') }}</span>
            <strong>
              {{ state.session?.canAccess ? t('profile.active') : '-' }}
            </strong>
          </div>

          <div class="metric-row">
            <span>{{ t('profile.token') }}</span>
            <strong>
              {{ state.session?.token ? t('profile.available') : '-' }}
            </strong>
          </div>
        </div>

        <button class="btn-secondary logout-button" type="button" @click="handleSignOut">
          {{ t('actions.closeSession') }}
        </button>
      </UiCard>
    </section>
  </AppLayout>
</template>

<script setup>
import { onMounted, reactive, watch } from "vue";
import { useRouter } from "vue-router";

import AppLayout from "../../../../shared/presentation/components/app-layout/app-layout.component.vue";
import UiCard from "../../../../shared/presentation/components/ui-card/ui-card.component.vue";

import { useIamStore } from "../../../application/store/iam.store";
import { useTranslation } from "../../../../shared/application/services/translation.service";

const router = useRouter();
const { state, loadSession, updateProfile, signOut } = useIamStore();
const { t } = useTranslation();

const form = reactive({
  fullName: "",
  phone: "",
});

onMounted(async () => {
  await loadSession();
});

watch(
    () => state.user,
    (user) => {
      if (!user) return;

      form.fullName = user.fullName;
      form.phone = user.phone;
    },
    { immediate: true }
);

async function handleSaveProfile() {
  await updateProfile({
    fullName: form.fullName,
    phone: form.phone,
  });
}

async function handleSignOut() {
  await signOut();
  await router.push({ name: "login" });
}
</script>

<style scoped>
.profile-grid {
  display: grid;
  grid-template-columns: 0.9fr 1.1fr;
  gap: 20px;
}

.profile-bottom {
  margin-top: 20px;
}

.profile-card {
  display: grid;
  justify-items: center;
  gap: 12px;
  text-align: center;
}

.avatar {
  display: grid;
  place-items: center;
  width: 92px;
  height: 92px;
  border-radius: 50%;
  background: var(--color-primary);
  color: #ffffff;
  font-size: 34px;
  font-weight: 900;
  box-shadow: var(--shadow-button);
}

.profile-name {
  margin: 0;
  font-size: 24px;
  font-weight: 900;
}

.profile-email {
  color: var(--color-text-muted);
  margin: 0;
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

.metric-row span {
  color: var(--color-text-muted);
}

.metric-row strong {
  color: var(--color-text);
  font-weight: 900;
}

.form {
  display: grid;
  gap: 16px;
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

.logout-button {
  width: 100%;
  margin-top: 18px;
}

@media (max-width: 800px) {
  .profile-grid {
    grid-template-columns: 1fr;
  }

  .metric-row {
    flex-direction: column;
  }
}
</style>