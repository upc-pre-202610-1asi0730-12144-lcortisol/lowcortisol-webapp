<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">LowCortisol</h1>
        <p class="page-subtitle">
          Plataforma de monitoreo inteligente orientada a sensores, alertas,
          reportes, planes y control operativo.
        </p>
      </div>

      <RouterLink class="btn-primary" :to="{ name: 'dashboard' }">
        Ir al panel
      </RouterLink>
    </section>

    <section class="grid grid-3">
      <UiCard title="EP01" eyebrow="IAM">
        <p class="home-card-text">
          Registro, inicio de sesión, recuperación de contraseña, perfil y cambio de idioma.
        </p>
      </UiCard>

      <UiCard title="EP02" eyebrow="Monitoreo inteligente">
        <p class="home-card-text">
          Visualización de agua, gas, sensores, alertas automáticas e historial de consumo.
        </p>
      </UiCard>

      <UiCard title="EP07" eyebrow="Servicios API">
        <p class="home-card-text">
          Endpoints técnicos para usuarios, sensores, lecturas, alertas, reportes y planes.
        </p>
      </UiCard>
    </section>
  </AppLayout>
</template>

<script setup>
import AppLayout from "../../components/app-layout/app-layout.component.vue";
import UiCard from "../../components/ui-card/ui-card.component.vue";
</script>

<style scoped>
.home-card-text {
  color: var(--color-text-muted);
  line-height: 1.55;
  margin: 0;
}
</style>