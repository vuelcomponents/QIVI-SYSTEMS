import type { CellClickedEvent } from 'ag-grid-community';
import type { User } from '@/types/User';

export const employeeCellClicked = (params: CellClickedEvent, emit: Function) => {
  switch (true) {
    case params.colDef.field === 'updateEmployee':
      emit('updateEmployee', params.data as User);
      break;
  }
};
