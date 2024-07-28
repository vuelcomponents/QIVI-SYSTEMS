<script setup lang="ts">
defineProps<{
  icon: string;
  title: string;
  description: string;
}>();
</script>

<template>
  <header
    class="flex justify-between bg-west-2 p-4 px-4 pb-0 pt-4 font-dms text-west-3 dark:bg-surface-950 dark:text-surface-100/90"
  >
    <div class="flex w-full items-center rounded-lg dark:ring-0">
      <div class="flex items-center text-5xl font-bold">
        <i :class="[icon, 'pr-2']" />
      </div>
      <div class="flex flex-col justify-center text-sm">
        <p v-text="title" class="font-bold" />
        <p v-text="description" />
      </div>
    </div>
    <div></div>
  </header>
</template>
