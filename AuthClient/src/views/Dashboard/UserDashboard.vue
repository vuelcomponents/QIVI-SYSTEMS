<script setup lang="ts">
import ViewHeader from '@/components/reusable/headers/VueHeader.vue';
import Menubar from 'primevue/menubar';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';
import MainAppChooser from '@/components/referenced/headers/MainAppChooser.vue';
const { t } = useI18n();
const menuItems = computed(() => [
  {
    label: t('actions'),
    icon: 'mdi mdi-wrench',
    items: [],
  },
]);
const stepperItems = [{ label: 'authorize', id: 1, icon: 'mdi mdi-account' }];
</script>

<template>
  <ViewHeader
    icon="mdi mdi-chart-donut-variant"
    :title="$t('dashboard')"
    :description="$t('dashboardDescription')"
  />
  <SomeStepper :items="stepperItems" class="w-full">
    <template #before>
      <Menubar :model="menuItems" class="w-full" />
    </template>
    <template #hrtStepper1>
      <MainAppChooser dashboard />
    </template>
  </SomeStepper>
</template>
