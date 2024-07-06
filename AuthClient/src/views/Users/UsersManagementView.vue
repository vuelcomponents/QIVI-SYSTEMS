<script setup lang="ts">
import UsersManagementList from '@/views/Users/UsersManagementList.vue';
import ViewHeader from '@/components/reusable/headers/VueHeader.vue';
import type { User } from '@/types/User';
import { inject, ref } from 'vue';
import Dialog from 'primevue/dialog';
import UsersManagementUpdate from '@/views/Users/UsersManagementUpdate.vue';
import type { ServiceRoots } from '@/services/ServiceRoots';
import { UserService } from '@/services/UserService';
import UsersManagementCreate from '@/views/Users/UsersManagementCreate.vue';

const servies = inject<ServiceRoots>('services')!;
const userService: UserService = servies.create<UserService>('UserService')!;

const updateDialog = ref<{ open: boolean; user: User | undefined }>({
  open: false,
  user: undefined,
});
const createDialog = ref<{ open: boolean }>({ open: false });

const openUpdateDialog = (user?: User) => {
  if (!user) {
    return console.error('No user provided with click');
  }
  updateDialog.value.open = true;
  updateDialog.value.user = user;
};
const openCreateDialog = () => {
  createDialog.value.open = true;
};
const emit = defineEmits<{
  created: [User];
}>();
const userManagementList = ref<typeof UsersManagementList>();
const onCreated = (user: User) => {
  userManagementList.value!.addUser(user);
  createDialog.value.open = false;
};
const onUpdated = (user: User) => {
  userManagementList.value!.updateUser(user);
};
</script>

<template>
  <ViewHeader
    icon="mdi mdi-account-multiple"
    :title="$t('usersManagement')"
    :description="$t('usersManagementDescription')"
  />
  <section class="h-full">
    <UsersManagementList
      ref="userManagementList"
      @updateUser="openUpdateDialog"
      @createUser="openCreateDialog"
      :service="userService"
    />
  </section>
  <Dialog v-model:visible="updateDialog.open" modal>
    <template #header>
      <h1></h1>
    </template>
    <UsersManagementUpdate
      @updated="onUpdated"
      :user="updateDialog.user!"
      :service="userService"
      @close="updateDialog.open = false"
    />
  </Dialog>
  <Dialog v-model:visible="createDialog.open" modal>
    <template #header>
      <h1></h1>
    </template>
    <UsersManagementCreate
      @created="onCreated"
      :service="userService"
      @close="createDialog.open = false"
    />
  </Dialog>
</template>
