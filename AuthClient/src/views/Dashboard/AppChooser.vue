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
  <WeedCard :title="'services'" class="h-[65px]" :active="true" black-ring>
    <template #description>
      <h1 class="text-[0.7em] lg:text-sm">{{ $t('youCanAuthorizeFromHere') }}</h1>
    </template>
    <template #default>
      <div class="flex gap-4">
        <div
          v-for="licence in user.licences"
          :key="licence.id"
          @click="redirect(licence.host)"
          v-tooltip="licence.code"
          class="cursor-pointer rounded-full px-4 py-3 text-light-700 ring-4 ring-inset ring-[#eab30880] transition-colors hover:bg-light-700 hover:text-surface-200 dark:text-surface-200 dark:ring-forest-400 dark:hover:bg-mintSplash-200"
        >
          <i :class="licence.icon" />
        </div>
      </div>
    </template>
  </WeedCard>
</template>
