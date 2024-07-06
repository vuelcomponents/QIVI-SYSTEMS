<script setup lang="ts">
import { type User } from '@/types/User';
import type { AuthService } from '@/services/AuthService';
import { ref } from 'vue';
import InputText from 'primevue/inputtext';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import { useI18n } from 'vue-i18n';
import type { AxiosResponse } from 'axios';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import Menubar from 'primevue/menubar';
import type { SomeStepperItem } from '@/components/reusable/steppers/SomeStepperItem';
const props = defineProps<{
  service: AuthService;
}>();
const { t } = useI18n({});
export type EmailChange = {
  password: string;
  newEmail: string;
};
const models = ref<EmailChange>({
  password: '',
  newEmail: '',
});
const steps: SomeStepperItem[] = [
  {
    label: t('changeEmail'),
    id: 1,
    icon: 'mdi mdi-account-edit',
  },
];
const emit = defineEmits<{
  close: [];
}>();
const onChangePassword = () => {
  if (
    unfilledRequired<EmailChange>(
      ['password', 'newEmail'],
      models.value,
      props.service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  props.service.changeEmail(models.value).then((res: AxiosResponse<User>) => {
    if (res.status === 200 && res.data) {
      props.service.emitter.emit('info', {
        severity: 'success',
        summary: t(`emailVerificationSent`),
        detail: t(`linkHasBeenSentToYourNewEmailAddress`),
        life: 3000,
      });
      models.value = { password: '', newEmail: '' };
      emit('close');
    }
  });
};
</script>

<template>
  <section class="p-1 dark:bg-forest-0 dark:text-surface-100 md:h-[300px]">
    <SomeStepper :items="steps" class="w-full">
      <template #before>
        <Menubar :model="[]" class="w-full" />
      </template>
      <template #hrtStepper1>
        <section class="w-full">
          <div class="m-0 mb-1 flex gap-1 p-0 text-sm">
            <i class="text-red-700">*</i> {{ $t(`newEmail`) }}
          </div>
          <input-text
            id="hrt-form-newEmail"
            type="email"
            class="mb-2 w-full"
            v-model="models.newEmail"
          />
          <div class="m-0 mb-1 flex gap-1 p-0 text-sm">
            <i class="text-red-700">*</i> {{ $t(`password`) }}
          </div>
          <input-text
            id="hrt-form-password"
            type="password"
            class="mb-2 w-full"
            v-model="models.password"
          />
          <Button
            class="w-full bg-mintSplash-200 hover:bg-mintSplash-100"
            severity="success"
            @click="onChangePassword"
            >{{ $t('changeEmail') }}</Button
          >
        </section>
      </template>
    </SomeStepper>
  </section>
</template>
