<script setup lang="ts">
defineProps<{
  name: string;
  icon: string;
  bulb?: boolean;
  bulbCondition?: boolean;
}>();
</script>

<template>
  <div class="relative">
    <div
      class="absolute right-0 top-[5px] h-[6px] w-[6px] rounded-full bg-primary-400 dark:bg-forest-400"
      v-if="bulb && (bulbCondition ?? true)"
    />
    <i :class="[name, icon, 'cursor-pointer']" />
    <div class="absolute left-[-200px] w-[200px]">
      <slot />
    </div>
  </div>
</template>

<style scoped></style>
