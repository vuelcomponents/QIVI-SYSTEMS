<script setup lang="ts">
import type { ICellRendererParams } from 'ag-grid-community';

const props = defineProps<{
  params: ICellRendererParams;
}>();
</script>

<template>
  <div v-if="params.data.verified">
    <i class="mdi mdi-check" />
  </div>
  <div v-else>
    <i class="mdi mdi-cancel" />
  </div>
</template>

<style scoped></style>
