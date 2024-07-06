<script setup lang="ts">
import { ref } from 'vue';
import type { NotificationType, User } from '@/types/User';
import InputText from 'primevue/inputtext';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import { useI18n } from 'vue-i18n';
import Menubar from 'primevue/menubar';
import type { UserService } from '@/services/UserService';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
const props = defineProps<{
  selectedUsers: Array<User>;
  userService: UserService;
}>();
const emit = defineEmits<{
  close: [];
}>();
const notification = ref<NotificationType>({} as NotificationType);
const { t } = useI18n();
const steps = [
  {
    label: 'sendNotification',
    id: 1,
    icon: 'mdi mdi-send',
  },
];
const sendNotification = () => {
  if (
    unfilledRequired<NotificationType>(
      ['code'],
      notification.value,
      props.userService.emitter,
      t,
      true
    )
  ) {
    return;
  }
  props.userService.sendNotification(props.selectedUsers, notification.value).then((res) => {
    if (res.status === 200) {
      props.userService.emitter.emit('info', {
        severity: 'success',
        summary: t(`successfullySent`),
        detail: t('successfullySentDescription'),
        life: 3000,
      });
      emit('close');
    }
  });
};
const menuItems = [
  {
    label: t('send'),
    icon: 'mdi mdi-send',
    command: sendNotification,
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('close'),
  },
];
const receivers = props.selectedUsers.map((u) =>
  u.firstName && u.lastName ? `${u.firstName} ${u.lastName}` : `${u.email}`
);
</script>

<template>
  <SomeStepper :items="steps" class="w-full">
    <template #before>
      <Menubar :model="menuItems" class="mb-2 w-full" />
    </template>
    <template #hrtStepper1>
      <main class="max-h-[400px]">
        <div class="flex gap-1">
          <span class="font-bold">{{ $t('receivers') }}: </span>
          <span v-for="receiver in receivers" :key="receiver">{{ receiver }}, </span>
        </div>
        <section class="mt-2 flex justify-center">
          <div class="flex max-h-[300px] w-full flex-col">
            <label>{{ $t('title-code') }}</label>
            <input-text id="hrt-form-code" v-model="notification.code" :max="100" class="w-full" />
            <label>{{ $t('description') }}</label>
            <input-text
              id="hrt-form-code"
              v-model="notification.description"
              :max="500"
              class="w-full"
            />
          </div>
        </section>
      </main>
    </template>
  </SomeStepper>
</template>
