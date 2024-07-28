import type { GridApi } from 'ag-grid-community';
export default (api: GridApi) => {
  document.addEventListener('keydown', (event: KeyboardEvent) => {
    const selected = api.getSelectedRows();
    switch (true) {
      case event.key === 'W' && event.shiftKey && selected.length > 0:
        console.log('combination pressed on grid');
        break;
    }
  });
};
