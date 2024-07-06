<template>
  <div>
    <h1 class="text-lg">{{ $t('ownedLicences') }}</h1>
    <h1 class="mb-5 text-sm">{{ $t('ownedLicencesDescription') }}</h1>
    <div class="max-h-[80%]">
      <AgGrid
        class="ag-theme-quartz h-full"
        :rowData="licencesGrid.data"
        domLayout="autoHeight"
        :grid-options="licencesGrid.options"
        rowSelection="multiple"
        animateRows="true"
        :columnDefs="getLicencesColumns()"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { Grid, type IGrid } from '@/utils/grid/Grid';
import type { User } from '@/types/User';
import { useI18n } from 'vue-i18n';
import type { AuthService } from '@/services/AuthService';
import LicencesColumnDef from '@/utils/grid/columns/LicencesColumnDef';
const props = defineProps<{
  user: User;
  authService: AuthService;
}>();
const { t } = useI18n({});
const loadLicencesList = () => {
  licencesGrid.value.api!.setGridOption('rowData', props.user.licences);
};

const licencesGrid = ref<IGrid>(
  new Grid(
    {
      onCellClicked: undefined,
      onCellValueChanged: undefined,
    },
    {
      loadList: loadLicencesList,
    },
    {
      service: undefined,
    }
  )
);
const getLicencesColumns = () => LicencesColumnDef(t);
</script>
