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
        :grid-options="devicesGrid.options as GridOptions"
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
import type { AxiosResponse } from 'axios';
import type { Device, User } from '@/types/User';
import type { UserService } from '@/services/UserService';
import type { GridOptions } from 'ag-grid-community';

const { t } = useI18n({});
const props = defineProps<{
  userService: UserService;
  user: User;
}>();
const loadDevicesList = () => {
  props.userService.getUserDevices(props.user.id).then((res: AxiosResponse<Array<Device>>) => {
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
    props.userService
      .blockUserDevices(props.user.id, devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<Array<Device>>) => {
        devicesGrid.value.updateMany(res.data);
      });
  },
  unlockDevices: () => {
    props.userService
      .unlockUserDevices(props.user.id, devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<Array<Device>>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
  blockIps: () => {
    props.userService
      .blockUserIps(props.user.id, devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<Array<Device>>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
  unlockIps: () => {
    props.userService
      .unlockUserIps(props.user.id, devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<Array<Device>>) => {
        if (res.status === 200 && res.data) {
          devicesGrid.value.updateMany(res.data);
        }
      });
  },
  verifyDevices: () => {
    props.userService
      .verifyDevices(props.user.id, devicesGrid.value.selectedToIds())
      .then((res: AxiosResponse<Array<Device>>) => {
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
            label: t('verifyDeviceAdmin'),
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
