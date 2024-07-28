<script setup lang="ts">
import SomeStepper2 from '@/Components/Steppers/C-Stepper.vue';
import ViewHeader from '@/Components/Headers/VueHeader.vue';
import EmployeesView from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeView.vue';
import { useStepper } from '@/Components/Steppers/StepperComposables/useStepper';

const { stepperItems } =
  useStepper(1, [
  {
    icon: 'mdi mdi-account-multiple',
    label: 'employeesList',
    id: 1
  },
  {
    icon: 'mdi mdi-book-variant',
    label: 'employeeSpecifications',
    id: 2
  },
])

</script>

<template>
  <ViewHeader
    icon="mdi mdi-account-multiple"
    :title="$t('employees')"
    :description="$t('employeesDescription')"
  />
  <SomeStepper2
    :items="stepperItems"
    class="w-full"
  >
    <template #hrtStepper1>
      <EmployeesView/>
    </template>
    <template #hrtStepper2>
    </template>
  </SomeStepper2>

</template>
