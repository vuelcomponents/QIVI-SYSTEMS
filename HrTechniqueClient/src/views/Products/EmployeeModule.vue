<script setup lang="ts">

import SomeStepper2 from '@/components/reusable/steppers/SomeStepperComfortable.vue';
import { computed } from 'vue';
import ViewHeader from '@/components/reusable/headers/VueHeader.vue';
import EmployeesView from '@/views/Products/Employees/EmployeeView.vue';
const stepperItems = computed(()=>
  [
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
