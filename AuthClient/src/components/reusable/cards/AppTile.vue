<script setup lang="ts">
defineProps<{
  icon: string;
  tooltip: string;
  dashboard?: boolean;
}>();
</script>

<template>
  <div
    class="group flex h-full min-h-[100px] flex-col items-center justify-center rounded-sm ring-1 ring-surface-100 group-hover:h-[100%] group-hover:w-[100%] dark:ring-surface-100/5"
    :class="{ 'dark:ring-0': dashboard }"
  >
    <div
      class="flex h-fit w-fit cursor-pointer flex-col items-center justify-center rounded-full p-1 text-sm transition-all group-hover:m-0 group-hover:h-[100%] group-hover:w-[100%] group-hover:rounded-none group-hover:bg-surface-0 group-hover:text-xl group-hover:shadow-md group-hover:ring-surface-0/10 group-hover:contrast-125 group-hover:dark:bg-forest-300"
    >
      <div
        class="rounded-full bg-[white]/90 p-3 pl-4 pr-4 ring-1 ring-surface-0/30 backdrop-contrast-125 dark:bg-forest-300 dark:ring-0"
      >
        <i :class="icon" class="text-3xl text-[#2f5288] dark:text-forest-100" v-tooltip="tooltip" />
      </div>
    </div>
  </div>
</template>
