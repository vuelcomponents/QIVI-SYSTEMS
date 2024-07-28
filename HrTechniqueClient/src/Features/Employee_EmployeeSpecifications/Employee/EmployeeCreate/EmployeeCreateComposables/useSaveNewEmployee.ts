import { unfilledRequired } from '@/Components/Forms/FormsComposables/unfilledRequired';
import { type Employee, employeeFormGeneralRequired, employeeFormUpdateRequired } from '@/Entities/Employee/Employee';
import type { AxiosResponse } from 'axios';
import type { Ref } from 'vue';
import type { EmployeeService } from '@/Entities/Employee/EmployeeService';

export const useSaveNewEmployee = () => { return { saveNewEmployee } }

const saveNewEmployee = (employee:Ref<Employee>,
                         service:EmployeeService,
                         t:any,
                         emit:any
                         ) => {
  if (
    unfilledRequired<Employee>(
      [...employeeFormUpdateRequired, ...employeeFormGeneralRequired],
      employee.value,
      service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  service.create(employee.value).then((res: AxiosResponse<Employee>) => {
    if (res.status === 200 && res.data) {
      emit('created', res.data);
      service.emitter.emit('info', {
        severity: 'success',
        summary: t(`employeeCreated`),
        detail: t(`confirmationEmailHasBeenSent`),
        life: 3000,
      });
    }
  });
};