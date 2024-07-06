<template>
  <SomeStepper :items="stepperItems" @onStepChange="onStepChange">
    <template #before>
      <Menubar :model="items" class="mb-2 w-full" />
    </template>
    <template #hrtStepper1>
      <section class="grid h-fit w-full grid-cols-4 gap-2 2xl:col-span-4">
        <div
          v-for="param in userFormStrings"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="text-france-3 m-0 -mb-1 flex gap-1 p-0 text-[0.8em] dark:text-surface-100/50">
            <i
              class="text-red-700"
              v-if="[...userFormGeneralRequired, ...userFormUpdateRequired].includes(param.field)"
            >
              *
            </i>
            {{ $t(`${param.field}`) }}
          </div>
          <input-text
            :id="`hrt-form-${param.field!}`"
            v-model="(user as UserUpdate)[param.field!]"
            :minlength="param.min ?? 0"
            :maxlength="param.max"
            class="w-full"
          />
        </div>
      </section>
    </template>
    <template #hrtStepper2>
      <div class="h-fit py-5">
        <PickList
          v-model="licences"
          @update:model-value="onLicenceUpdate"
          listStyle="height:342px"
          dataKey="id"
          breakpoint="1400px"
        >
          <template #sourceheader> {{ $t('available') }} </template>
          <template #targetheader> {{ $t('permitted') }} </template>
          <template #item="slotProps">
            <div class="align-items-center flex flex-wrap gap-3 p-2">
              <div class="flex-column flex flex-1 gap-2">
                <span class="font-bold">
                  <i :class="slotProps.item.icon" />
                  {{ slotProps.item.code }}
                </span>
              </div>
            </div>
          </template>
        </PickList>
      </div>
    </template>
    <template #hrtStepper3>
      <div class="h-fit">
        <UserManagementPreferences owned-user :user="props.user" />
        <UserManagementDevicesGrid :user-service="props.service" :user="props.user" />
      </div>
    </template>
  </SomeStepper>
  <Dialog v-model:visible="notificationDialog">
    <template #header>
      <h1></h1>
    </template>
    <SendNotificationDialog
      @close="notificationDialog = false"
      :user-service="service"
      :selected-users="[props.user]"
    />
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
import UserManagementDevicesGrid from '@/views/Users/UserManagementDevicesGrid.vue';
import UserManagementPreferences from '@/views/Users/UserPreferences.vue';
import InputText from 'primevue/inputtext';
import Menubar from 'primevue/menubar';
import type { UserService } from '@/services/UserService';
import type { AxiosResponse } from 'axios';
import { useI18n } from 'vue-i18n';
import PickList from 'primevue/picklist';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import { computed, ref } from 'vue';
import Dialog from 'primevue/dialog';
import SendNotificationDialog from '@/components/referenced/dialogs/SendNotificationDialog.vue';
import { UseUserStore } from '@/stores/UserStore';

const props = defineProps<{
  user: User;
  service: UserService;
}>();
const { t } = useI18n({});
const emit = defineEmits<{
  updated: [User];
  close: [];
}>();
const notificationDialog = ref(false);
const userStore = UseUserStore();
const admin = userStore.user;
const licences = ref([
  admin.licences.filter((al) => !props.user.licences.some((l) => al.id === l.id)),
  props.user.licences,
]);

const stepperItems = [
  { label: 'profile', id: 1, icon: 'mdi mdi-account' },
  { label: 'licences', id: 2, icon: 'mdi mdi-file-document-check' },
  { label: 'security', id: 3, icon: 'mdi mdi-lock' },
];

const onLicenceUpdate = () => {
  props.user.licences = licences.value[1];
};
const saveChanges = () => {
  if (
    unfilledRequired<User>(
      [...userFormUpdateRequired, ...userFormGeneralRequired],
      props.user,
      props.service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  props.service.update(props.user).then((res: AxiosResponse<User>) => {
    if (res.status === 200 && res.data) {
      emit('updated', res.data);
    }
  });
};
const currentStep = ref<number>(1);
const onStepChange = (id: number) => {
  currentStep.value = id;
};
const items = computed(() => [
  {
    label: t('saveChanges'),
    icon: 'mdi mdi-content-save',
    command: saveChanges,
  },
  {
    label: t('actions'),
    icon: 'mdi mdi-wrench',
    items: [
      {
        label: t('notifyUser'),
        icon: 'mdi mdi-bell-ring-outline',
        command: () => {
          notificationDialog.value = true;
        },
      },
    ],
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('close'),
  },
]);
</script>
