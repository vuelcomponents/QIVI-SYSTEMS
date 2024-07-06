<script setup lang="ts">
import {  inject, ref } from 'vue';
import { AuthService } from '@/services/AuthService';
import { ServiceRoots } from '@/services/ServiceRoots';
import ProgressSpinner from 'primevue/progressspinner';
import { openKivi } from '@/views/Auth/openKivi';

const services = inject<ServiceRoots>('services')!;

const authService = services.create<AuthService>('AuthService')!;

const proceed = ref(false);
const intervalId = ref(0);
const handleOpenKivi = () => openKivi(intervalId, proceed, authService);
</script>

<template>
  <div
    class="flex w-full flex-col items-center justify-center text-west-3 dark:text-light-500"
    style="height: calc(100vh - 130px)"
  >
    <div class="rounded-xl p-8 ring-1 ring-surface-950/10 dark:ring-surface-200/20">
      <div class="w-full flex justify-center mb-4">
        <i class="mdi mdi-language-r text-3xl font-bold" />
        <span class="flex font-bold">
          {{ $t('HRTECHNIQUE') }}
          <span class="text-[0.5em]">DEV</span>
        </span>
      </div>
      <div class="flex justify-center">
        <ProgressSpinner v-if="proceed" class="m-0 my-0 mb-0 mt-0 w-[50px] select-none" />
      </div>
      <div
        @click="handleOpenKivi"
        class="group relative mx-4 mt-2 flex cursor-pointer items-center justify-center rounded-xl bg-surface-700/5 px-4 py-2 font-dms text-sm text-surface-700/60 shadow-sm ring-1 ring-surface-200/10 dark:bg-surface-100/5 dark:text-surface-200/80 dark:ring-surface-100/60"
      >
        <div
          class="flex items-center justify-center text-[0.8em] transition-all group-hover:text-surface-700 dark:group-hover:text-surface-100"
        >
          <strong> <i class="mdi mdi-vector-triangle mr-2" />{{ $t('signInWithKivi') }} </strong>
        </div>
      </div>
    </div>
  </div>
</template>
