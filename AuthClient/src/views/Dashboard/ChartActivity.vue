<template>
  <div
    class="flex h-[50px] items-center rounded-t-lg bg-light-700 p-2 px-4 text-sm text-surface-200 dark:bg-surface-1000 dark:ring-1 dark:ring-surface-600/80"
  >
    <p>{{ $t('monthlyVisits') }}</p>
  </div>
  <div
    class="card flex h-full w-full flex-1 items-center justify-center rounded-b-lg bg-trueGreen-400/5 p-4 ring-1 ring-surface-200 dark:bg-surface-1111 dark:ring-surface-700"
  >
    <Chart
      type="doughnut"
      :data="chartData"
      :options="chartOptions"
      class="flex h-full min-h-[300px] w-full flex-1 items-center justify-center"
    >
    </Chart>
  </div>
</template>

<script setup lang="ts">
import Chart from 'primevue/chart';
import { ref, onMounted, watch } from 'vue';
import { UseSettingStore } from '@/stores/SettingStore';
import { useI18n } from 'vue-i18n';
import { storeToRefs } from 'pinia';

const props = defineProps<{
  labels: Array<string>;
  monthlyVisits: Array<number>;
}>();

onMounted(() => {
  chartData.value = setChartData();
  chartOptions.value = setChartOptions();
});

const settingStore = storeToRefs(UseSettingStore());

watch(
  () => [settingStore.theme.value, settingStore.language.value],
  () => {
    chartData.value = setChartData();
    chartOptions.value = setChartOptions();
  }
);
watch(
  () => [props.monthlyVisits],
  () => {
    chartData.value = setChartData();
    chartOptions.value = setChartOptions();
  }
);
const chartData = ref();
const chartOptions = ref();

const { t } = useI18n();
const setChartData = () => {
  return {
    labels: props.labels,
    datasets: [
      {
        label: t('topMonthlyVisits'),
        data: props.monthlyVisits,
        fill: false,
        tension: 0.4,
        borderColor: settingStore.theme.value === 'dark' ? ['#9ed5c5', '#66d723'] : ['#13c4cb'],
        backgroundColor:
          settingStore.theme.value === 'dark' ? ['#13c1a360', '#66d72360'] : ['#0c4fe1'],
      },
    ],
  };
};
const setChartOptions = () => {
  const textColor = settingStore.theme.value === 'dark' ? '#ffffff70' : '#2f528880';
  const textColorSecondary = settingStore.theme.value === 'dark' ? '#ffffff40' : '#2f528840';
  const surfaceBorder = settingStore.theme.value === 'dark' ? '#ffffff40' : '#2f528830';
  return {
    maintainAspectRatio: true,
    aspectRatio: 1.6,
    plugins: {
      legend: {
        labels: {
          color: textColor,
        },
      },
    },
    scales: {
      x: {
        ticks: {
          color: textColorSecondary,
          display: false,
        },
        grid: {
          color: surfaceBorder,
        },
      },
      y: {
        ticks: {
          color: textColorSecondary,
          callback: function (value: any) {
            if (Number.isInteger(value)) {
              return value.toString();
            } else {
              return '';
            }
          },
        },
        grid: {
          color: surfaceBorder,
        },
      },
    },
  };
};
</script>
