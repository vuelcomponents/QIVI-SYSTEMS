<script setup lang="ts">
import { computed, inject, ref } from 'vue';
import type { StoreHandler } from '@/Stores/StoreHandler';
import { useI18n } from 'vue-i18n';
import { useRoute } from 'vue-router';
import type { TinyEmitter } from 'tiny-emitter';

const storeHandler: StoreHandler = inject<StoreHandler>('storeHandler')!;

const expanded = ref(true);
const toggleExpanded = () => {
  expanded.value = !expanded.value;
};
const emitter = inject<TinyEmitter>('emitter')!;
const route = useRoute();
const { t } = useI18n({});
const items = computed<any>(() =>
  [
    {
      label: t('dashboard'),
      icon: 'mdi mdi-home-lightning-bolt',
      route: 'Dashboard',
    },
    {
      label: t('employees'),
      icon: 'mdi mdi-account-hard-hat',
      route:'Employee',
      active:route.matched.some(r=>r.path.includes('employees'))
      // items:[
      //   {
      //     label: t('listOfProducts'),
      //     icon: 'mdi mdi-escalator-box',
      //     route: 'Employee_EmployeeSpecifications',
      //   },
      //   {
      //     label: t('productCategories'),
      //     icon: 'mdi mdi-source-branch',
      //     route: 'ProductCategories',
      //   },
      //   {
      //     label: t('productSpecifications'), // warianty na produkty
      //     icon: 'mdi mdi-book-variant',
      //     route: 'ProductSpecifications',
      //   }
      // ]
    },
    {
      label: t('companies'),
      icon: 'mdi mdi-domain',
      // items:[
      //   {
      //     label: t('listOfShelves'), // polki beda przyjmowac requirementy wariantowe
      //     icon: 'mdi mdi-library-shelves',
      //     route: 'Shelves',
      //   },
      //   {
      //     label: t('storeLayout'),  // bedzie to rozmieszczenie polek - budujemy divy wg grida z danych i mozna zaznaczac mnoga liczbe polek, a nastepnie dla kilku przypisywac requirenmenty
      //     icon: 'mdi mdi-view-grid-plus',
      //     route: 'ShelvesSpecifications',
      //   },
      // ]
    },
    {
      label: t('housingAssistant'),
      icon: 'mdi mdi-home-assistant',
      // items:[
      //   {
      //     label: t('listOfLoans'), // polki beda przyjmowac requirementy wariantowe
      //     icon: 'mdi mdi-cash-clock',
      //     route: 'Loans',
      //   },
      // ]
    },
    {
      label: t('recruiter'),
      icon: 'mdi mdi-briefcase-download',
      // items:[
      //   {
      //     label: t('clients'),
      //     icon: 'mdi mdi-account-heart-outline',
      //     route: 'Clients',
      //   },
      //   {
      //     label: t('blacklist'),
      //     icon: 'mdi mdi-account-cowboy-hat',
      //     route: 'Blacklist',
      //   }
      // ]
    },
    {
      label: t('documents'),
      icon: 'mdi mdi-printer',
      // items: [
      //   {
      //     label: t('deals'),
      //     icon: 'mdi mdi-attachment-lock',
      //     route: 'Deals',
      //   },
      //   {
      //     label: t('taxes'),
      //     icon: 'mdi mdi-paper-roll',
      //     route: 'Taxes',
      //   },
      // ],
    },
    {
      label: t('integrations'),
      icon: 'mdi mdi-connection',
      // items: [
      //   {
      //     label: t('allegro'),
      //     icon: 'mdi mdi-store-marker',
      //     route: 'AllegroIntegration',
      //   },
      //   {
      //     label: t('olx'),
      //     icon: 'mdi mdi-store-marker',
      //     route: 'OlxIntegration',
      //   },
      // ],
    },
    {
      label: t('statistics'),
      icon: 'mdi mdi-chart-arc',
      // items: [
      //   {
      //     label: t('topSellers'),
      //     icon: 'mdi mdi-chart-arc',
      //     route: 'Deals',
      //   },
      // ],
    },
  ]);

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
        <i class="mdi mdi-language-r text-3xl font-bold" />
        <span class="flex font-bold">
          {{ $t('HR TECHNIQUE') }}
          <span class="text-[0.5em]"> DEV </span>
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
