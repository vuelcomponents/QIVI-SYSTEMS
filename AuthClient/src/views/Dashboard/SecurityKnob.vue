<script setup lang="ts">
import Knob from 'primevue/knob';
import WeedCard from '@/components/reusable/cards/WeedCard.vue';
const props = defineProps<{ globalSecuritySettings: number }>();
</script>
<template>
  <WeedCard title=" " :active="true" black-ring>
    <template #description>
      <Knob
        v-model="props.globalSecuritySettings"
        :size="50"
        valueColor="Red"
        rangeColor="#000000"
        class="h-full w-full"
      />
    </template>
    <template #default>
      <h1 class="text-[0.7em] lg:text-sm">{{ $t('usersSecuritySettingsLevel') }}</h1>
    </template>
  </WeedCard>
</template>
