<script setup lang="ts">
import { useRoute } from 'vue-router';
import { inject, ref } from 'vue';
import { ServiceRoots } from '@/services/ServiceRoots';
import { AuthService } from '@/services/AuthService';
import { router } from '@/router/router';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import Menubar from 'primevue/menubar';
import { useI18n } from 'vue-i18n';
import InputText from 'primevue/inputtext';
import type { MenuItem } from 'primevue/menuitem';

const route = useRoute();
const services = inject<ServiceRoots>('services')!;
const authService: AuthService = services.create<AuthService>('AuthService')!;

const { t } = useI18n();
const steps = [
  {
    label: t('setNewPassword'),
    id: 1,
    icon: 'mdi mdi-account-edit',
  },
];
const models = ref({
  newPassword: '',
});
const items = ref<Array<MenuItem[]>>([]);

const resetPassword = () => {
  authService.resetPassword(route.params.t as string, models.value.newPassword).then((res) => {
    if (res.status === 200) {
      authService.emitter.emit('info', {
        severity: 'success',
        summary: t('passwordHasBeenChanged'),
        detail: t('passwordHasBeenChangedDescription'),
        life: 2000,
      });
      setTimeout(() => {
        router.push({ name: 'Dashboard' });
      }, 1000);
    }
  });
};
</script>

<template>
  <main class="flex h-full items-center justify-center">
    <SomeStepper :items="steps" class="w-full xl:w-[30%]" suppress-min-height>
      <template #before>
        <Menubar :model="items" class="w-full" />
      </template>
      <template #hrtStepper1>
        <div class="flex h-full items-center justify-center px-[10%] py-[20%]">
          <div class="flex w-full flex-col gap-2">
            <input-text
              id="hrt-form-newPassword"
              type="password"
              class="w-full"
              v-model="models.newPassword"
              :placeholder="$t('newPassword')"
            />
            <Button class="flex w-full gap-2" @click="resetPassword" severity="success">
              <i class="mdi mdi-lock-reset" />
              <span>{{ $t('resetPassword') }}</span>
            </Button>
          </div>
        </div>
      </template>
    </SomeStepper>
  </main>
</template>
