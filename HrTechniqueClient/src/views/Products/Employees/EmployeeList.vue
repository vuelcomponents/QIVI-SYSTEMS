<template>
  <div class="relative mb-[5px] w-full">
    <Menubar :model="items" class="w-full" />
    <input-text
      @input="grid.search"
      type="search"
      v-model="grid.searchText"
      :placeholder="$t('search')"
      class="configured-input absolute right-0 top-[25%] mr-5 w-[30%] lg:w-[60%]"
    />
    <span
      v-if="grid.searchText.length < 1"
      class="absolute right-[10px] top-[37%] mr-5 dark:text-surface-100/30 md:top-[33%] lg:top-[31%]"
    >
      <i class="mdi mdi-magnify" />
    </span>
  </div>
  <AgGrid
        class="ag-theme-quartz h-full"
        style="height:calc(100% - 50px)"
        :rowData="grid.data"
        :grid-options="grid.options as GridOptions"
        rowSelection="multiple"
        animateRows="true"
        :columnDefs="getColumns()"
      />



</template>
<script setup lang="ts">
import { Grid, type IGrid } from '@/utils/grid/Grid';
import { computed, ref } from 'vue';
import { EmployeeService } from '@/services/EmployeeService';
import type { CellClickedEvent, GridOptions } from 'ag-grid-community';
import Menubar from 'primevue/menubar';
import { useI18n } from 'vue-i18n';
import { type Employee } from '@/types/Employee';
import InputText from 'primevue/inputtext';
import EmployeeColumnDef from '@/utils/grid/columns/EmployeeColumnDef';
import { employeeCellClicked } from '@/utils/grid/cell-clicked/employeeCellClicked';
const stepperItems = computed(()=>
  [
    {
      icon: 'mdi mdi-escalator-box',
      label: 'belongingEmployeesList',
      id: 1
    },
    {
      icon: 'mdi mdi-source-branch',
      label: 'employeeCategories',
      id: 2
    },
    {
      icon: 'mdi mdi-book-variant',
      label: 'employeeSpecifications',
      id: 3
    },
  ])
const { t } = useI18n({});
const props = defineProps<{
  service: EmployeeService;
}>();

const emit = defineEmits<{
  updateEmployee: [];
  createEmployee: [];
}>();
const grid = ref<IGrid>(
  new Grid(
    {
      onCellClicked: (params: CellClickedEvent) => employeeCellClicked(params, emit),
      onCellValueChanged: undefined,
    },
    {},
    {
      service: props.service,
    }
  )
);

const items = computed(()=>[
  {
    label: t('create'),
    icon: 'mdi mdi-account-edit',
    command: () => emit('createEmployee'),
  },
  {
    label: t('actions'),
    icon: 'mdi mdi-wrench',
    items: [
      {
        label: t('removeSelected'),
        icon: 'mdi mdi-minus',
        command: () => grid.value.deleteManyApi(true),
      },
    ],
  },
]);
const addEmployee = (employee: Employee) => {
  grid.value.api!.applyTransaction({ add: [employee] });
};
const updateEmployee = (employee: Employee) => {
  grid.value.updateRowById(employee.id, employee);
};

defineExpose({
  addEmployee,
  updateEmployee,
});
const getColumns = () => EmployeeColumnDef(t);
</script>
