<script setup lang="ts">
import PanelMenu from 'primevue/panelmenu';
import { computed, ref } from 'vue';
import { router } from '@/router/router';
import { useI18n } from 'vue-i18n';

const expanded = ref(true);
const toggleExpanded = () => {
  expanded.value = !expanded.value;
};
const { t } = useI18n({});
const handleClick = (data: { name: string; query: { content: string } }) => {
  if (expanded.value && window.outerWidth < 700) expanded.value = false;
  router.push({ name: data.name, query: data.query });
};
const items = computed<any>(() => [
  {
    label: t('QUARX'),
    icon: 'mdi mdi-cloud-lock',
    items: [
      {
        label: t('aboutQuarx'),
        icon: 'mdi mdi-information',
        command: () => handleClick({ name: 'AboutQuarx', query: { content: 'aboutQuarx' } }),
      },
      {
        label: t('authorization'),
        icon: 'mdi mdi-two-factor-authentication',
        command: () => handleClick({ name: 'AboutQuarx', query: { content: 'authorization' } }),
      },
      {
        label: t('licences'),
        icon: 'mdi mdi-scale-balance',
        command: () => handleClick({ name: 'AboutQuarx', query: { content: 'licences' } }),
      },
      {
        label: t('hostDistribution'),
        icon: 'mdi mdi-server',
        command: () => handleClick({ name: 'AboutQuarx', query: { content: 'hostDistribution' } }),
      },
      {
        label: t('usersManagement'),
        icon: 'mdi mdi-account-multiple',
        command: () => handleClick({ name: 'AboutQuarx', query: { content: 'usersManagement' } }),
      },
      {
        label: t('securitySettings'),
        icon: 'mdi mdi-security',
        command: () => handleClick({ name: 'AboutQuarx', query: { content: 'securitySettings' } }),
      },
    ],
  },
  {
    label: t('HrTechnique'),
    icon: 'mdi mdi-language-r',
    items: [
      {
        label: t('aboutHrt'),
        icon: 'mdi mdi-information',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'aboutHrt' } }),
      },
      {
        label: t('employees'),
        icon: 'mdi mdi-account-hard-hat',
        route: 'Employees',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'employees' } }),
      },
      {
        label: t('companies'),
        icon: 'mdi mdi-domain',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'companies' } }),
      },
      {
        label: t('housingAssistant'),
        icon: 'mdi mdi-home-assistant',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'housingAssistant' } }),
      },
      {
        label: t('recruiter'),
        icon: 'mdi mdi-briefcase-download',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'recruiter' } }),
      },
      {
        label: t('documents'),
        icon: 'mdi mdi-printer',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'documents' } }),
      },
      {
        label: t('integrations'),
        icon: 'mdi mdi-connection',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'integrations' } }),
      },
      {
        label: t('statistics'),
        icon: 'mdi mdi-chart-arc',
        command: () => handleClick({ name: 'AboutHrt', query: { content: 'statistics' } }),
      },
    ],
  },
  {
    label: t('Lombandit'),
    icon: 'mdi mdi-account-cowboy-hat',
    items: [
      {
        label: t('aboutLombandit'),
        icon: 'mdi mdi-information',
        command: () =>
          router.push({ name: 'AboutLombandit', query: { content: 'aboutLombandit' } }),
      },
      {
        label: t('products'),
        icon: 'mdi mdi-escalator-box',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'products' } }),
      },
      {
        label: t('shelves'),
        icon: 'mdi mdi-library-shelves',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'shelves' } }),
      },
      {
        label: t('loans'),
        icon: 'mdi mdi-cash-clock',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'loands' } }),
      },
      {
        label: t('clients'),
        icon: 'mdi mdi-account-heart-outline',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'clients' } }),
      },
      {
        label: t('documents'),
        icon: 'mdi mdi-printer',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'documents' } }),
      },
      {
        label: t('integrations'),
        icon: 'mdi mdi-connection',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'integrations' } }),
      },
      {
        label: t('statistics'),
        icon: 'mdi mdi-chart-arc',
        command: () => router.push({ name: 'AboutLombandit', query: { content: 'statistics' } }),
      },
    ],
  },
]);
</script>

<template>
  <main
    :class="[
      'h-full w-full  border-r-[1px] border-r-gray-200 bg-light-700 shadow-md dark:border-none dark:bg-forest-100 ',
      expanded
        ? 'max-w-screen absolute z-50 w-screen md:relative md:max-w-[13rem]'
        : 'max-w-[4rem] transition-all',
    ]"
  >
    <section :class="[' p-2']">
      <div
        @click="toggleExpanded"
        :class="[
          'flex  cursor-pointer  rounded-md bg-light-600  px-2 py-1 text-surface-200 ring-2 ring-light-600 transition-colors hover:bg-light-700 dark:rounded-sm ' +
            ' dark:bg-gradient-to-r dark:from-[#6de524] dark:to-[#40a151] dark:text-mintSplash-deepblack dark:ring-0',
          expanded ? ' justify-between   ' : 'justify-center',
        ]"
      >
        <span v-if="expanded"
          ><strong>{{ $t('tools') }}</strong></span
        >
        <div class="cursor-pointer">
          <i class="mdi mdi-arrow-left" v-if="expanded" />
          <i class="mdi mdi-arrow-right" v-else />
        </div>
      </div>
    </section>
    <section class="p-2">
      <PanelMenu :model="items" class="font-dms dark:bg-forest-100">
        <template #item="{ item, label, active, hasSubmenu }">
          <div
            @click="() => (item.route ? router.push({ name: item.route }) : () => {})"
            v-ripple
            v-tooltip="!expanded && label"
            :class="[
              'group mb-2 flex cursor-pointer items-center px-3  py-1  text-trueGreen-400 shadow-sm  ring-mintSplash-0/20 transition-colors hover:shadow-lg hover:ring-light-700 dark:bg-forest-100 dark:text-surface-0  dark:hover:ring-mintSplash-200 ',
              expanded
                ? 'justify-between   rounded-none py-1  '
                : ' justify-center rounded-lg  ring-1 dark:rounded-none dark:bg-[#000000] dark:ring-0 dark:ring-forest-400  dark:hover:bg-forest-400',
            ]"
          >
            <span>
              <i
                :class="[
                  'text-trueGreen-600  transition-all group-hover:text-yellow-200  dark:text-mintSplash-300 group-hover:dark:text-forest-100  ',
                  { ' group-hover:dark:text-forest-400': expanded },
                  item.icon,
                ]"
              />
              <span
                v-if="expanded"
                :class="[
                  'ml-4  mr-4 leading-none text-surface-200 transition-all group-hover:text-yellow-100 dark:text-surface-100 dark:group-hover:text-mintSplash-200 ',
                  'font-dms',
                  { 'font-semibold': hasSubmenu },
                ]"
              >
                {{ label }}
              </span>
            </span>
            <i
              v-if="hasSubmenu && expanded"
              :class="[
                'mdi font-bold  dark:text-surface-300',
                !active ? 'mdi-chevron-down' : 'mdi-chevron-up',
              ]"
            />
          </div>
        </template>
      </PanelMenu>
    </section>
  </main>
</template>
