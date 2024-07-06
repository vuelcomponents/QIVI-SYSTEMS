<template>
  <div
    class="flex h-[50px] items-center rounded-t-lg bg-west-2 p-2 px-4 text-[0.7em] text-west-3 ring-1 ring-surface-500/10 dark:bg-surface-950 dark:text-surface-200/30 dark:ring-1 dark:ring-surface-600/80"
  >
    <p>{{ $t('securityChart') }}</p>
  </div>
  <div
    class="card bg-white-1 flex h-full w-full flex-1 items-center justify-center rounded-b-lg p-4 ring-1 ring-surface-200 dark:bg-surface-950 dark:ring-surface-700"
  >
    <Chart
      type="line"
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
  devicesCount: Array<number>;
  blockedDevices: Array<number>;
  blockedIps: Array<Number>;
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
  () => [props.devicesCount, props.blockedDevices, props.blockedIps],
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
        label: t('Devices Quantity'),
        data: props.devicesCount,
        fill: false,
        tension: 0.4,
        borderColor: '#68da23',
        backgroundColor: '#68da23',
      },
      {
        label: t('Blocked Devices'),
        data: props.blockedDevices,
        fill: false,
        tension: 0.4,
        borderColor: '#7af298',
        backgroundColor: '#7af298',
      },
      {
        label: t('Blocked IPs'),
        data: props.blockedIps,
        fill: true,
        borderColor: '#4cc35b',
        backgroundColor: '#4cc35b',
        tension: 0.4,
      },
    ],
  };
};
const setChartOptions = () => {
  const textColor = '#ffffff70';
  const textColorSecondary = '#ffffff40';
  const surfaceBorder = '#ffffff40';
  return {
    maintainAspectRatio: false,
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
