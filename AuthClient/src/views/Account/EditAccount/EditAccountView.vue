<template>
  <ViewHeader
    icon="mdi mdi-account"
    :title="$t('editYourAccount')"
    :description="$t('editYourAccountDescription')"
  />
  <SomeStepper :items="steps" class="w-full" @onStepChange="onStepChange">
    <template #before>
      <Menubar :model="items" class="w-full" />
    </template>
    <template #hrtStepper1>
      <section class="grid h-fit w-full grid-cols-4 gap-2 md:gap-5 md:p-5 2xl:col-span-4">
        <div
          v-for="param in userFormStrings"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-[0.8em] text-west-3 dark:text-surface-100/50">
            <i
              class="text-red-700"
              v-if="[...userFormGeneralRequired, ...userFormUpdateRequired].includes(param.field)"
            >
              *
            </i>
            {{ $t(`${param.field}`) }}
          </div>
          <input-text
            :id="`hrt-form-${param.field}`"
            v-model="(user as UserUpdate)[param.field!]"
            :minlength="param.min ?? 0"
            :maxlength="param.max"
            class="w-full"
          />
        </div>
      </section>
    </template>
    <template #hrtStepper2>
      <LicencesGrid v-if="user" :auth-service="authService" :user="user" />
    </template>
    <template #hrtStepper3>
      <div class="h-fit">
        <UserPreferences :user="user" show-clock />
        <DevicesGrid :auth-service="authService" :user="user" />
      </div>
    </template>
  </SomeStepper>
  <Dialog v-model:visible="changePasswordDialog">
    <EditPassword :service="authService" @close="changePasswordDialog = false" />
  </Dialog>
  <Dialog v-model:visible="changeEmailDialog">
    <EditEmail :service="authService" @close="changeEmailDialog = false" />
  </Dialog>
</template>
<script setup lang="ts">
import {
  type User,
  userFormGeneralRequired,
  userFormStrings,
  userFormUpdateRequired,
  type UserUpdate,
} from '@/types/User';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import InputText from 'primevue/inputtext';
import Menubar from 'primevue/menubar';
import type { AxiosResponse } from 'axios';
import { useI18n } from 'vue-i18n';
import { computed, inject, onMounted, ref } from 'vue';
import { ServiceRoots } from '@/services/ServiceRoots';
import type { AuthService } from '@/services/AuthService';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import type { SomeStepperItem } from '@/components/reusable/steppers/SomeStepperItem';
import ViewHeader from '@/components/reusable/headers/VueHeader.vue';
import EditPassword from '@/views/Account/EditAccount/EditPassword.vue';
import Dialog from 'primevue/dialog';
import LicencesGrid from '@/views/Account/EditAccount/LicencesGrid.vue';
import DevicesGrid from '@/views/Account/EditAccount/DevicesGrid.vue';
import UserPreferences from '@/views/Users/UserPreferences.vue';
import EditEmail from '@/views/Account/EditAccount/EditEmail.vue';

const { t } = useI18n({});
const services = inject<ServiceRoots>('services')!;
const authService = services.create<AuthService>('AuthService')!;
const user = ref<User>(authService.storeHandler.userStore.user as User);
const changePasswordDialog = ref(false);
const changeEmailDialog = ref(false);

const emit = defineEmits<{
  updated: [User];
}>();

onMounted(() => {
  authService.getSelf().then((res) => {
    if (res.status == 200 && res.data) {
      user.value = res.data;
    }
  });
});

const actions = {
  saveChanges: () => {
    if (
      unfilledRequired<User>(
        [...userFormUpdateRequired, ...userFormGeneralRequired],
        user.value,
        authService.emitter,
        t,
        true
      )
    ) {
      return;
    }
    authService.update(user.value).then((res: AxiosResponse<User>) => {
      if (res.status === 200 && res.data) {
        authService.storeHandler.userStore.updateAuthUser(res.data);
      }
    });
  },
  showChangePassword: () => (changePasswordDialog.value = true),
  showChangeEmail: () => (changeEmailDialog.value = true),
};
const currentStep = ref<number>(1);

const items = computed(() => [
  {
    label: t('saveChanges'),
    icon: 'mdi mdi-content-save',
    command: actions.saveChanges,
  },
  {
    label: t('actions'),
    icon: 'mdi mdi-wrench',
    items: [
      {
        label: t('changeEmail'),
        icon: 'mdi mdi-lock-reset',
        command: actions.showChangeEmail,
      },
      {
        label: t('changePassword'),
        icon: 'mdi mdi-lock-reset',
        command: actions.showChangePassword,
      },
    ],
  },
]);

const onStepChange = (id: number) => {
  currentStep.value = id;
};
const steps: SomeStepperItem[] = [
  {
    label: 'editPersonalDetails',
    id: 1,
    icon: 'mdi mdi-account-edit',
  },
  {
    label: 'info',
    id: 2,
    icon: 'mdi mdi-information-slab-circle',
  },
  {
    label: 'security',
    id: 3,
    icon: 'mdi mdi-lock',
  },
];
</script>
