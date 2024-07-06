<script setup lang="ts">
import { UseUserStore } from '@/stores/UserStore';
import AppTile from '@/components/reusable/cards/AppTile.vue';
import { getAssociatedUrl } from '@/variables/urls';
defineProps<{
  dashboard?: boolean;
}>();
const user = UseUserStore().user;
const redirect = (host: string) => {
  location.href = host;
};
</script>

<template>
  <div v-if="user.licences.length < 1" class="flex items-center justify-center">
    <div class="rounded-xl p-4 px-10 ring-1 ring-light-300 dark:ring-surface-500">
      {{ $t('noLicenceOwned') }}
    </div>
  </div>
  <div class="grid grid-cols-2" v-else>
    <AppTile
      v-for="licence in user.licences"
      :key="licence.id"
      @click="redirect(licence.host)"
      :tooltip="licence.code"
      :icon="licence.icon"
      :dashboard="dashboard"
    />
    <AppTile
      @click="redirect(getAssociatedUrl('auth-client'))"
      :tooltip="'QIVI'"
      icon="mdi mdi-vector-triangle"
      :dashboard="dashboard"
    />
    <AppTile
      @click="redirect(`${getAssociatedUrl('auth-client')}/docs`)"
      :tooltip="'Docs'"
      icon="mdi mdi-book-open"
      :dashboard="dashboard"
    />
  </div>
</template>
