<script setup lang="ts">
import type { ICellRendererParams } from 'ag-grid-community';
defineProps<{
  params: ICellRendererParams;
}>();
</script>

<template>
  <div>
    <div v-if="params.data[params.colDef!.field as string]">
      <i class="mdi mdi-check" />
    </div>
    <div v-else>
      <i class="mdi mdi-cancel" />
    </div>
  </div>
</template>

<style scoped></style>
