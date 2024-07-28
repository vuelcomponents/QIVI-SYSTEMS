<script setup lang="ts">
import type { ICellRendererParams } from 'ag-grid-community';

import type { ServiceRoots } from '@/Shared/Services/ServiceRoots';
import { inject } from 'vue';
import type { AuthService } from '@/Entities/User/AuthService';
import { useI18n } from 'vue-i18n';
import type { AxiosResponse } from 'axios';
const props = defineProps<{
  params: ICellRendererParams;
}>();
const { t } = useI18n({});
const services: ServiceRoots = inject('Services')!;
const authService: AuthService = services.create<AuthService>('AuthService')!;
</script>

<template>
  <div>
    <span class="relative" v-if="props.params.data.ipBlocked" v-tooltip="t('blockedIp')">
      <i class="mdi mdi-ip text-2xl" />
      <span
        class="absolute right-0 top-[7px] z-20 flex h-[15px] w-[15px] items-center justify-center rounded-xl bg-red-500"
      >
        <i class="mdi mdi-cancel text-surface-100" />
      </span>
    </span>
    <span class="relative" v-if="props.params.data.blocked" v-tooltip="t('blockedDevice')">
      <i class="mdi mdi-desktop-classic text-2xl" />
      <span
        class="absolute right-0 top-[7px] z-20 flex h-[15px] w-[15px] items-center justify-center rounded-xl bg-red-500"
      >
        <i class="mdi mdi-cancel text-surface-100" />
      </span>
    </span>
    <span class="relative" v-if="props.params.data.active" v-tooltip="t('online')">
      <i class="mdi mdi-earth text-2xl" />
      <span
        class="absolute right-0 top-[7px] z-20 flex h-[15px] w-[15px] items-center justify-center rounded-xl bg-mintSplash-100"
      >
        <i class="mdi mdi-check text-surface-100" />
      </span>
    </span>
    <span
      class="relative"
      v-if="!props.params.data.active && !props.params.data.blocked && !props.params.data.ipBlocked"
      v-tooltip="t('off')"
    >
      <i class="mdi mdi-coffee-to-go text-2xl" />
      <span
        class="absolute right-0 top-[7px] z-20 flex h-[15px] w-[15px] items-center justify-center rounded-xl bg-[grey]"
      >
        <i class="mdi mdi mdi-timer-sand text-surface-100" />
      </span>
    </span>
  </div>
</template>
