<template>
  <BeerCard>
    <template #header>
      <i class="mdi mdi-bell-ring" />
      {{ $t('activities') }}
    </template>
    <template #content>
      <VuelCalendar
        :vuel-calendar-options="options as VuelCalendarOptions"
        :colors="colors"
        class="rounded-xl"
      />
    </template>
  </BeerCard>
</template>
<script setup lang="ts">
import {
  type Colors,
  type IVuelCalendarApi,
  type IVuelCalendarOptions,
  VuelCalendarOptions,
} from 'vuelcalendar';
import { ref } from 'vue';
import VuelCalendar from 'vuelcalendar';
import BeerCard from '@/components/reusable/cards/BeerCard.vue';

const calendarApi = ref<IVuelCalendarApi>();
const options: IVuelCalendarOptions = {
  height: 400,
  theme: 'light',
  startDate: new Date(),
  endDate: new Date(new Date().setDate(new Date().getDate() + 1)),
  startHour: 8,
  endHour: 20,
  draggableEvents: true,
  resizableEvents: true,
  showCursorTime: true,
  throwErrors: false,
  onVuelCalendarApiReady: (api: IVuelCalendarApi) => {
    calendarApi.value = api;
    api.setEvents([
      {
        id: 1,
        label: 'Event #1',
        data: {},
        start: new Date(new Date(new Date().setHours(8, 0)).setDate(22)),
        end: new Date(new Date(new Date().setHours(18, 2)).setDate(26)),
      },
      {
        id: 20,
        label: 'Event #20',
        data: {},
        start: new Date(new Date(new Date().setHours(8, 0)).setDate(22)),
        end: new Date(new Date(new Date().setHours(18, 2)).setDate(22)),
      },
    ]);
  },
};
const colors: Colors = {
  surface: '#334155',
  primary: '#33415570',
  event: '',
  highlight: '',
  textPrimary: '',
  menuBg: '',
  dragging: '',
};
</script>
