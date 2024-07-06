<script setup lang="ts">
import { Grid, type IGrid } from '@/utils/grid/Grid';
import { ref } from 'vue';
import type { UserStats } from '@/types/Stats';
import { useI18n } from 'vue-i18n';
const props = defineProps<{
  users: UserStats[];
}>();

const grid = ref<IGrid>(new Grid({}, {}, {}));

const { t } = useI18n();
const getStatisticsColumns = () => [
  {
    headerName: t('online'),
    field: 'email',
    flex: 1,
  },
  {
    headerName: t('devicesOnline'),
    field: 'activeDeviceCount',
    headerClass: 'ag-right-aligned-header',
    cellStyle: { textAlign: 'right' },
    cellClass: 'ag-right-aligned-cell',
  },
];
</script>

<template>
  <div class="grid-ag h-full">
    <AgGrid
      class="ag-theme-quartz h-full"
      :rowData="props.users"
      :grid-options="grid.options"
      rowSelection="multiple"
      animateRows="true"
      :columnDefs="getStatisticsColumns()"
    />
  </div>
</template>
