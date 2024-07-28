import type { Service } from '@/Shared/Services/Service';
import defaultColDef from '@/Shared/Grid/defaultColDef';
import addGridKeyListeners from '@/Shared/Grid/listeners/addGridKeyListeners';
import mainOnCellValueChanged from '@/Shared/Grid/mainOnCellValueChanged';
import type {
  CellClickedEvent,
  CellValueChangedEvent,
  GridApi,
  GridOptions,
  GridReadyEvent,
  RowNode,
  SelectionChangedEvent,
} from 'ag-grid-community';
import type { TinyEmitter } from 'tiny-emitter';
import { i18n } from '@/Shared/Translates';
export interface IGrid {
  service?: Service;
  api?: GridApi;
  selectedToIds: Function;
  deleteRowsByParam: (param: string, value: any) => void;
  updateRowByParam: (param: any, value: any, data: any) => void;
  updateRowById: (id: number, data: any) => void;
  updateMany: (list: any) => void;
  data: any;
  deleteRowsByParamMany: (param: string, values: any[]) => void;
  deleteManyApi: (noLoad?: boolean) => void;
  selected: [];
  searchText: string;
  search: () => void;
  emitter?: TinyEmitter;
  onCellClicked: (params: CellClickedEvent) => void;
  onCellValueChanged: (params: CellValueChangedEvent) => void;
  onSelectionChanged: (params: SelectionChangedEvent) => void;
  emit: (summary: string, detail: string) => void;
  loadList: () => void;
  getAllRows: () => Array<RowNode>;
  deleteSelectedRows: () => void;
  updateDialog: () => void;
  options: GridOptions;
}
export class Grid implements IGrid {
  service?: Service;
  api?: GridApi;
  selected: any = [];
  searchText = '';
  emitter?: TinyEmitter;
  updateDialog = () => {
    // console.warn('update dialog has not been implemented');
  };
  onCellClicked = (params: CellClickedEvent) => {
    // console.warn('GRID CL: on cell clicked has not been implemented', params);
  };
  onCellValueChanged = (params: CellValueChangedEvent) => {
    // console.warn('GRID CL: on cell value changed has not been implemented');
    // console.warn('GRID CL: no update method has been triggered', params);
    //
    // if (this.service) {
    //   this.service.update(params.data, true).then((res) => {
    //     switch (res?.status) {
    //       case 200:
    //         params.node.setData(res.data);
    //         return;
    //       default:
    //         params.node.data[params.colDef.field!] = params.oldValue;
    //         params.node.setData(params.node.data);
    //         return;
    //     }
    //   });
    // }
  };
  onSelectionChanged = (params: SelectionChangedEvent) => {
    if (!params.api.isDestroyed()) this.selected = params.api.getSelectedRows();
  };

  emit = (summary: string, detail: string) => {
    if (this.emitter) {
      this.emitter.emit('error', { severity: 'error', summary, detail, life: 3000 });
    }
  };
  constructor(events: any, functions: any, $: any) {
    if (events?.updateDialog) {
      this.updateDialog = events.updateDialog;
    }
    if (events?.onCellValueChanged) {
      this.onCellValueChanged = events.onCellValueChanged;
    }
    if (events?.onCellClicked) {
      this.onCellClicked = events.onCellClicked;
    }
    if ($?.emitter) {
      this.emitter = $.emitter;
    }
    if (functions.loadList) {
      this.loadList = functions.loadList;
    }
    if (!$?.service) {
      // console.warn('GRID CLASS: Service has not been provided');
      // this.emit('Service error', 'Service has not been provided to grid');
      return;
    }
    this.service = $.service;
  }

  loadList = async () => {
    if (!this.service) return;
    const list = (await this.service!.getAll()).data;
    this.api!.setGridOption('rowData', list);
  };
  getAllRows = (): Array<RowNode> => {
    const rowData: any[] = [];
    this.api!.forEachNode((node) => rowData.push(node.data));
    return rowData;
  };
  deleteSelectedRows = () => {
    const data = this.getAllRows().filter((d) => !this.selected.some((r: any) => r.id === d.id));
    this.api!.setGridOption('rowData', data);
  };
  deleteRowsByIds = (idDtos: any) => {
    const data = this.getAllRows().filter((d) => !idDtos.some((r: any) => r.id === d.id));
    this.api!.setGridOption('rowData', data);
  };
  deleteRowsByParam = (param: string, value: any) => {
    const data = this.getAllRows().filter((d: any) => d[param] !== value);
    this.api!.setGridOption('rowData', data);
  };
  deleteRowsByParamMany = (param: string, values: any[]) => {
    const data = this.getAllRows().filter(
      (d: any) => !values.some((value) => d[param] === value.id)
    );
    this.api!.setGridOption('rowData', data);
  };
  deleteManyApi = (noLoad?: boolean) => {
    if (noLoad) {
      return this.service!.deleteMany(this.selectedToIds()).then((res: any) => {
        if (res.status === 200) {
          this.api!.applyTransaction({ remove: this.selected });
        }
      });
    }
    return this.service!.deleteMany(this.selectedToIds()).then((res) => {
      this.loadList();
    });
  };
  updateRowById = (id: number, data: any) => {
    this.api!.forEachNode((node) => {
      if (node.data.id === id) {
        node.setData(data);
      }
    });
  };
  updateMany = (list: any) => {
    list.forEach((data: any) => {
      this.api!.forEachNode((node) => {
        if (node.data.id === data.id) {
          node.setData(data);
        }
      });
    });
  };
  updateRowByParam = (param: any, value: any, data: any) => {
    this.api!.forEachNode((node) => {
      if (node.data[param] === value) {
        node.setData(data);
      }
    });
  };
  selectedToIds = (): Array<any> => {
    const ids: any[] = [];
    this.selected.forEach((s: any) => ids.push({ id: s.id }));
    return ids;
  };
  search = () => {
    this.api!.setQuickFilter(this.searchText);
  };

  options: GridOptions<any> = {
    localeText: (i18n.global.messages as any).value[(i18n.global.locale as any).value],
    pagination: true,
    paginationPageSize: 20,
    onGridReady: async (params: GridReadyEvent) => {
      this.api = params.api;
      addGridKeyListeners(params.api);
      await this.loadList();
      console.log('grid data', this.getAllRows());
    },
    onCellValueChanged: (params: CellValueChangedEvent) => {
      if (!params.data) {
        return;
      }
      if (this.onCellValueChanged) {
        this.onCellValueChanged(params);
        return;
      }
      if (this.service) {
        mainOnCellValueChanged(params, { service: this.service });
      }
    },
    onCellClicked: (params: CellClickedEvent) => {
      this.onCellClicked(params);
    },
    onSelectionChanged: (params: SelectionChangedEvent) => {
      this.onSelectionChanged(params);
    },
    // getRowStyle: (params: any) => {
    // if (params.node.rowIndex % 2 === 0) {
    //   return { background: '#27344b', color:'#fff' , borderBottom:'solid 2px #2e3c54'};
    // } else {
    //   return { background: '#2a374f', color:'#fff', borderBottom:'solid 2px #2e3c54' };
    // }
    // },
    defaultColDef,
    // sideBar: {
    //   toolPanels: ['filters']
    // }
  };
  data = [];
}
