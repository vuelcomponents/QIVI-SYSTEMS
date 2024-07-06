<template>
  <div>
    <SplitButton :model="items" severity="link">
      <span v-clickbrother class="align-items-center flex font-bold">
        <i class="mdi mdi-wrench mr-2" />
        <span>{{ $t('actions') }}</span>
      </span>
    </SplitButton>
    <div class="max-h-[80%]">
      <AgGrid
        class="ag-theme-quartz h-full"
        :rowData="devicesGrid.data"
        domLayout="autoHeight"
        :grid-options="devicesGrid.options"
        rowSelection="multiple"
        animateRows="true"
        :columnDefs="getDevicesColumns()"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { Grid, type IGrid } from '@/utils/grid/Grid';
import DevicesColumnDef from '@/utils/grid/columns/DevicesColumnDef';
import SplitButton from 'primevue/splitbutton';
import { useI18n } from 'vue-i18n';
import type { AuthService } from '@/services/AuthService';
import type { AxiosResponse } from 'axios';

const { t } = useI18n({});
const props = defineProps<{
  authService: AuthService;
}>();
const loadDevicesList = () => {
  props.authService.getDevices().then((res: AxiosResponse<any>) => {
    if (res.status === 200) {
      devicesGrid.value.api!.setGridOption('rowData', res.data);
    }
  });
};
const devicesGrid = ref<IGrid>(
  new Grid(
    {
      onCellClicked: undefined,
      onCellValueChanged: undefined,
    },
    {
      loadList: loadDevicesList,
    },
    {
      service: undefined,
    }
  )
);

const actions = {
  blockDevices: () => {
    props.authService
      .blockDevices(devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<any>) => {
        devicesGrid.value.updateMany(res.data);
      });
  },
  unlockDevices: () => {
    props.authService
      .unlockDevices(devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<any>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
  blockIps: () => {
    props.authService
      .blockIps(devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<any>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
  unlockIps: () => {
    props.authService
      .unlockIps(devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<any>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
  verifyDevices: () => {
    props.authService
      .verifyDevices(devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<any>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
};

const items = [
  {
    label: t('selected'),
    icon: 'mdi mdi-order-bool-descending-variant',
    items: [
      {
        label: t('devices'),
        icon: 'mdi mdi-cellphone-link',
        items: [
          {
            label: t('verifyDevice'),
            icon: 'mdi mdi-mail',
            command: actions.verifyDevices,
          },
          {
            label: t('blockDevice'),
            icon: 'mdi mdi-lock',
            command: actions.blockDevices,
          },
          {
            label: t('unlockDevice'),
            icon: 'mdi mdi-lock-open',
            command: actions.unlockDevices,
          },
        ],
      },
      {
        label: t('networks'),
        icon: 'mdi mdi-ip-network',
        items: [
          {
            label: t('blockIpAddress'),
            icon: 'mdi mdi-lock',
            command: actions.blockIps,
          },
          {
            label: t('unlockIpAddress'),
            icon: 'mdi mdi-lock-open',
            command: actions.unlockIps,
          },
        ],
      },
    ],
  },
];

const getDevicesColumns = () => DevicesColumnDef(t);
</script>
