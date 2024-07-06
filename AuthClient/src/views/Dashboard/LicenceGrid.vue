<script setup lang="ts">
import { Grid, type IGrid } from '@/utils/grid/Grid';
import { ref } from 'vue';
import type { UserStats } from '@/types/Stats';
import { useI18n } from 'vue-i18n';
import { UseUserStore } from '@/stores/UserStore';
import LicencesColumnDef from '@/utils/grid/columns/LicencesColumnDef';
const props = defineProps<{
  users: UserStats[];
}>();

const grid = ref<IGrid>(new Grid({}, {}, {}));
const user = UseUserStore().user;

const { t } = useI18n();
const getStatisticsColumns = () => [
  {
    headerName: t('description'),
    field: 'description',
    flex: 1,
  },
  {
    headerName: t('code'),
    field: 'code',
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
      :rowData="user.licences"
      :grid-options="grid.options"
      rowSelection="multiple"
      animateRows="true"
      :columnDefs="LicencesColumnDef($t)"
    />
  </div>
</template>
