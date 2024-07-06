<script setup lang="ts">
import type { AuthService } from '@/services/AuthService';
import LsdCard from '@/components/reusable/cards/LsdCard.vue';
import type { NotificationType } from '@/types/User';
import { ref } from 'vue';

const props = defineProps<{
  authService: AuthService;
}>();
const notifications = ref<Array<NotificationType>>(
  (await props.authService.getNotifications()).data
);


const seenNotification = (id: number) => {
  props.authService.seenNotification(id).then((res) => {
    if (res.status === 200) {
      notifications.value = notifications.value.filter((n) => n.id !== id);
      props.authService.storeHandler.userStore.updateUserNotifications(notifications.value);
    }
  });
};
</script>

<template>
  <main
    class="max-h-[400px] overflow-y-auto p-1 ring-1 ring-surface-400/5 dark:ring-surface-600 sm:max-h-screen"
  >
    <div v-if="notifications.length < 1" class="flex items-center justify-center p-2 text-sm">
      {{ $t('noNotifications') }}
    </div>
    <div v-else v-for="notification in notifications" :key="notification.id">
      <LsdCard :title="notification.code" class="h-[65px]" :active="true" black-ring>
        <template>
          {{ notification.code }}
        </template>
        <template #description>
          {{ notification.description }}
        </template>
        <template #right>
          <span
            @click="seenNotification(notification.id)"
            class="group flex h-fit cursor-pointer items-center justify-center rounded-full px-1 ring-1 ring-mintSplash-300 transition-colors hover:bg-mintSplash-300"
          >
            <i class="mdi mdi-check text-mintSplash-300 group-hover:text-surface-200" />
          </span>
        </template>
      </LsdCard>
    </div>
  </main>
</template>
