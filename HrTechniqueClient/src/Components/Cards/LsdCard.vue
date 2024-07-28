<script setup lang="ts">
defineProps<{
  active: boolean;
  icon?: string;
  title: string;
  blackRing?: boolean;
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
      'overflow-auto',
    ]"
  >
    <div class="flex w-full justify-between p-2">
      <div class="break-all text-sm">
        <h1 class="text-sm font-bold">{{ $t(title) }}</h1>
        <slot name="description" />
      </div>
      <div class="flex justify-end">
        <slot name="right" />
      </div>
    </div>
  </div>
</template>
