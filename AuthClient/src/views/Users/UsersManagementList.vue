<template>
  <SomeStepper
    :items="[{ icon: 'mdi mdi-account-multiple', label: 'belongingUsersList', id: 1 }]"
    class="w-full"
  >
    <template #before>
      <div class="relative w-full">
        <Menubar :model="items" class="w-full" />
        <input-text
          @input="grid.search"
          type="search"
          v-model="grid.searchText"
          :placeholder="$t('search')"
          class="configured-input absolute right-4 top-[20%] w-[30%] lg:w-[40%]"
        />
        <span
          v-if="grid.searchText.length < 1"
          class="absolute right-6 top-[28%] text-xl dark:text-surface-100/90 md:top-[24%] lg:top-[28%]"
        >
          <i class="mdi mdi-magnify" />
        </span>
      </div>
    </template>
    <template #hrtStepper1>
      <AgGrid
        class="ag-theme-quartz h-full"
        :rowData="grid.data"
        :grid-options="grid.options as GridOptions"
        rowSelection="multiple"
        animateRows="true"
        :columnDefs="getColumns()"
      />
    </template>
  </SomeStepper>
  <Dialog v-model:visible="notificationDialog">
    <template #header>
      <h1></h1>
    </template>
    <SendNotificationDialog
      @close="notificationDialog = false"
      :user-service="service"
      :selected-users="grid.selected"
    />
  </Dialog>
</template>
<script setup lang="ts">
import { Grid, type IGrid } from '@/utils/grid/Grid';
import { computed, ref } from 'vue';
import { UserService } from '@/services/UserService';
import OrderColumnDef from '@/utils/grid/columns/UserManagementColumnDef';
import { userManagementCellClicked } from '@/utils/grid/cell-clicked/UserManagementCellClicked';
import type { CellClickedEvent, GridOptions } from 'ag-grid-community';
import Menubar from 'primevue/menubar';
import { useI18n } from 'vue-i18n';
import { type User } from '@/types/User';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import InputText from 'primevue/inputtext';
import SendNotificationDialog from '@/components/referenced/dialogs/SendNotificationDialog.vue';
import Dialog from 'primevue/dialog';
import type { AxiosResponse } from 'axios';

const { t } = useI18n({});
const props = defineProps<{
  service: UserService;
}>();

const emit = defineEmits<{
  updateUser: [];
  createUser: [];
}>();
const grid = ref<IGrid>(
  new Grid(
    {
      onCellClicked: (params: CellClickedEvent) => userManagementCellClicked(params, emit),
      onCellValueChanged: undefined,
    },
    {},
    {
      service: props.service,
    }
  )
);
const blockSelected = () => {
  props.service
    .blockSelected(grid.value.selectedToIds())
    .then((res: AxiosResponse<Array<User>>) => {
      if (res.status === 200) {
        grid.value.updateMany(res.data);
      }
    });
};
const unlockSelected = () => {
  props.service
    .unlockSelected(grid.value.selectedToIds())
    .then((res: AxiosResponse<Array<User>>) => {
      if (res.status === 200) {
        grid.value.updateMany(res.data);
      }
    });
};
const items = computed(() => [
  {
    label: t('create'),
    icon: 'mdi mdi-account-edit',
    command: () => emit('createUser'),
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
      {
        label: t('notifyUser'),
        icon: 'mdi mdi-bell-ring-outline',
        command: () => {
          grid.value.selected.length > 0
            ? (notificationDialog.value = true)
            : props.service.emitter.emit('info', {
                severity: 'info',
                summary: t(`noSelectedUsers`),
                detail: t('pleaseChooseReceiver'),
                life: 3000,
              });
        },
      },
      {
        label: t('blockAccount'),
        icon: 'mdi mdi-account-cancel-outline',
        command: blockSelected,
      },
      {
        label: t('unlockAccount'),
        icon: 'mdi mdi-account-check-outline',
        command: unlockSelected,
      },
    ],
  },
]);
const addUser = (user: User) => {
  grid.value.api!.applyTransaction({ add: [user] });
};
const updateUser = (user: User) => {
  grid.value.updateRowById(user.id, user);
};
const notificationDialog = ref(false);

defineExpose({
  addUser,
  updateUser,
});
const getColumns = () => OrderColumnDef();
</script>
