<script setup lang="ts">
import InputNumber from 'primevue/inputnumber';
import Calendar from 'primevue/calendar';
import InputText from 'primevue/inputtext';
import {
  type Employee, employeeFormDates,
  employeeFormGeneralRequired,
  employeeFormNumbers,
  employeeFormStrings,
  employeeFormUpdateRequired, employeeUpdateFormNumbers,
} from '@/Entities/Employee/Employee';
import type { EmployeeService } from '@/Entities/Employee/EmployeeService';

const props = defineProps<{
  employee: Employee;
  service: EmployeeService;
}>();

const settingStore = props.service.storeHandler.settingStore;

</script>

<template>
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