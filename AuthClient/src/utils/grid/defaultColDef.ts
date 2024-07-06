import type { CellClassParams, ColDef, SuppressKeyboardEventParams } from 'ag-grid-community';

const colDef: ColDef = {
  // flex: 1,
  minWidth: 75,
  resizable: true,
  menuTabs: ['filterMenuTab'],
  filter: true,
  sortable: true,
  // headerClass:'grid-itm-header',
  editable: false,
  valueGetter: (params: any) => {
    const value = params.data[params.colDef.field];
    if (value) {
      switch (true) {
        case !isNaN(value):
          return value.toLocaleString();
        case !isNaN(Date.parse(value)):
          return `${new Date(value).toLocaleDateString()} ${new Date(value).toLocaleTimeString()}`;
      }
    }
    return value;
  },
  suppressKeyboardEvent: (params: SuppressKeyboardEventParams): any => {
    if (params.event?.key === 'Enter') {
      return false;
    }
    return true;
  },
  // eslint-disable-next-line no-unused-vars
  cellStyle: (params: CellClassParams<any, any>): any => {
    return {
      textAlign: 'left',
    };
  },
};
export default colDef;
