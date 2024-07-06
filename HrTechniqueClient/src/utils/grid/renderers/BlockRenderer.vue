<script setup lang="ts">
import type { ICellRendererParams } from 'ag-grid-community';
import Button from 'primevue/button';
import type { ServiceRoots } from '@/services/ServiceRoots';
import { inject } from 'vue';
import type { AuthService } from '@/services/AuthService';
import { useI18n } from 'vue-i18n';
import type { AxiosResponse } from 'axios';
const props = defineProps<{
  params: ICellRendererParams;
}>();
const { t } = useI18n({});
const services: ServiceRoots = inject('services')!;
const authService: AuthService = services.create<AuthService>('AuthService')!;
const block = () => {
  authService.blockDevice(props.params.data.id).then((res: AxiosResponse<any>) => {
    if (res.status === 200 && res.data) {
      authService.emitter.emit('info', {
        severity: 'info',
        summary: t('deviceHasBeenBlocked'),
        detail: t('deviceHasBeenBlockedDescription'),
        life: 3000,
      });
      props.params.api!.forEachNode((node) => {
        if (node.data.id === res.data.id) {
          node.setData(res.data);
        }
      });
    }
  });
};
const unlock = () => {
  authService.unlockDevice(props.params.data.id).then((res: AxiosResponse<any>) => {
    if (res.status === 200 && res.data) {
      authService.emitter.emit('info', {
        severity: 'info',
        summary: t('deviceHasBeenUnlocked'),
        detail: t('deviceHasBeenUnlockedDescription'),
        life: 3000,
      });
      props.params.api!.forEachNode((node) => {
        if (node.data.id === res.data.id) {
          node.setData(res.data);
        }
      });
    }
  });
};
</script>

<template>
  <Button v-if="!params.data.blocked" severity="secondary" @click="block" class="h-[60%] w-full">{{
    $t('block')
  }}</Button>
  <Button v-if="params.data.blocked" severity="secondary" @click="unlock" class="h-[60%] w-full">{{
    $t('unlock')
  }}</Button>
</template>

<style scoped></style>
