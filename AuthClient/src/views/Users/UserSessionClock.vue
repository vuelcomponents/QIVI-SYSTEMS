<template>
  <div v-if="view === 'token'" class="text-[0.7em] lg:text-sm">
    <div>{{ countDownHoursRef }} : {{ countDownMinutesRef }} : {{ countDownSecondsRef }}</div>
  </div>
  <div v-if="view === 'refresh'" class="text-[0.7em] lg:text-sm">
    <div v-if="!user.suppressTokenRefresh">
      {{ countDownRefreshHoursRef }} : {{ countDownRefreshMinutesRef }} :
      {{ countDownRefreshSecondsRef }}
    </div>
    <div v-else>{{ $t('refreshLocked') }}</div>
  </div>
</template>
<script setup lang="ts">
import type { User } from '@/types/User';
import { ref } from 'vue';
import UserDialogTimeCalc from '@/components/referenced/dialogs/UserDialogTimeCalc';
const props = defineProps<{
  user: User;
  view: string;
}>();

const expiry = ref(1000 * 60 * props.user.claimTokenExpiryMinutes);
const refresh = ref(expiry.value * 0.7);

const sud = props.user.signedInDateTime;
const signedInTime = new Date(sud);

const countDownHoursRef = ref<number | string>(0);
const countDownMinutesRef = ref<number | string>(0);
const countDownSecondsRef = ref<number | string>(0);
const countDownRefreshHoursRef = ref<number | string>(0);
const countDownRefreshMinutesRef = ref<number | string>(0);
const countDownRefreshSecondsRef = ref<number | string>(0);

if (props.view === 'token') {
  setInterval(() => {
    expiry.value = 1000 * 60 * props.user.claimTokenExpiryMinutes;
    refresh.value = expiry.value * 0.7;
    const timeDifference = expiry.value - (new Date().getTime() - signedInTime.getTime());
    const { cds, cdm, cdh } = UserDialogTimeCalc(timeDifference);
    countDownHoursRef.value = cdh < 10 ? '0' + cdh : cdh;
    countDownMinutesRef.value = cdm < 10 ? '0' + cdm : cdm;
    countDownSecondsRef.value = cds < 10 ? '0' + cds : cds;
    if (cdh < 1 && cdm < 1 && cds < 1) {
      countDownHoursRef.value = '00';
      countDownMinutesRef.value = '00';
      countDownSecondsRef.value = '00';
    }
  }, 1000);
}

if (props.view === 'refresh') {
  setInterval(() => {
    expiry.value = 1000 * 60 * props.user.claimTokenExpiryMinutes;
    refresh.value = expiry.value * 0.7;
    const timeDifference = refresh.value - (new Date().getTime() - signedInTime.getTime());
    const { cds, cdm, cdh } = UserDialogTimeCalc(timeDifference);
    countDownRefreshHoursRef.value = cdh < 10 ? '0' + cdh : cdh;
    countDownRefreshMinutesRef.value = cdm < 10 ? '0' + cdm : cdm;
    countDownRefreshSecondsRef.value = cds < 10 ? '0' + cds : cds;
    if (cdh < 1 && cdm < 1 && cds < 1) {
      countDownRefreshHoursRef.value = '00';
      countDownRefreshMinutesRef.value = '00';
      countDownRefreshSecondsRef.value = '00';
    }
  }, 1000);
}
</script>
