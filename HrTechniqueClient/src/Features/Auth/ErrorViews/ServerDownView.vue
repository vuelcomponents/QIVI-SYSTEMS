<script lang="ts">
import { defineComponent } from 'vue';
import { getAssociatedUrl } from '@/Shared/Resources/urls';

export default defineComponent({
  name: 'UnauthorizedView',
  mounted() {
    setTimeout(() => {
      location.href = getAssociatedUrl('auth-client');
    }, 10000);
  },
});
</script>

<template>
  <div class="flex w-full items-center justify-center" style="height: calc(100vh - 130px)">
    <div class="mb-3 rounded-xl p-5 pl-8 pr-8 text-surface-900 ring-1 ring-surface-200">
      <i class="mdi mdi-alert-circle-outline mr-5 text-[#f87171]" />
      {{ $t('serverDown') }}
    </div>
  </div>
</template>

<style scoped></style>
