<script setup lang="ts">
import AppHeader from '@/Components/Headers/_Referenced/QuarxHeader.vue';
import ProgressSpinner from 'primevue/progressspinner';
import { inject, onMounted, ref } from 'vue';
import type { TinyEmitter } from 'tiny-emitter';
import { useToast } from 'primevue/usetoast';
import type { ToastMessageOptions } from 'primevue/toast';
import { UseUserStore } from '@/Stores/UserStore';
import Toast from 'primevue/toast';
import type { VueCookies } from 'vue-cookies';

const emitter = inject('emitter') as TinyEmitter;

const toast = useToast();
const $cookies = inject<VueCookies>('$cookies');
const userStore = UseUserStore();

const load = ref(false);
const loggedIn = ref(false);
const handleEmits = () => {
  emitter.on('error', (params: ToastMessageOptions) => {
    toast.add(params);
    console.error(params.summary, params.detail);
  });
  emitter.on('remove', () => {
    toast.removeAllGroups();
  });
  emitter.on('info', (params: ToastMessageOptions) => {
    toast.add(params);
    console.info('Row has been successfully updated');
  });
  emitter.on('load', (bool: boolean) => {
    load.value = bool;
  });
  emitter.on('logged-in', (data: any) => {
    loggedIn.value = true;
    if (data) {
      if (data.token) {
        $cookies?.set('hrtechniquetoken', data.token);
      }
      if (data.user) {
        userStore.updateAuthUser(data.user);
      }
    }
  });
};

onMounted(() => {
  handleEmits();
});
</script>

<template>
  <ProgressSpinner
    v-if="load"
    class="fixed left-0 top-0 select-none"
    style="position: fixed; top: 0; left: 0"
  />
  <section
    class="section-full grid h-[100vh] min-h-[100vh] w-full overflow-auto bg-west-2 dark:bg-surface-1000"
  >
    <!--     50px -->
    <header class="right-2 top-2 z-[1000] w-full xl:fixed xl:w-fit">
      <app-header :logged-in="loggedIn" />
    </header>
    <!-- rest -->
    <section class="grid">
      <router-view />
    </section>
    <!-- 50px -->
    <Toast />
  </section>
</template>
