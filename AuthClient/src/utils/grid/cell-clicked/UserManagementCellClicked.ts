import type { CellClickedEvent } from 'ag-grid-community';
import type { User } from '@/types/User';

export const userManagementCellClicked = (params: CellClickedEvent, emit: Function) => {
  switch (true) {
    case params.colDef.field === 'updateUser':
      emit('updateUser', params.data as User);
      break;
  }
};
