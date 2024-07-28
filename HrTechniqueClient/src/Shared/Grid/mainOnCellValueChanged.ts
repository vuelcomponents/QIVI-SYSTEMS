import type { CellValueChangedEvent } from 'ag-grid-community';

export default (params: CellValueChangedEvent, $: any) => {
  console.log('main on cell value changed not implemented', params, $);
};
