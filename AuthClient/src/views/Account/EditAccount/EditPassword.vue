<script setup lang="ts">
import { type User } from '@/types/User';
import type { AuthService } from '@/services/AuthService';
import { ref } from 'vue';
import InputText from 'primevue/inputtext';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import type { NewPassword } from '@/types/NewPassword';
import { useI18n } from 'vue-i18n';
import type { AxiosResponse } from 'axios';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import Menubar from 'primevue/menubar';
import type { SomeStepperItem } from '@/components/reusable/steppers/SomeStepperItem';
const props = defineProps<{
  service: AuthService;
}>();
const { t } = useI18n({});
const models = ref<NewPassword>({
  oldPassword: '',
  newPassword: '',
  reNewPassword: '',
});
const steps: SomeStepperItem[] = [
  {
    label: t('changePassword'),
    id: 1,
    icon: 'mdi mdi-account-edit',
  },
];
const emit = defineEmits<{
  close: [];
}>();
const onChangePassword = () => {
  if (
    unfilledRequired<NewPassword>(
      ['oldPassword', 'newPassword', 'reNewPassword'],
      models.value,
      props.service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  props.service.changePassword(models.value).then((res: AxiosResponse<User>) => {
    if (res.status === 200 && res.data) {
      props.service.emitter.emit('info', {
        severity: 'success',
        summary: t ? t(`passwordChanged`) : `passwordChanged`,
        detail: t
          ? t(`yourPasswordHasBeenSuccessfullyChanged`)
          : `yourPasswordHasBeenSuccessfullyChanged`,
        life: 3000,
      });
      models.value = {
        newPassword: '',
        reNewPassword: '',
        oldPassword: '',
      };
      emit('close');
    }
  });
};
</script>

<template>
  <section class="p-1 dark:bg-forest-0 dark:text-surface-100 md:h-[350px]">
    <SomeStepper :items="steps" class="w-full">
      <template #before>
        <Menubar :model="[]" class="w-full" />
      </template>
      <template #hrtStepper1>
        <section class="w-full">
          <div class="m-0 mb-1 flex gap-1 p-0 text-sm">
            <i class="text-red-700">*</i>
            {{ $t(`oldPassword`) }}
          </div>
          <input-text
            id="hrt-form-oldPassword"
            type="password"
            class="mb-2 w-full"
            v-model="models.oldPassword"
          />
          <div class="m-0 mb-1 flex gap-1 p-0 text-sm">
            <i class="text-red-700">*</i>
            {{ $t(`newPassword`) }}
          </div>
          <input-text
            id="hrt-form-newPassword"
            type="password"
            class="mb-2 w-full"
            v-model="models.newPassword"
          />
          <div class="m-0 mb-1 flex gap-1 p-0 text-sm">
            <i class="text-red-700">*</i>
            {{ $t(`confirmNewPassword`) }}
          </div>
          <input-text
            id="hrt-form-reNewPassword"
            type="password"
            class="mb-2 w-full"
            v-model="models.reNewPassword"
          />
          <Button
            class="w-full bg-mintSplash-200 hover:bg-mintSplash-100"
            severity="success"
            @click="onChangePassword"
          >
            {{ $t('changePassword') }}
          </Button>
        </section>
      </template>
    </SomeStepper>
  </section>
</template>
