<script setup lang="ts">
import type { ICellRendererParams } from 'ag-grid-community';
import Button from 'primevue/button';
import type { ServiceRoots } from '@/services/ServiceRoots';
import { inject } from 'vue';
import type { AuthService } from '@/services/AuthService';
import { useI18n } from 'vue-i18n';
const props = defineProps<{
  params: ICellRendererParams;
}>();
const { t } = useI18n({});
const services: ServiceRoots = inject('services')!;
const authService: AuthService = services.create<AuthService>('AuthService')!;
const signOut = () => {
  authService.signOutDevice(props.params.data.id).then((res: any) => {
    if (res.status === 200 && res.data) {
      authService.emitter.emit('info', {
        severity: 'info',
        summary: t('deviceHasBeenSignout'),
        detail: t('deviceHasBeenSignoutDescription'),
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
  <Button
    :disabled="!params.data.active"
    severity="secondary"
    @click="signOut"
    class="h-[60%] w-full"
    >{{ $t('signOut') }}</Button
  >
</template>

<style scoped></style>
