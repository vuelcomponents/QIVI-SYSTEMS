<template>
  <SomeStepper :items="stepperItems" @onStepChange="onStepChange">
    <template #before>
      <Menubar :model="items" class="mb-2 w-full" />
    </template>
    <template #hrtStepper1>
      <section class="grid h-fit w-full grid-cols-4 gap-2 2xl:col-span-4">
        <div
          v-for="param in employeeFormStrings"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-sm">
            <i
              class="text-red-700"
              v-if="[...employeeFormGeneralRequired, ...employeeFormUpdateRequired].includes(param.field)"
            >
              *
            </i>
            {{ $t(`${param.field}`) }}
          </div>
          <input-text
            :id="`hrt-form-${param.field!}`"
            v-model="(employee as any)[param.field!]"
            :minlength="param.min ?? 0"
            :maxlength="param.max"
            class="w-full"
          />
        </div>
        <div
          v-for="param in [...employeeUpdateFormNumbers, ...employeeFormNumbers]"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-sm">
            <i
              class="text-red-700"
              v-if="[...employeeFormUpdateRequired, ...employeeFormGeneralRequired].includes(param.field)"
            >
              *
            </i>
            {{ $t(`${param.field}`) }}
          </div>
          <input-number
            :id="`hrt-form-${param.field!}`"
            v-model="(employee as any)[param.field!]"
            class="w-full input-align-right"
            :max-fraction-digits="2"
            :min-fraction-digits="2"
            :suffix="settingStore.currency"
          />
        </div>
        <div
          v-for="param in [...employeeFormDates]"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-sm">
            <i
              class="text-red-700"
              v-if="[...employeeFormUpdateRequired, ...employeeFormGeneralRequired].includes(param.field)"
            >
              *
            </i>
            {{ $t(`${param.field}`) }}
          </div>
          <calendar
            :id="`hrt-form-${param.field!}`"
            v-model="(employee as any)[param.field!]"
            class="w-full input-align-right"

          />
        </div>
      </section>
    </template>
    <template #hrtStepper2>

    </template>
    <template #hrtStepper3>
      <div class="h-fit">

      </div>
    </template>
  </SomeStepper>
</template>
<script setup lang="ts">
import {
  type Employee, employeeFormCreateRequired, employeeFormDates,
  employeeFormGeneralRequired,
  employeeFormNumbers,
  employeeFormStrings,
  employeeFormUpdateRequired, employeeUpdateFormNumbers,
} from '@/types/Employee';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import InputText from 'primevue/inputtext';
import Menubar from 'primevue/menubar';
import type { EmployeeService } from '@/services/EmployeeService';
import type { AxiosResponse } from 'axios';
import { useI18n } from 'vue-i18n';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import { computed, ref } from 'vue';
import InputNumber from 'primevue/inputnumber';
import Calendar from 'primevue/calendar';
const props = defineProps<{
  employee: Employee;
  service: EmployeeService;
}>();
const settingStore = props.service.storeHandler.settingStore;
const { t } = useI18n({});
const emit = defineEmits<{
  updated: [Employee];
  close: [];
}>();

const stepperItems = [
  { label: 'profile', id: 1, icon: 'mdi mdi-account' },
  { label: 'specification', id: 2, icon: 'mdi mdi-book-variant' },
];

const saveChanges = () => {
  if (
    unfilledRequired<Employee>(
      [...employeeFormUpdateRequired, ...employeeFormGeneralRequired],
      props.employee,
      props.service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  props.service.update(props.employee).then((res: AxiosResponse<Employee>) => {
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
    items:[]
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('close'),
  },
]);
</script>
