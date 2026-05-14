<template>
  <AppLayout>
    <section class="page-header">
      <div>
        <h1 class="page-title">Acerca del proyecto</h1>
        <p class="page-subtitle">
          LowCortisol organiza sus funcionalidades mediante épicas, historias de usuario
          y tareas técnicas.
        </p>
      </div>
    </section>

    <section class="grid grid-2">
      <UiCard title="Propósito">
        <p class="about-text">
          Supervisar consumo de agua y gas, detectar anomalías, administrar dispositivos,
          generar reportes y gestionar planes de servicio.
        </p>
      </UiCard>

      <UiCard title="Arquitectura">
        <p class="about-text">
          El frontend está organizado por módulos funcionales con capas de dominio,
          aplicación, infraestructura y presentación.
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
.about-text {
  color: var(--color-text-muted);
  line-height: 1.6;
  margin: 0;
}
</style>