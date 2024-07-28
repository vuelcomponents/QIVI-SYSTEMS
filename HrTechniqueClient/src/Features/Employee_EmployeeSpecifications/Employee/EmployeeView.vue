<script setup lang="ts">
import { inject, ref } from 'vue';
import Dialog from 'primevue/dialog';
import type { ServiceRoots } from '@/Shared/Services/ServiceRoots';
import type { EmployeeService } from '@/Entities/Employee/EmployeeService';
import type { Employee } from '@/Entities/Employee/Employee';
import EmployeeList from './EmployeeGrid/EmployeeGrid.vue';
import EmployeeUpdateStepper from './EmployeeUpdate/EmployeeUpdate_Stepper_Modal.vue';
import EmployeeCreateStepper from './EmployeeCreate/EmployeeCreate_Stepper_Modal.vue';

const services = inject<ServiceRoots>('Services')!;
const employeeService: EmployeeService = services.create<EmployeeService>('EmployeeService')!;

const updateDialog = ref<{ open: boolean; employee: Employee | undefined }>({
  open: false,
  employee: undefined,
});
const createDialog = ref<{ open: boolean }>({ open: false });

const openUpdateDialog = (employee?: Employee) => {
  if (!employee) {
    return console.error('No employee provided with click');
  }
  updateDialog.value.open = true;
  updateDialog.value.employee = employee;
};
const openCreateDialog = () => {
  createDialog.value.open = true;
};
const emit = defineEmits<{
  created: [Employee];
}>();
const employeeList = ref<typeof EmployeeList>();
const onCreated = (employee: Employee) => {
  employeeList.value!.addEmployee(employee);
  createDialog.value.open = false;
};
const onUpdated = (employee: Employee) => {
  employeeList.value!.updateEmployee(employee);
};
</script>

<template>
  <section class="h-full">
    <EmployeeList
      ref="employeeList"
      @updateEmployee="openUpdateDialog"
      @createEmployee="openCreateDialog"
      :service="employeeService"
    />
  </section>
  <Dialog v-model:visible="updateDialog.open" modal>
    <template #header>
      <h1></h1>
    </template>
    <EmployeeUpdateStepper
      @updated="onUpdated"
      :employee="updateDialog.employee!"
      :service="employeeService"
      @close="updateDialog.open = false"
    />
  </Dialog>
  <Dialog v-model:visible="createDialog.open" modal>
    <template #header>
      <h1></h1>
    </template>
    <EmployeeCreateStepper
      @created="onCreated"
      :service="employeeService"
      @close="createDialog.open = false"
    />
  </Dialog>
</template>
