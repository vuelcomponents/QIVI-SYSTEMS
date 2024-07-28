<template>
  <SomeStepper :items="stepperItems">
    <template #before>
      <EmployeeCreateMenubar @onSave="onSave" @onClose="()=> emit('close')"/>
    </template>
    <template #hrtStepper1>
      <EmployeeCreateProfile :service="service" :employee="employee"/>
    </template>
    <template #hrtStepper2>
      <EmployeeCreateSpecifications/>
    </template>
  </SomeStepper>
</template>
<script setup lang="ts">
import {
  type Employee,
} from '@/Entities/Employee/Employee';
import type { EmployeeService } from '@/Entities/Employee/EmployeeService';
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';
import SomeStepper from '@/Components/Steppers/V-Stepper.vue';
import EmployeeCreateProfile
  from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeCreate/EmployeeCreateSteps/EmployeeCreateProfile.vue';
import EmployeeCreateSpecifications
  from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeCreate/EmployeeCreateSteps/EmployeeCreateSpecifications.vue';
import {
  useSaveNewEmployee
} from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeCreate/EmployeeCreateComposables/useSaveNewEmployee';
import EmployeeCreateMenubar
  from '@/Features/Employee_EmployeeSpecifications/Employee/EmployeeCreate/EmployeeCreateComponents/EmployeeCreateMenubar.vue';
import { useStepper } from '@/Components/Steppers/StepperComposables/useStepper';
const { t } = useI18n({});
const props = defineProps<{
  service: EmployeeService;
}>();
const emit = defineEmits<{
  created: [Employee];
  close: [];
}>();

const employee = ref<Employee>({} as Employee);

const { stepperItems, onStepChange, currentStep } =
  useStepper(1, [
  { label: 'info', id: 1, icon: 'mdi mdi-information' },
  { label: 'specification', id: 2, icon: 'mdi mdi-book-variant' },
])

const { saveNewEmployee } = useSaveNewEmployee();
const onSave = () =>{
  saveNewEmployee(employee, props.service, t, emit)
}
</script>
