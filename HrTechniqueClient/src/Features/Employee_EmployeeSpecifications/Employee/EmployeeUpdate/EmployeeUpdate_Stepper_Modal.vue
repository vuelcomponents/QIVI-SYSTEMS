<template>
  <SomeStepper :items="stepperItems" @onStepChange="onStepChange">
    <template #before>
      <EmployeeUpdateMenubar @onSave="onSave" @onClose="emit('close')"/>
    </template>
    <template #hrtStepper1>
      <EmployeeUpdateProfile :service="service" :employee="employee"/>
    </template>
    <template #hrtStepper2>
       <EmployeeUpdateSpecifications/>
    </template>
  </SomeStepper>
</template>
<script setup lang="ts">
import {
  type Employee,
} from '@/Entities/Employee/Employee';
import type { EmployeeService } from '@/Entities/Employee/EmployeeService';
import SomeStepper from '@/Components/Steppers/V-Stepper.vue';
import EmployeeUpdateProfile
  from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeUpdate/EmployeeUpdateSteps/EmployeeUpdateProfile.vue';
import EmployeeUpdateSpecifications
  from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeUpdate/EmployeeUpdateSteps/EmployeeUpdateSpecifications.vue';
import EmployeeUpdateMenubar
  from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeUpdate/EmployeeUpdateComponents/EmployeeUpdateMenubar.vue';
import {
  useSaveExistingEmployee
} from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeUpdate/EmployeeUpdateComposables/useSaveExisitingEmployee';
import { useI18n } from 'vue-i18n';
import {
  useStepper
} from '@/Components/Steppers/StepperComposables/useStepper';

const { t } = useI18n();
const props = defineProps<{
  employee: Employee;
  service: EmployeeService;
}>();
const emit = defineEmits<{
  updated: [Employee];
  close: [];
}>();

const { stepperItems, onStepChange, currentStep }
  = useStepper(1, [
  { label: 'profile', id: 1, icon: 'mdi mdi-account' },
  { label: 'specification', id: 2, icon: 'mdi mdi-book-variant' },
])

const { saveExistingEmployee } = useSaveExistingEmployee();
const onSave = () =>{
    saveExistingEmployee(props.employee, props.service, t, emit);
}

</script>
