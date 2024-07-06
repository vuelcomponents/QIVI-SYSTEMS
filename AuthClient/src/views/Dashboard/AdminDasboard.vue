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
      <div
        class="mt-1 flex h-full w-full flex-col gap-2 overflow-x-hidden overflow-y-hidden xl:flex-row"
      >
        <div class="flex h-full w-full flex-1 flex-col">
          <LicenceGrid class="h-full min-h-[300px]" :users="users!" />
        </div>
      </div>
      <div
        class="flex h-full w-full flex-col gap-2 overflow-x-hidden overflow-y-hidden xl:flex-row"
      >
        <div class="flex h-full w-full flex-1 flex-col">
          <div class="pt-1">
            <app-chooser />
          </div>
          <OnlineDeviceGrid class="h-full min-h-[300px]" :users="users!" />
        </div>
        <div class="flex h-full w-full flex-1 flex-col">
          <div class="pt-2 lg:pt-1">
            <SecurityKnob :global-security-settings="globalSecuritySettings" class="w-full" />
          </div>
          <ChartUsersSecurity
            :labels="labels"
            :devices-count="devicesCount"
            :blocked-devices="blockedDevicesCount"
            :blocked-ips="blockedIpCount"
            v-if="labels && devicesCount && blockedDevicesCount && blockedIpCount"
          />
        </div>
      </div>
    </template>
  </SomeStepper>
</template>
<script setup lang="ts">
import ChartUsersSecurity from '@/views/Dashboard/ChartUsersSecurity.vue';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import Menubar from 'primevue/menubar';
import { computed, inject, ref } from 'vue';
import type { ServiceRoots } from '@/services/ServiceRoots';
import { StatService } from '@/services/StatService';
import type { AxiosResponse } from 'axios';
import { type UserStats, type Stats } from '@/types/Stats';
import OnlineDeviceGrid from '@/views/Dashboard/OnlineDeviceGrid.vue';
import SecurityKnob from '@/views/Dashboard/SecurityKnob.vue';
import AppChooser from '@/views/Dashboard/AppChooser.vue';
import { useI18n } from 'vue-i18n';
import ViewHeader from '@/components/reusable/headers/VueHeader.vue';
import ChartLicenceManagement from '@/views/Dashboard/ChartLicenceManagement.vue';
import ChartVisits from '@/views/Dashboard/ChartVisits.vue';
import ChartActivity from '@/views/Dashboard/ChartActivity.vue';
import NotificationsGrid from '@/views/Dashboard/NotificationsGrid.vue';
import LicenceGrid from '@/views/Dashboard/LicenceGrid.vue';
import WelcomeCard from '@/views/Dashboard/WelcomeCard.vue';
const { t } = useI18n({});

const services = inject('services') as ServiceRoots;
const statService: StatService = services.create<StatService>('StatService') as StatService;

const stats = ref<Stats>();

statService.getAll().then((res: AxiosResponse<Stats>) => {
  console.log('stats', res.data);
  stats.value = res.data;
});
const menuItems = computed(() => [
  {
    label: t('actions'),
    icon: 'mdi mdi-wrench',
    items: [],
  },
]);
const stepperItems = [{ label: 'usersSecurity', id: 1, icon: 'mdi mdi-account' }];
const globalSecuritySettings = computed<number>((): number => {
  return parseInt(stats.value?.globalSecuritySettings.toFixed(0)!);
});
const labels = computed<Array<string>>((): Array<string> => {
  return stats.value?.userStats.map((user) => user.email)! ?? [];
});
const devicesCount = computed<Array<number>>((): Array<number> => {
  return stats.value?.userStats.map((user) => user.devicesCount)! ?? [];
});
const blockedDevicesCount = computed<Array<number>>((): Array<number> => {
  return stats.value?.userStats.map((user) => user.blockedDevicesCount)! ?? [];
});
const blockedIpCount = computed<Array<number>>((): Array<number> => {
  return stats.value?.userStats.map((user) => user.blockedIpsCount)! ?? [];
});
const users = computed<Array<UserStats>>((): Array<UserStats> => {
  return stats.value?.userStats.filter((user) => user.active)! ?? [];
});
const monthlyVisits = computed<Array<number>>((): Array<number> => {
  return (stats.value?.userStats.map((user) => user.monthlyVisits)! ?? []).slice(0, 5);
});
const monthlyActivities = computed<Array<number>>((): Array<number> => {
  return (stats.value?.userStats.map((user) => user.monthlyActivities)! ?? []).slice(0, 5);
});
</script>
