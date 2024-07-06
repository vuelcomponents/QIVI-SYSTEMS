<template>
  <div class="flex w-full items-center justify-center px-[4px] py-[6px] relative">
    <button  class="w-full h-full flex items-center justify-center bg-gradient-to-br from-forest-300 to-forest-400 rounded-xl text-surface-200/90 dark:text-surface-200  dark:ring-primary-700/50">
      <!--      @click="$router.push({name:'order', params:{id:params.data.to_nag_id}})"-->
      <i class="mdi mdi-account"
      /></button>
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
