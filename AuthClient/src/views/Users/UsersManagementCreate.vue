<template>
  <SomeStepper :items="stepperItems">
    <template #before>
      <Menubar :model="items" class="mb-2 w-full" />
    </template>
    <template #hrtStepper1>
      <section class="grid h-fit w-full grid-cols-4 gap-2 2xl:col-span-4">
        <div
          v-for="param in [...userCreateFormStrings, ...userFormStrings]"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-[0.8em] text-west-3 dark:text-surface-200/50">
            <i
              class="text-red-700"
              v-if="[...userFormCreateRequired, ...userFormGeneralRequired].includes(param.field)"
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
            class="w-full text-west-3 dark:text-surface-200"
          />
        </div>
      </section>
    </template>
    <template #hrtStepper2>
      <div class="mt-5 h-fit">
        <PickList
          v-model="licences"
          @update:model-value="onLicenceUpdate"
          listStyle="height:342px"
          dataKey="id"
          breakpoint="1400px"
        >
          <template #sourceheader>
            {{ $t('available') }}
          </template>
          <template #targetheader>
            {{ $t('permitted') }}
          </template>
          <template #item="slotProps">
            <div class="align-items-center flex flex-wrap gap-3 rounded-xl p-2 dark:bg-black/20">
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
  </SomeStepper>
</template>
<script setup lang="ts">
import {
  type User,
  userFormCreateRequired,
  userFormGeneralRequired,
  userFormStrings,
  userFormUpdateRequired,
  userCreateFormStrings,
  type UserUpdate,
  type Licence,
} from '@/types/User';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import InputText from 'primevue/inputtext';
import Menubar from 'primevue/menubar';
import type { UserService } from '@/services/UserService';
import { ref } from 'vue';
import type { AxiosResponse } from 'axios';
import { useI18n } from 'vue-i18n';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import PickList from 'primevue/picklist';
import { UseUserStore } from '@/stores/UserStore';

const props = defineProps<{
  service: UserService;
}>();
const emit = defineEmits<{
  created: [User];
  close: [];
}>();
const { t } = useI18n({});
const user = ref<User>({ licences: new Array<Licence>() } as User);
const userStore = UseUserStore();
const admin = userStore.user;
const licences = ref([admin.licences, []]);

const onLicenceUpdate = () => {
  user.value.licences = licences.value[1];
};
const saveNewUser = () => {
  if (
    unfilledRequired<User>(
      [...userFormUpdateRequired, ...userFormGeneralRequired],
      user.value,
      props.service.emitter,
      t
    )
  ) {
    return;
  }
  props.service.create(user.value).then((res: AxiosResponse<User>) => {
    if (res.status === 200 && res.data) {
      emit('created', res.data);
      props.service.emitter.emit('info', {
        severity: 'success',
        summary: t(`userCreated`),
        detail: t(`confirmationEmailHasBeenSent`),
        life: 3000,
      });
    }
  });
};
const stepperItems = [
  { label: 'profile', id: 1, icon: 'mdi mdi-account' },
  { label: 'licences', id: 2, icon: 'mdi mdi-file-document-check' },
];
const items = [
  {
    label: t('saveNewUser'),
    icon: 'mdi mdi-content-save',
    command: saveNewUser,
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('close'),
  },
];
</script>
