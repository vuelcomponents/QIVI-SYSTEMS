<script setup lang="ts">
import { ref } from 'vue';
import type { AuthService } from '@/Entities/User/AuthService';
import InputSwitch from 'primevue/inputswitch';
import { useI18n } from 'vue-i18n';

const props = defineProps<{
  authService: AuthService;
}>();
const { t } = useI18n();

const darkMode = ref(props.authService.storeHandler.settingStore.theme === 'dark');
const updateDarkMode = () => {
  props.authService.storeHandler.settingStore.setTheme(darkMode.value ? 'dark' : 'light');
};
const adminInfo = ref<{ name: string; color: string }>({ name: 'user', color: '' });
switch (props.authService.storeHandler.userStore.user?.role) {
  case 1:
    adminInfo.value = { name: t('privileged'), color: 'text-mintSplash-300' };
    break;
  case 2:
    adminInfo.value = { name: t('admin'), color: 'text-orange-400' };
    break;
  case 3:
    adminInfo.value = { name: t('superAdmin'), color: 'text-red-400' };
    break;
}
</script>

<template>
  <div
    class="flex w-full select-none flex-col justify-center p-2 py-3 text-sm ring-1 ring-surface-600/5 dark:ring-surface-600"
  >
    <span class="text-[0.7em]">{{ $t('welcome') }}</span>
    <div class="text-md">
      <span v-if="authService.storeHandler.userStore.user?.role > 1">
        <i :class="['mdi mdi-shield-sun-outline', adminInfo.color]" v-tooltip="adminInfo.name" />
      </span>
      {{ authService.storeHandler.userStore.user?.email }}
    </div>
    <div class="mt-2 flex items-center gap-2">
      <i
        :class="
          authService.storeHandler.settingStore.theme === 'dark'
            ? 'mdi mdi-weather-night'
            : 'mdi mdi-weather-sunny'
        "
      />
      <input-switch v-model="darkMode" @update:model-value="updateDarkMode" />
    </div>
  </div>
  <div class="w-full">
    <div
      class="group relative mx-4 mt-2 flex cursor-pointer items-center justify-center rounded-xl bg-surface-700/5 px-4 py-2 font-dms text-sm text-surface-700/60 shadow-sm ring-1 ring-surface-200/10 dark:bg-surface-100/5 dark:text-surface-200/80 dark:ring-surface-100/60"
      @click="authService.logout()"
    >
      <div
        class="flex items-center justify-center text-[0.8em] transition-all group-hover:text-surface-700 dark:group-hover:text-surface-100"
      >
        <strong class="text-orange-400">
          <i class="mdi mdi-lock-open mr-2" />{{ $t('signOut') }}
        </strong>
      </div>
    </div>
  </div>
</template>
