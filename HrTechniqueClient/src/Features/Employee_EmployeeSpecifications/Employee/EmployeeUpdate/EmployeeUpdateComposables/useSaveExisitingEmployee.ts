import { unfilledRequired } from '@/Components/Forms/FormsComposables/unfilledRequired';
import { type Employee, employeeFormGeneralRequired, employeeFormUpdateRequired } from '@/Entities/Employee/Employee';
import type { AxiosResponse } from 'axios';
import type { EmployeeService } from '@/Entities/Employee/EmployeeService';

export const useSaveExistingEmployee = () => { return { saveExistingEmployee } }

const saveExistingEmployee = (employee:Employee,
                               service:EmployeeService,
                               t:any,
                               emit:any) => {
  if (
    unfilledRequired<Employee>(
      [...employeeFormUpdateRequired, ...employeeFormGeneralRequired],
      employee,
      service.emitter,
      t,
      true
    )
  ) {
    return;
  }
  service.update(employee).then((res: AxiosResponse<Employee>) => {
    if (res.status === 200 && res.data) {
      emit('updated', res.data);
    }
  });
};