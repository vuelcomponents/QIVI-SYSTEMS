<template>
  <div class="flex w-full items-center justify-center">
    <button class="w-full rounded-xl bg-west-3 ring-inset dark:bg-west-2">
      <!--      @click="$router.push({name:'order', params:{id:params.data.to_nag_id}})"-->
      <i class="mdi mdi-package-variant rounded-lg px-1.5 py-1 text-surface-200 dark:text-west-3" />
    </button>
  </div>
</template>
<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import Button from 'primevue/button';
import type { ICellRendererParams } from 'ag-grid-community';

export default defineComponent({
  // eslint-disable-next-line vue/no-reserved-component-names
  components: { Button },
  props: {
    params: {
      type: Object as PropType<ICellRendererParams>,
      required: true,
    },
  },
});
</script>
<style></style>
