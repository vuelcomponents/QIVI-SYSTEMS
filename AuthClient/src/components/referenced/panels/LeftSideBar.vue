<script setup lang="ts">
import type { StoreHandler } from '@/stores/StoreHandler';
import { computed, inject, ref } from 'vue';
import { router } from '@/router/router';
import { useI18n } from 'vue-i18n';
import { useRoute } from 'vue-router';
import type { TinyEmitter } from 'tiny-emitter';

const storeHandler: StoreHandler = inject<StoreHandler>('storeHandler')!;
const emitter: TinyEmitter = inject<TinyEmitter>('emitter')!;
const route = useRoute();
const expanded = ref(true);
const toggleExpanded = () => {
  expanded.value = !expanded.value;
};
const handleClick = (item: { route?: { name: string } }) => {
  if (item.route) {
    if (expanded.value && window.outerWidth < 700) expanded.value = false;
    router.push({ name: item.route.toString() });
  }
};

const { t } = useI18n({});
const items = computed<any>(() =>
  [
    {
      label: t('dashboard'),
      icon: 'mdi mdi-home-lightning-bolt',
      route: 'Dashboard',
      active: route.matched.some((m) => m.path.includes('dashboard')),
    },
    {
      label: t('account'),
      icon: 'mdi mdi-card-account-details',
      route: 'EditAccount',
      active: route.matched.some((m) => m.path.includes('edit-account')),
    },
    storeHandler.userStore.user.role > 1
      ? {
          label: t('users'),
          icon: 'mdi mdi-account-multiple',
          route: 'UsersManagement',
          active: route.matched.some((m) => m.path.includes('users-management')),
        }
      : null,
  ].filter((i) => i !== null)
);

emitter.on('toggleMenu', (bol: boolean) => {
  toggleExpanded();
});
</script>

<template>
  <nav class="h-full w-fit bg-west-2 p-2 dark:bg-surface-1000 " v-show="expanded">
    <div class="h-full w-full rounded-xl bg-white/10 px-8 dark:bg-black/10 xl:min-w-[300px] ">
      <p
        class="text-md relative flex items-center justify-between gap-2 p-4 text-west-3 dark:text-surface-200"
      >
        <!--      <i class="font-bold mdi mdi-cloud text-xl absolute top-2 left-4 Z-[3]"/>-->
        <!--    <span class="bg-green-500 rounded-full h-[20px] flex items-center">-->
        <!--       <i class="mdi mdi-earth text-xl text-surface-200 "/>-->
        <!--    </span>-->
        <i class="mdi mdi-vector-triangle text-3xl font-bold" />
        <span class="flex font-bold">
          {{ $t('QIVI') }}
          <span class="text-[0.5em]"> AUTH 1.0 </span>
        </span>
      </p>
      <div class="h-[1px] w-full bg-surface-200/20">
        <div class="py-8">
          <div
            class="my-3 flex cursor-pointer select-none items-center justify-between rounded-xl p-2 text-west-3 hover:backdrop-blur dark:text-surface-200"
            v-for="item in items"
            :class="{
              'bg-west-1 font-bold text-surface-200 shadow-md dark:bg-surface-200 dark:text-surface-800':
                item.active,
              ' transition-colors hover:bg-surface-200 dark:hover:bg-surface-200/20 ': !item.active,
            }"
            @click="item.route ? $router.push({ name: item.route }) : ''"
          >
            <div class="flex items-center gap-2">
              <div
                class="flex h-[36px] w-[36px] items-center justify-center rounded-xl shadow-md"
                :class="[
                  item.active ? 'bg-gradient-to-br from-forest-300 to-forest-400' : 'bg-white/85',
                ]"
              >
                <i :class="[item.icon, item.active ? 'text-surface-200' : 'text-surface-700']" />
              </div>
              <span class="font-mulish text-sm">
                {{ item.label }}
              </span>
            </div>
            <span>
              <i class="mdi mdi-chevron-right" />
            </span>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<style></style>
