<script setup lang="ts">
defineProps<{
  active: boolean;
  icon?: string;
  title: string;
  blackRing?: boolean;
  disabled?: boolean;
}>();
</script>

<template>
  <div
    :class="[
      `text-md mb-2 flex justify-between  rounded-lg ring-1`,
      active
        ? `${blackRing ? 'ring-light-300 dark:ring-surface-700' : 'dark:ring-light-600'}
              bg-light-500/10 dark:bg-light-500/5`
        : 'ring-surface-300',
      { 'opacity-50': disabled },
    ]"
  >
    <div class="col-span-1 flex items-center justify-center p-2 text-lg lg:text-6xl" v-if="icon">
      <i :class="icon" />
    </div>
    <div class="flex w-full justify-start p-2">
      <div>
        <h1 class="text-sm font-bold">{{ $t(title) }}</h1>
        <slot name="description" />
      </div>
    </div>
    <div class="flex flex-col items-end justify-center p-2">
      <div class="grid">
        <slot name="default" />
      </div>
    </div>
  </div>
</template>
