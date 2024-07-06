<script setup lang="ts">
import WeedCard from '@/components/reusable/cards/WeedCard.vue';
import { useI18n } from 'vue-i18n';
import { UseUserStore } from '@/stores/UserStore';
const { t } = useI18n();
const userStore = UseUserStore();
const user = userStore.user;
const redirect = (host: string) => {
  location.href = host;
};
</script>

<template>
  <WeedCard :title="'welcome'" class="h-[65px]" :active="true" black-ring>
    <template #description>
      <h1 class="text-[0.7em] lg:text-sm">
        {{ user.notifications?.length > 0 ? $t('youHaveNotifications') : $t('noNotifications') }}
      </h1>
    </template>
    <template #default>
      <div class="flex gap-2">
        <div
          class="rounded-full px-4 py-3 text-light-700 ring-1 ring-trueGreen-600/10 transition-colors dark:text-surface-200 dark:ring-mintSplash-200/10"
        >
          <i class="mdi mdi-information" />
        </div>
      </div>
    </template>
  </WeedCard>
</template>
