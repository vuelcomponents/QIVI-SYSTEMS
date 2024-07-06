<template>
  <SomeStepper :items="stepperItems">
    <template #before>
      <Menubar :model="items" class="mb-2 w-full" />
    </template>
    <template #hrtStepper1>
      <section class="grid h-fit w-full grid-cols-4 gap-2 2xl:col-span-4">
        <div
          v-for="param in [...employeeCreateFormStrings, ...employeeFormStrings]"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-sm">
            <i
              class="text-red-700"
              v-if="[...employeeFormCreateRequired, ...employeeFormGeneralRequired].includes(param.field)"
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
          v-for="param in [...employeeCreateFormNumbers, ...employeeFormNumbers]"
          :class="['flex w-full flex-col items-start justify-end gap-2', param.styles]"
        >
          <div class="m-0 -mb-1 flex gap-1 p-0 text-sm">
            <i
              class="text-red-700"
              v-if="[...employeeFormCreateRequired, ...employeeFormGeneralRequired].includes(param.field)"
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
              v-if="[...employeeFormCreateRequired, ...employeeFormGeneralRequired].includes(param.field)"
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
  </SomeStepper>
</template>
<script setup lang="ts">
import {
  type Employee, employeeCreateFormNumbers,
  employeeCreateFormStrings,
  employeeFormCreateRequired, employeeFormDates,
  employeeFormGeneralRequired, employeeFormNumbers,
  employeeFormStrings,
  employeeFormUpdateRequired,
} from '@/types/Employee';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import InputText from 'primevue/inputtext';
import Menubar from 'primevue/menubar';
import type { EmployeeService } from '@/services/EmployeeService';
import { ref } from 'vue';
import type { AxiosResponse } from 'axios';
import { useI18n } from 'vue-i18n';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import InputNumber from 'primevue/inputnumber';
import Calendar from 'primevue/calendar';
const props = defineProps<{
  service: EmployeeService;
}>();
const settingStore = props.service.storeHandler.settingStore;

const emit = defineEmits<{
  created: [Employee];
  close: [];
}>();
const { t } = useI18n({});
const employee = ref<Employee>({} as Employee);



const saveNewEmployee = () => {
  if (
    unfilledRequired<Employee>(
      [...employeeFormUpdateRequired, ...employeeFormGeneralRequired],
      employee.value,
      props.service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  props.service.create(employee.value).then((res: AxiosResponse<Employee>) => {
    if (res.status === 200 && res.data) {
      emit('created', res.data);
      props.service.emitter.emit('info', {
        severity: 'success',
        summary: t(`employeeCreated`),
        detail: t(`confirmationEmailHasBeenSent`),
        life: 3000,
      });
    }
  });
};
const stepperItems = [
  { label: 'info', id: 1, icon: 'mdi mdi-information' },
  { label: 'specification', id: 2, icon: 'mdi mdi-book-variant' },
];
const items = [
  {
    label: t('saveNewEmployee'),
    icon: 'mdi mdi-content-save',
    command: saveNewEmployee,
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('close'),
  },
];
</script>
